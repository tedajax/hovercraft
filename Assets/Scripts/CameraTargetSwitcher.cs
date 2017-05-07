using UnityEngine;

public class CameraTargetSwitcher : MonoBehaviour
{
    public FollowCamera followCamera;
    public UnityStandardAssets.ImageEffects.DepthOfField depthOfField;

    public Transform[] transforms;
    public int transformIndex;
    public KeyCode switchKey;

    void Awake()
    {
        if (transforms.Length == 0) {
            Destroy(this);
        }

        SetTransform(transforms[transformIndex]);
    }

    void Update()
    {
        if (Input.GetKeyDown(switchKey)) {
            transformIndex++;
            transformIndex %= transforms.Length;
            SetTransform(transforms[transformIndex]);
        }
    }

    public void SetTransform(Transform transform)
    {
        followCamera.followTransform = transform;
        depthOfField.focalTransform = transform;
    }
}