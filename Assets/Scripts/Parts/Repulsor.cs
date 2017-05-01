using UnityEngine;
using System;

[Serializable]
public class RepulsorData
{
    public float rideHeight;
    public float forceFactor;
    public float dampingFactor;
    public float minHeight;
    public LayerMask layerMask;
}

public class Repulsor : MonoBehaviour, IPart
{
    public RepulsorData data;

    public void SetData(object data)
    {
        if (data is RepulsorData) {
            this.data = data as RepulsorData;
        }
    }

    public void ApplyForces(Rigidbody rigidbody)
    {
        Vector3 down = -transform.up;
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, down, out hitInfo, data.layerMask)) {
            Vector3 force = calculateForce(hitInfo.distance);
            rigidbody.AddForceAtPosition(force, transform.position);
        }
    }

    private Vector3 calculateForce(float height)
    {
        float force = data.forceFactor * (data.rideHeight - height);
        force += data.forceFactor * Mathf.Pow(data.minHeight / height, 2);
        float damping = -1f * data.dampingFactor * force;
        force += damping;

        return transform.TransformDirection(Vector3.up) * force;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.2f);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + -transform.up);
    }
}