using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FloatReference
{
    public bool UseConstant = true;
    public float ConstantVariable;
    public FloatVariable Variable;

    public float Value
    {
        get {return UseConstant ? ConstantVariable : Variable.Value;}
    }
}
