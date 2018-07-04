using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {

    public int width = 256;
    public int height = 256;

    public int depth = 20;

    public float scale = 50f;

    public float offsetX = 100f;
    public float offsetY = 100f;

    private void Start()
    {
        offsetX = Random.Range(0f, 99999f);
        offsetY = Random.Range(0f, 99999f);

        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    TerrainData GenerateTerrain(TerrainData tData)
    {
        tData.heightmapResolution = width + 1; 
        tData.size = new Vector3(width, depth, height);

        tData.SetHeights(0, 0, GenerateHeights());

        return tData;

    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];

        for(int x = 0; x < 10; x++)
        {
            for(int y = 0; y < 10; y++ )
            {
                heights[x,y] = Mathf.PerlinNoise(x,y);
            }
        }

        for(int x = 10; x < width-10; x++)
        {
            for(int y = 10; y < height-10; y++)
            {
                heights[x, y] = CalculateHeight(x, y);
            }
        }

        return heights;
    }

    float CalculateHeight(int x, int y)
    {
        float xCoord = (float) x / width * scale + offsetX;
        float yCoord = (float) y / width * scale + offsetY;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
