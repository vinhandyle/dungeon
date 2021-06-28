using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the player.
/// </summary>
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerHealth health = null;

    private float damageMultiplier = 1;

    // Tools
    private bool hasSpear, hasShield, hasBomb;

    /// <summary>
    /// Increases the player's health by 1.
    /// </summary>
    public void HealthUp()
    {
        health.IncreaseMaxHealth(1);
    }
}
