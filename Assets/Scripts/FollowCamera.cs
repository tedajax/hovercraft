using System;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform followTransform;
    public float followDistance;
    public float followHeight;
    public float lookAheadDistance = 2f;
    public float positionSmoothFactor = 0.25f;
    public float rotationSmoothFactor = 0.25f;

    private Vector3 velocity;

    public void Update()
    {
        if (followTransform == null) {
            return;
        }

        Vector3 targetPosition = followTransform.position + -followTransform.forward * followDistance + Vector3.up * followHeight;

        Vector3 position = transform.position;
        position = Vector3.SmoothDamp(position, targetPosition, ref velocity, positionSmoothFactor);
        transform.position = position;

        Vector3 targetViewPosition = followTransform.position + followTransform.forward * lookAheadDistance;
        Quaternion targetRotation = Quaternion.LookRotation(targetViewPosition - transform.position, Vector3.up);

        Quaternion rotation = transform.rotation;
        rotation = Quaternion.Slerp(rotation, targetRotation, rotationSmoothFactor * Time.deltaTime);
        transform.rotation = rotation;
    }
}