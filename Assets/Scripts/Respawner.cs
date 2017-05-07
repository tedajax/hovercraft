using UnityEngine;

public class Respawner : MonoBehaviour
{
    public float killY = -5f;

    private new Rigidbody rigidbody;
    private Vector3 respawnPoint;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        respawnPoint = transform.position;
    }

    void Update()
    {
        if (Input.GetButtonDown("Reset")) {
            Respawn();
        }

        if (transform.position.y <= killY) {
            Respawn();
        }
    }

    public void Respawn()
    {
        rigidbody.velocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.position = respawnPoint;
    }
}