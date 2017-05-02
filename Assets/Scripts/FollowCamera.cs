using System;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform followTransform;
    public float followDistance;
    public float followHeight;
    public float lookAheadDistance = 2f;
    public float smoothFactor = 0.25f;

    public void Update()
    {
        if (followTransform == null) {
            return;
        }

        Vector3 targetPosition = followTransform.position + -followTransform.forward * followDistance + Vector3.up * followHeight;

        Vector3 position = transform.position;
        position = Vector3.Lerp(position, targetPosition, smoothFactor * Time.deltaTime);
        transform.position = position;

        Vector3 targetViewPosition = followTransform.position + followTransform.forward * lookAheadDistance;
        Quaternion targetRotation = Quaternion.LookRotation(targetViewPosition - transform.position, Vector3.up);
        Quaternion rotation = transform.rotation;
        rotation = Quaternion.Slerp(rotation, targetRotation, smoothFactor * Time.deltaTime);
        transform.rotation = rotation;
    }
}