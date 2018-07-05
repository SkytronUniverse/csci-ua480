using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is based on the tutorial given by Renan Oliveira at
 * https://gamedevacademy.org/complete-guide-to-procedural-level-generation-in-unity-part-1/
 * It is used to procedurally generate a landscape out of a plane. This script will generate
 * multiple planes from the Ground Prefab, and ensure that they each line up on the edges.
 * 
 * This script is attached to: 
 *      Level GameObject
 */
namespace kmb_assignment04
{
    public class LevelGeneration : MonoBehaviour
    {
        [SerializeField]
        private int mapWidth, mapDepth; // number of tiles for level generation

        [SerializeField]
        private GameObject groundPrefab; 

        private void Start()
        {
            GenerateMap();
        }

        //function to create level map
        void GenerateMap()
        {
            //dimension of the tile prefab used for level generation
            Vector3 tileSize = groundPrefab.GetComponent<MeshRenderer>().bounds.size;
            int tileWidth = (int)tileSize.x;
            int tileDepth = (int)tileSize.z;

            for(int xIndex = 0; xIndex < mapWidth; xIndex++)
            {
                for(int zIndex = 0; zIndex < mapDepth; zIndex++)
                {
                    //Get position of next tile
                    Vector3 tPosition = new Vector3(this.gameObject.transform.position.x + xIndex * tileWidth, this.gameObject.transform.position.y, this.gameObject.transform.position.z + zIndex*tileDepth);
                    GameObject tile = Instantiate(groundPrefab, tPosition, Quaternion.identity); //Instantiate new ground right next to previous
                }
            }
        }
    }
}
