using UnityEngine;

public class Thruster : MonoBehaviour, IPart
{
    public InputBinding throttleBinding;
    public InputBinding reverseBinding;

    public float force = 5f;

    public void SetData(object data) { }

    public void ApplyForces(Rigidbody rigidbody)
    {
        float throttle = calculateThrottle();
        Vector3 forceVec = transform.forward * throttle * force;

        rigidbody.AddForceAtPosition(forceVec, transform.position);
    }

    public void Update()
    {
        throttleBinding.Update();
        reverseBinding.Update();
    }

    private float calculateThrottle()
    {
        float throttle = 0f;
        if (throttleBinding != null) {
            throttle = throttleBinding.Value;
        }

        float reverse = 0f;
        if (reverseBinding != null) {
            reverse = reverseBinding.Value;
        }

        return throttle - reverse;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 0.05f);

        Gizmos.color = new Color(1.0f, 0.5f, 0.0f);
        Vector3 forceDir = transform.forward;
        Gizmos.DrawLine(transform.position, transform.position + -forceDir * 0.25f);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + -forceDir * calculateThrottle());
    }
}