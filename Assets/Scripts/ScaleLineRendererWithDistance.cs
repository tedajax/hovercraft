using UnityEngine;

public class ScaleLineRendererWithDistance : MonoBehaviour
{
    public Transform observerTransform;
    public LineRenderer lineRenderer;

    public float minDistance = 0f;
    public float maxDistance = 50f;

    public float minSize = 0f;
    public float maxSize = 50f;

    void Awake()
    {
        if (observerTransform == null) {
            observerTransform = Camera.main.transform;
        }

        if (lineRenderer == null) {
            lineRenderer = GetComponent<LineRenderer>();
        }
    }

    void Update()
    {
        if (observerTransform == null) {
            return;
        }

        float distance = (transform.position - observerTransform.position).magnitude;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
        float ratio = (distance - minDistance) / (maxDistance - minDistance);

        float size = Mathf.Lerp(minSize, maxSize, ratio);

        lineRenderer.startWidth = size;
        lineRenderer.endWidth = size;
    }
}