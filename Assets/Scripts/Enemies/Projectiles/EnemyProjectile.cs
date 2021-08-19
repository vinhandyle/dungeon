using System;
using UnityEngine;

/// <summary>
/// Base class for enemy projectiles.
/// </summary>
public abstract class EnemyProjectile : Projectile
{
    protected Player player = null;
    protected PlayerHealth playerHealth = null;
    protected event Action<Collider2D> OnHitPlayer = null;

    protected const string playerTag = "Player";

    protected override void Awake()
    {
        base.Awake();

        OnHitPlayer += (Collider2D collision) => 
        {
            if (player == null)
            {
                player = collision.gameObject.GetComponentInParent<Player>();
                playerHealth = player.GetComponent<PlayerHealth>();
            }            
        };
    }

    /// <summary>
    /// Triggers any effects that should follow after the projectile has hit the player.
    /// </summary>
    /// <param name="collision">The 2D collider of the object being collided into.</param>
    protected void HitPlayer(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            OnHitPlayer?.Invoke(collision);
        }
    }
}