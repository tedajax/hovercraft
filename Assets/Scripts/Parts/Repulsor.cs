﻿using UnityEngine;
using System;

[Serializable]
public class RepulsorData
{
    public float rideHeight;
    public float forceFactor;
    public float dampingFactor;
    public float dampLinearVel;
    public float dampQuadVel;
    public float minHeight;
    public LayerMask layerMask;
}

public class Repulsor : MonoBehaviour, IPart
{
    public RepulsorData data;

    private Vector3 velocity;

    public void SetData(object data)
    {
        if (data is RepulsorData) {
            this.data = data as RepulsorData;
        }
    }

    public void ApplyForces(Rigidbody rigidbody)
    {
        velocity = rigidbody.velocity;
        Vector3 down = -transform.up;
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, down, out hitInfo, data.layerMask)) {
            Vector3 force = calculateForce(hitInfo.distance);
            rigidbody.AddForceAtPosition(force, transform.position);
        }
    }

    private Vector3 calculateForce(float height)
    {
        float force = Mathf.Max(data.forceFactor * (data.rideHeight - height), 0f) +
            data.dampingFactor * (1 / (height * height)) -
            data.dampLinearVel * velocity.y -
            data.dampQuadVel * (velocity.y * velocity.y);

        return transform.up * Mathf.Max(force, 0f);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.2f);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + -transform.up);
    }
}