using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}