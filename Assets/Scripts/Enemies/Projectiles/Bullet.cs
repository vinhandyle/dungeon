using UnityEngine;

/// <summary>
/// Test enemy projectile.
/// </summary>
public class Bullet : EnemyProjectile
{
    protected override void Awake()
    {
        base.Awake();

        OnHitPlayer += (Collider2D collision) =>
        {
            playerHealth.TakeDamage(damage);
            player.Knockback(knockbackAmount, knockbackDuration, knockbackType, origin.transform.position);
        };
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        HitPlayer(collision);
        base.OnTriggerEnter2D(collision);
    }   
}
