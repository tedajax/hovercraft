using UnityEngine;
using System;

[Serializable]
public class BulletData
{
    public float speed;
}

public class Bullet : MonoBehaviour
{
    public BulletData data;

    void Update()
    {
        Vector3 position = transform.position;
        position += transform.forward * data.speed * Time.deltaTime;
        transform.position = position;
    }
    
}