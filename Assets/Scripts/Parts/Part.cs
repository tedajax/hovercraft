using System;
using UnityEngine;

public abstract class Part : MonoBehaviour, IPart
{
    public abstract float CurrentForce { get; }
    public abstract float ForcePercentage { get; }

    public abstract void ApplyForces(Rigidbody rigidbody);
    public abstract void SetData(object data);
}