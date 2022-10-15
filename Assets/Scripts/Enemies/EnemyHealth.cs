using System.Collections;
using UnityEngine;

/// <summary>
/// Represents any enemy's health.
/// </summary>
public class EnemyHealth : Health
{
    [Tooltip("Use for multi-segment enemies.")]
    [SerializeField] protected EnemyHealth mainHealth;

    protected override void OnDamageTakenEvent()
    {
        StartCoroutine(DamageFlash());
    }

    protected override void OnDeathEvent()
    {
        Destroy(gameObject);
    }
}
