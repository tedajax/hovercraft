using System.Collections.Generic;
using UnityEngine;

public class HovercraftController : MonoBehaviour
{
    public IPart[] parts = new IPart[0];

    public RepulsorData repulsorData;

    private new Rigidbody rigidbody;

    public void Start()
    {
        parts = gameObject.GetComponentsInChildren<IPart>();

        var repulsors = gameObject.GetComponentsInChildren<Repulsor>();
        foreach (var repulsor in repulsors) {
            repulsor.data = repulsorData;
        }

        rigidbody = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        foreach (var part in parts) {
            part.ApplyForces(rigidbody);
        }
    }
}