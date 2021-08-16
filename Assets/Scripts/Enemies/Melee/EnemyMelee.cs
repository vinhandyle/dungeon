using System;
using UnityEngine;

/// <summary>
/// Base class for all enemy melee attacks.
/// </summary>
public abstract class EnemyMelee : Melee
{
    protected Player player = null;
    protected PlayerHealth playerHealth = null;
    protected event Action<Collider2D> OnHitPlayer = null;

    protected const string playerTag = "Player";

    protected virtual void Awake()
    {
        OnHitPlayer += (Collider2D collision) =>
        {
            player = collision.gameObject.GetComponentInParent<Player>();
            playerHealth = player.GetComponent<PlayerHealth>();
        };
    }

    /// <summary>
    /// Triggers any effects that should follow after the attack has hit the player.
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
