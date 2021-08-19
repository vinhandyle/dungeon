using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the spear as an attack.
/// </summary>
public class SpearMelee : PlayerMelee
{
    [SerializeField] private bool onFire = false;

    private void Awake()
    {
        OnHitEnemy += (Collider2D collision) =>
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            EnemyHealth enemyHealth = enemy?.GetComponent<EnemyHealth>();

            enemyHealth?.TakeDamage(damage);
            enemy?.Knockback(knockbackAmount, knockbackDuration, knockbackType, transform.position);
        };       
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        HitEnemy(collision);

        // Add interaction when hitting fire
    }   
}
