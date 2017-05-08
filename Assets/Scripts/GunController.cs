using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GunData
{
    public Transform bulletSpawnTransform;
    public float fireInterval;
    public float spinUpTime;
    public float spinRate;
    public Transform gunTurretTransform;
    public Vector3 rotationAxis;
    public Transform muzzleFlashTransform;
    public GameObject muzzleFlashPrefab;
    public GameObject bulletPrefab;
}

public class GunController : MonoBehaviour
{
    public GunData data;
    public InputBinding fireBinding;

    private float fireTimer;
    private float spinRate;

    private float SpinAcceleration
    {
        get
        {
            return data.spinRate / data.spinUpTime;
        }
    }

    public void Awake()
    {
    }

    void Update()
    {
        if (fireBinding != null) {
            fireBinding.Update();
        }

        if (fireBinding.Value > 0f) {
            if (spinRate < data.spinRate) {
                spinRate += SpinAcceleration * Time.deltaTime;
                spinRate = Mathf.Min(spinRate, data.spinRate);
            }
            else {
                fireTimer -= Time.deltaTime;

                if (fireTimer <= 0f) {
                    fire();
                    fireTimer += data.fireInterval;
                }
            }
        }
        else {
            spinRate -= SpinAcceleration * Time.deltaTime;
            spinRate = Mathf.Max(spinRate, 0f);
        }

        if (data.gunTurretTransform != null) {
            Quaternion rotation = data.gunTurretTransform.rotation;
            rotation *= Quaternion.AngleAxis(spinRate * Time.deltaTime, data.rotationAxis);
            data.gunTurretTransform.rotation = rotation;
        }
    }

    private void fire()
    {
        var flash = Instantiate(data.muzzleFlashPrefab) as GameObject;
        flash.transform.SetParent(data.muzzleFlashTransform, false);

        var bulletObj = Instantiate(data.bulletPrefab, data.bulletSpawnTransform.position, data.bulletSpawnTransform.rotation) as GameObject;
    }
}