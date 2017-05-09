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

    public float lifetime = 0f;

    public GameObject explosionPrefab;

    public Vector3 AddedVelocity { get; set; }

    void Update()
    {
        if (lifetime > 0f) {
            lifetime -= Time.deltaTime;
            if (lifetime <= 0f) {
                destroy();
            }
        }

        Vector3 position = transform.position;
        position += (AddedVelocity + transform.forward * data.speed) * Time.deltaTime;
        transform.position = position;
    }

    public void OnCollisionEnter(Collision collision)
    {
        destroy();
    }

    private void destroy()
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}