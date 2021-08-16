using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the shield tool used to block and parry incoming attacks.
/// </summary>
public class Shield : Tool
{
    protected override void SetUpItem()
    {
        itemName = "Shield";
        itemDescription = "This is a shield.";
    }

    private void OnEnable()
    {
        OnUse += Block;
    }

    /// <summary>
    /// 
    /// </summary>
    private void Block()
    {

    }

    private void OnDisable()
    {
        OnUse -= Block;
    }
}
