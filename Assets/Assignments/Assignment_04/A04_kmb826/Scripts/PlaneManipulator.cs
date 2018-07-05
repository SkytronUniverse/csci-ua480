using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * This script is based on the tutorial given by Renan Oliveira at
 * https://gamedevacademy.org/complete-guide-to-procedural-level-generation-in-unity-part-1/
 * It is used to procedurally generate a landscape out of a plane. This script is responsible
 * for using Perlin noise to create values for different height level on a plane.
 * 
 * This script is attached to:
 *      Ground Prefab
 */
namespace kmb_assignment04
{
    //Wave class that will be used to create more "polished" looking height differences on the plane
    [System.Serializable]
    public class Wave
    {
        public float seed;
        public float frequency;
        public float amplitude;
    }

    public class PlaneManipulator : MonoBehaviour
    {

        // funtion that will create a 2D array with information for height values of vertices using PerlinNoise
        public float[,] GenerateNoiseMap(int depth, int width, float scale, float offsetX, float offsetZ, Wave[] waves)
        {
            float[,] noiseMap = new float[depth, width];

            for (int zIndex = 0; zIndex < depth; zIndex++)
            {
                for (int xIndex = 0; xIndex < width; xIndex++)
                {
                    //we calculate the indices based on the x and z coordinates, scale, and offset
                    float xCoord = (xIndex + offsetX) / scale;
                    float zCoord = (zIndex + offsetZ) / scale;

                    float noise = 0f;
                    float normalization = 0f;

                    foreach(Wave wave in waves)
                    {
                        //Use PerlinNoise to create a noise value
                        noise += wave.amplitude * Mathf.PerlinNoise(xCoord * wave.frequency + wave.seed, zCoord * wave.frequency + wave.seed);
                        normalization += wave.amplitude;
                    }

                    // noise value must be between 0 and 1
                    noise /= normalization;

                    // Map noise to correct index of noise map
                    noiseMap[zIndex, xIndex] = noise;
                }
            }
            return noiseMap;
        }

    }
}
