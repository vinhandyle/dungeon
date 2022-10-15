using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents the player's health indicator.
/// </summary>
public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private PlayerHealth player = null;

    [Header("Hit Point Sprites")]
    [SerializeField] private Sprite undamaged = null;
    [SerializeField] private Sprite damaged = null;
    [SerializeField] private Sprite unusable = null;

    [Header("Hit Point Objects")]
    [SerializeField] private List<GameObject> hitPoints = null;

    private void Update()
    {
        UpdateMaxHealth();
        UpdateUsableHealth();
        UpdateHealthDamage();
    }

    /// <summary>
    /// Updates which hitpoints should be visible to match the player's maximum health.
    /// </summary>
    private void UpdateMaxHealth()
    {
        for (int i = 0; i < hitPoints.Capacity; i++)
        {
            hitPoints[i].SetActive(i < player.maxHealth);
            hitPoints[i].GetComponent<Image>().sprite = undamaged;
        }
    }

    /// <summary>
    /// Updates which of the visible hitpoints should be "disabled" to match the player's
    /// maximum usable health.
    /// </summary>
    private void UpdateUsableHealth()
    {
        for (int i = hitPoints.Capacity - 1; i >= player.usableHealth; i--)
        {
            hitPoints[i].GetComponent<Image>().sprite = unusable;
        }
    }

    /// <summary>
    /// Updates which of the enabled hitpoints should be "damaged" to match the player's health.
    /// </summary>
    private void UpdateHealthDamage()
    {
        for (int i = player.usableHealth - 1; i >= player.health; i--)
        {
            hitPoints[i].GetComponent<Image>().sprite = damaged;
        }
    }
}
