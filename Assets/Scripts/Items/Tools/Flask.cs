using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Flask : Tool
{
    private void OnEnable()
    {
        OnUse += Drink;
    }

    /// <summary>
    /// 
    /// </summary>
    protected virtual void Drink()
    {

    }

    private void OnDisable()
    {
        OnUse -= Drink;
    }
}
