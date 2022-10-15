using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all variants of the flask tool.
/// </summary>
public abstract class Flask : Tool
{
    [SerializeField] protected int charges = 0;
    [SerializeField] protected float drinkingTime = 0;

    protected override void Awake()
    {
        base.Awake();

        OnUse += Drink;
    }

    protected virtual void Drink()
    {
        player.Knockback(0, drinkingTime, 0, Vector3.zero);
    }

    protected virtual void Finish()
    {
        inUse = false;
    }
}
