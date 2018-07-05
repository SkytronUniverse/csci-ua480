using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is based on the tutorial given by Renan Oliveira at
 * https://gamedevacademy.org/complete-guide-to-procedural-level-generation-in-unity-part-1/
 * It is used to procedurally generate a landscape out of a plane.
 * 
 * This script is attached to: 
 *      Ground Prefab
 */

namespace kmb_assignment04
{
    [System.Serializable]
    public class TerrainType
    {
        public string name;
        public float height;
        public Color color;
    }

    public class TileGeneration : MonoBehaviour
    {
        [SerializeField]
        private Wave[] waves; //waves will be use to create a more polished looking play area

        [SerializeField]
        private AnimationCurve heightCurve; //Animation curve is used to make sure that everything below height of 0.35 is a plane (i.e. craters will be flat)

        [SerializeField]
        private float heightMultiplier; //variable to manipulate the heights of vertices

        [SerializeField]
        private TerrainType[] terrainTypes; //array to store each type of terrain based on height

        [SerializeField]
        PlaneManipulator planeManipulator; // object created from PlaneManipulator.cs

        [SerializeField]
        private MeshRenderer tileRenderer; //Mesh Renderer of GameObject

        [SerializeField]
        private MeshCollider meshCollider; //Mesh Collider of GameObject

        [SerializeField]
        private MeshFilter meshFilter; //Mesh Filter of Game Object to send assets to Mesh Renderer to be created on screen

        [SerializeField]
        private float mapScale; // Variable used for scaling of the terrain

        void Start()
        {
            MakeTile();
        }

        void MakeTile()
        {
            //calculate dept and width based on mesh vertices
            Vector3[] meshVert = this.meshFilter.mesh.vertices;
            int tileDepth = (int)Mathf.Sqrt(meshVert.Length);
            int tileWidth = tileDepth;

            //calculate the offsets for boundary tile heights so they match up between planes
            float offsetX = -this.gameObject.transform.position.x;
            float offsetZ = -this.gameObject.transform.position.z;

            //calculate offsets based on tile position
            float[,] heightArray = this.planeManipulator.GenerateNoiseMap(tileDepth, tileWidth, this.mapScale, offsetX, offsetZ, waves); //heightArray contains all of the heights for each vertex of plane

            // Generate the height map using noise
            Texture2D tileTexture = CreateTexture(heightArray);
            this.tileRenderer.material.mainTexture = tileTexture;

            // Update the vertices based on the data from the height map
            UpdateMesh(heightArray);
        }

        private Texture2D CreateTexture(float[,] heightArray)
        {
            int tileDepth = heightArray.GetLength(0);
            int tileWidth = heightArray.GetLength(1);

            Color[] colorArray = new Color[tileDepth * tileWidth]; // Contains all of the colors that correspond to hieghts of each vertex of plane

            for (int zIndex = 0; zIndex < tileDepth; zIndex++)
            {
                for (int xIndex = 0; xIndex < tileWidth; xIndex++)
                {
                    //transform the 2D map index in an Array Index
                    int colorIndex = zIndex * tileWidth + xIndex;
                    float height = heightArray[zIndex, xIndex];
                    // TerrainType based on height value
                    TerrainType terrainType = ChooseTerrainType(height);
                    //Assign as color a shade of gray proportional to the height value
                    colorArray[colorIndex] = terrainType.color;
                }
            }

            //create a new texture and set its pixel colors
            Texture2D texture = new Texture2D(tileWidth, tileDepth) // texture of the plane
            {
                wrapMode = TextureWrapMode.Clamp // clamp texure to last pixel at edge to prevent wrapping artifacts (https://docs.unity3d.com/ScriptReference/TextureWrapMode.Clamp.html)
            };

            texture.SetPixels(colorArray);
            texture.Apply(); // It is very important, and easy to forget that after all of these changes are made Apply() must be executed

            return texture;
        }

        // Function to get terrain type (i.e. crater, ground, or mountain)
        TerrainType ChooseTerrainType(float height)
        {
            foreach (TerrainType terrainType in terrainTypes)
            {
                //Assign terrain type based on height
                if (height < terrainType.height)
                    return terrainType;
            }
            return terrainTypes[terrainTypes.Length - 1];
        }

        // Function to change the heights of the vertices of the mesh based on the values in the heightArray
        private void UpdateMesh(float[,] heightArray)
        {
            int tileDepth = heightArray.GetLength(0);
            int tileWidth = heightArray.GetLength(1);

            Vector3[] meshVertices = this.meshFilter.mesh.vertices;

            //iterate through the heightMap coordinates to update the vertex index
            int vertexIndex = 0;
            for (int zIndex = 0; zIndex < tileDepth; zIndex++)
            {
                for (int xIndex = 0; xIndex < tileWidth; xIndex++)
                {
                    float height = heightArray[zIndex, xIndex];

                    Vector3 vertex = meshVertices[vertexIndex];

                    //modify the vertex Y coordinate proportional to height
                    meshVertices[vertexIndex] = new Vector3(vertex.x, this.heightCurve.Evaluate(height) * this.heightMultiplier, vertex.z);

                    vertexIndex++;
                }
            }

            //update the vertices and properties of the mesh
            this.meshFilter.mesh.vertices = meshVertices;
            this.meshFilter.mesh.RecalculateBounds();
            this.meshFilter.mesh.RecalculateNormals();

            //update the mesh collider
            this.meshCollider.sharedMesh = this.meshFilter.mesh;
        }
    }

}
