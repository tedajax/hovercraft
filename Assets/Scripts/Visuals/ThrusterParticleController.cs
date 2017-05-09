using UnityEngine;

public class ThrusterParticleController : MonoBehaviour
{
    public Thruster thruster;
    public Vector3 scaleAxis;
    public float maxScale;

    private Vector3 baseScale;
    private Quaternion baseRotation;

    void Awake()
    {
        baseScale = transform.localScale;
        baseRotation = transform.localRotation;
    }

    void Update()
    {
        if (thruster == null) {
            return;
        }

        Vector3 forceScale = thruster.ForcePercentage * maxScale * scaleAxis + baseScale;
        transform.localScale = forceScale;

        var Yaw = thruster.Yaw;
        var Pitch = thruster.Pitch;

        transform.localRotation = baseRotation * Yaw * Pitch;
    }
}