using UnityEngine;

public class Thruster : Part
{
    public InputBinding throttleBinding;
    public InputBinding reverseBinding;
    public InputBinding horizontalVectorBinding;
    public InputBinding verticalVectorBinding;
    public InputBinding boostBinding;

    public float force = 5f;
    public float boostMultiplier = 1f;
    public float vectorRange = 0f;

    public override float CurrentForce
    {
        get
        {
            return calculateThrottle() * force;
        }
    }

    public override float ForcePercentage
    {
        get
        {
            return CurrentForce / force;
        }
    }

    public Quaternion Yaw { get; set; }
    public Quaternion Pitch { get; set; }

    public override void SetData(object data) { }

    public override void ApplyForces(Rigidbody rigidbody)
    {
        float throttle = calculateThrottle();
        Vector3 forceVec = calculateThrustDirection() * throttle * force;

        rigidbody.AddForceAtPosition(forceVec, transform.position);
    }

    public void Update()
    {
        throttleBinding.Update();
        reverseBinding.Update();
        horizontalVectorBinding.Update();
        verticalVectorBinding.Update();
        boostBinding.Update();
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

        float boost = 1f;
        if (boostBinding != null && boostBinding.IsPressed) {
            boost = boostBinding.Value * boostMultiplier;
        }

        return throttle * boost - reverse;
    }

    private Vector3 calculateThrustDirection()
    {
        float horiz = 0f, vert = 0f;

        if (horizontalVectorBinding != null) {
            horiz = horizontalVectorBinding.Value;
        }

        if (verticalVectorBinding != null) {
            vert = verticalVectorBinding.Value;
        }

        Yaw = Quaternion.AngleAxis(horiz * vectorRange, Vector3.up);
        Pitch = Quaternion.AngleAxis(vert * vectorRange, Vector3.right);

        return Yaw * Pitch * transform.forward;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 0.05f);

        Gizmos.color = new Color(1.0f, 0.5f, 0.0f);
        Vector3 forceDir = calculateThrustDirection();
        Gizmos.DrawLine(transform.position, transform.position + -forceDir * 0.25f);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + -forceDir * calculateThrottle());
    }
}