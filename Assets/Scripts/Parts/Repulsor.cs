using UnityEngine;
using System;

[Serializable]
public class RepulsorData
{
    public float rideHeight;
    public float maximumForce;
    public float forceFactor;
    public float dampingFactor;
    public float dampLinearVel;
    public float dampQuadVel;
    public float minHeight;
    public LayerMask layerMask;
    public InputBinding heightAdjustmentBinding;
    public float adjustableRideHeight;
}

public class Repulsor : Part
{
    public RepulsorData data;

    private Vector3 velocity;
    private Vector3 forceVector;
    private float adjustedHeight = 0f;

    public override float CurrentForce
    {
        get
        {
            return forceVector.magnitude;
        }
    }

    public override float ForcePercentage
    {
        get
        {
            return CurrentForce / data.maximumForce;
        }
    }

    public override void SetData(object data)
    {
        if (data is RepulsorData) {
            this.data = data as RepulsorData;
        }
    }

    public override void ApplyForces(Rigidbody rigidbody)
    {
        velocity = rigidbody.velocity;
        Vector3 down = -transform.up;
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, down, out hitInfo, data.layerMask)) {
            forceVector = calculateForce(hitInfo.distance);
            rigidbody.AddForceAtPosition(forceVector, transform.position);
        }
    }

    void Update()
    {
        if (data.heightAdjustmentBinding != null) {
            data.heightAdjustmentBinding.Update();
            adjustedHeight = data.heightAdjustmentBinding.Value * data.adjustableRideHeight;
        }
    }

    private Vector3 calculateForce(float height)
    {
        float rideHeight = data.rideHeight + adjustedHeight;

        float force = Mathf.Max(data.forceFactor * (rideHeight - height), 0f) +
            data.dampingFactor * (1 / (height * height)) -
            data.dampLinearVel * velocity.y -
            data.dampQuadVel * (velocity.y * velocity.y);

        return transform.up * Mathf.Clamp(force, 0f, (data.maximumForce > 0f) ? data.maximumForce : Mathf.Infinity);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.2f);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + -transform.up);
    }
}