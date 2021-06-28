using UnityEngine;

/// <summary>
/// Test projectile.
/// </summary>
public class Bullet : Projectile
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            player = collision.gameObject.GetComponentInParent<PlayerHealth>();
            player.TakeDamage(damage);
        }

        base.OnTriggerEnter2D(collision);
    }
}
