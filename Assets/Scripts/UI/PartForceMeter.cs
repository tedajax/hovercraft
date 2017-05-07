using UnityEngine;
using UnityEngine.UI;

public class PartForceMeter : MonoBehaviour
{
    public Image barImage;

    public Part part;

    void Awake()
    {
        if (barImage == null) {
            Debug.LogWarning("Unable to find barImage on PartForceMeter '" + gameObject.name + "'.");
            Destroy(this);
        }
    }

    void Update()
    {
        if (part == null) {
            barImage.fillAmount = 0f;
            return;
        }

        barImage.fillAmount = Mathf.Clamp01(part.ForcePercentage);
    }
}