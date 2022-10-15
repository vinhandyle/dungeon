using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the flask of water tool use to restore the player's health.
/// </summary>
public class WaterFlask : Flask
{
    protected override void SetUpItem()
    {
        itemName = "Water Flask";
        itemDescription = "This is a flask of water.";
    }

    /// <summary>
    /// Restores the player's health by 1.
    /// </summary>
    protected override void Drink()
    {
        base.Drink();

        if (charges > 0)
        {
            playerHealth.Heal(1);
            charges--;
        }       
    }
}
