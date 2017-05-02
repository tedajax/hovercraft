using System;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDeformer : MonoBehaviour
{
    Terrain terrain;

    int width;
    int height;
    float[,] terrainHeights;

    void Awake()
    {
        terrain = GetComponent<Terrain>();

        if (terrain == null) {
            Destroy(this);
        }

        width = terrain.terrainData.heightmapWidth;
        height = terrain.terrainData.heightmapHeight;
        terrainHeights = new float[width, height];
    }

    void Update()
    {
        for (int y = 0; y < height; ++y) {
            for (int x = 0; x < width; ++x) {
                terrainHeights[x, y] = ((Mathf.Sin(x * 0.1f) + 1) / 2f) * 0.01f + 0.001f;
            }
        }

        terrain.terrainData.SetHeights(0, 0, terrainHeights);
    }
}