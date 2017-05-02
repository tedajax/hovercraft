using System;
using UnityEngine;

public enum InputBindingType
{
    Button,
    Axis,
}

[Serializable]
public class InputBinding
{
    public InputBindingType inputBindingType;
    public string inputName;
    public bool invert;
    public float min = -1f;
    public float max = 1f;

    public float Value { get; private set; }


    public void Update()
    {
        Value = evaluate();
    }

    private float evaluate()
    {
        if (string.IsNullOrEmpty(inputName)) {
            return 0f;
        }

        float value = 0f;

        switch (inputBindingType) {
            case InputBindingType.Button:
                if (Input.GetButton(inputName)) {
                    value = (invert) ? 0f : 1f;
                }
                else {
                    value = (invert) ? 1f : 0f;
                }
                break;

            case InputBindingType.Axis:
                float axis = Input.GetAxis(inputName);
                value = (invert) ? -axis : axis;
                break;
        }

        return Mathf.Clamp(value, min, max);
    }
}