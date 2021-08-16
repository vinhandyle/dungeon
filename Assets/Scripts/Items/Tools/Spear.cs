using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the spear tool used to launch frontal attacks. 
/// </summary>
public class Spear : Tool
{
    protected override void SetUpItem()
    {
        itemName = "Spear";
        itemDescription = "This is a spear.";
    }

    private void OnEnable()
    {
        OnUse += Thrust;
    }

    /// <summary>
    /// The spear travels forward to its maximum range relative to the player,
    /// dealing damage and inflicting weak knockback to enemies in the way.
    /// </summary>
    private void Thrust()
    {
        gameObject.transform.localPosition += Vector3.zero;
    }

    /// <summary>
    /// The spear travels back to its initial state relative to the player.
    /// </summary>
    private void Return()
    {

    }

    private void OnDisable()
    {
        OnUse -= Thrust;
    }
}
