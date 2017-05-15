using System;
using UnityEngine;

[ExecuteInEditMode]
public class SnapToTerrain : MonoBehaviour
{
    public Terrain terrain;
    public float extraHeight = 0f;

    void Awake()
    {
        if (terrain == null) {
            var terrainObj = GameObject.FindGameObjectWithTag("Terrain");
            if (terrainObj != null) {
                terrain = terrainObj.GetComponent<Terrain>();
            }
            else {
                Debug.LogWarning("Unable to find a terrain to snap to.");
            }
        }
    }

    void Update()
    {
        if (terrain == null) {
            return;
        }

        float terrainHeight = terrain.SampleHeight(transform.position);

        Vector3 position = transform.position;
        position.y = terrainHeight + extraHeight;
        transform.position = position;
    }
}