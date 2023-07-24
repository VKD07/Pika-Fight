using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Float", menuName = "References/FloatReference")]
public class FloatReference : ScriptableObject
{
    [SerializeField] float value;

    public float Value
    {
        get { return value; }
        set { this.value = value; }
    }

    public void AddValue(float value)
    {
        this.value += value;
    }
}
