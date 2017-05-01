using System;
using UnityEngine;

[Serializable]
public class AirbrakeData
{
    public float brakingDrag;
    public float unbrakedDrag;
}

public class Airbrake : MonoBehaviour, IPart
{
    public InputBinding brakeBinding;

    public AirbrakeData data;

    private float brakePower;

    public void SetData(object data)
    {
        if (data is AirbrakeData) {
            data = data as AirbrakeData;
        }
    }

    public void ApplyForces(Rigidbody rigidbody)
    {
        rigidbody.drag = brakePower;
    }

    public void Update()
    {
        float brakeScalar = 0f;
        if (brakeBinding != null) {
            brakeBinding.Update();
            brakeScalar = brakeBinding.Value;
        }

        brakePower = Mathf.Max(Mathf.Lerp(data.unbrakedDrag, data.brakingDrag, brakeScalar), 0f);
    }
}