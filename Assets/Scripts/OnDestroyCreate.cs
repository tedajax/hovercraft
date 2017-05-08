using UnityEngine;

public class OnDestroyCreate : MonoBehaviour
{
    public GameObject prefab;

    void OnDestroy()
    {
        Instantiate(prefab, transform.position, transform.rotation);
    }
}