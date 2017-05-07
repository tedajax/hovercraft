using System;
using UnityEngine;

public interface IPart
{
    void ApplyForces(Rigidbody rigidbody);
    void SetData(object data);
    float CurrentForce { get; }
    float ForcePercentage { get; }
}