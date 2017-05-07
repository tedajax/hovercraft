using System;
using UnityEngine;

[Serializable]
public class AirbrakeData
{
    public float brakingDrag;
    public float unbrakedDrag;

    public float brakeSpeed;
}

public class Airbrake : Part
{
    public InputBinding brakeBinding;

    public AirbrakeData data;

    private float brakeDrag;
    private float brakePower;
    private float brakeVelocity;

    public override float CurrentForce
    {
        get
        {
            return brakeDrag;
        }
    }

    public override float ForcePercentage
    {
        get
        {
            return brakePower;
        }
    }

    public override void SetData(object data)
    {
        if (data is AirbrakeData) {
            data = data as AirbrakeData;
        }
    }

    public override void ApplyForces(Rigidbody rigidbody)
    {
        rigidbody.drag = brakeDrag;
    }

    public void Update()
    {
        float targetBrakePower = 0f;
        if (brakeBinding != null) {
            brakeBinding.Update();
            targetBrakePower = brakeBinding.Value;
        }

        brakePower = Mathf.MoveTowards(brakePower, targetBrakePower, data.brakeSpeed * Time.deltaTime);
        brakeDrag = Mathf.Max(Mathf.Lerp(data.unbrakedDrag, data.brakingDrag, brakePower), 0f);
    }
}