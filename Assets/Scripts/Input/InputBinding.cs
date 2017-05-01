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

        switch (inputBindingType) {
            case InputBindingType.Button:
                if (Input.GetButton(inputName)) {
                    return (invert) ? 0f : 1f;
                }
                else {
                    return (invert) ? 1f : 0f;
                }

            case InputBindingType.Axis:
                float axis = Input.GetAxis(inputName);
                return (invert) ? -axis : axis;

            default: return 0f;
        }
    }
}