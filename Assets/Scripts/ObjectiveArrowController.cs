using UnityEngine;

public class ObjectiveArrowController : MonoBehaviour
{
    public Transform fromTransform;
    public Transform targetTransform;

    void Update()
    {
        if (targetTransform == null) {
            return;
        }

        Vector3 delta = targetTransform.position - fromTransform.position;
        delta.y = 0f;
        delta.Normalize();
        float angle = Mathf.Atan2(delta.x, delta.z) + Mathf.PI * 2f;

        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg - fromTransform.rotation.eulerAngles.y, Vector3.up);
    }
}