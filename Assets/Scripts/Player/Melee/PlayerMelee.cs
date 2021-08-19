using System;
using UnityEngine;

/// <summary>
/// Base class for all player melee attacks.
/// </summary>
public abstract class PlayerMelee : Melee
{
    protected event Action<Collider2D> OnHitEnemy = null;

    protected const int enemyLayer = 9;

    protected void HitEnemy(Collider2D collision)
    {
        if (collision.gameObject.layer == enemyLayer)
        {
            OnHitEnemy?.Invoke(collision);
        }
    }
}
