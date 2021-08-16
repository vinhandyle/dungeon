using System;
using UnityEngine;

/// <summary>
/// Base class for all projectiles.
/// </summary>
public abstract class Projectile : MonoBehaviour
{
    [Header("Generic Projectile Info")]
    [SerializeField] protected int damage = 0;
    [SerializeField] protected float speed = 0;

    [SerializeField] protected float knockbackAmount = 0;
    [SerializeField] protected float knockbackDuration = 0;
    [SerializeField] protected int knockbackType = 0;

    [SerializeField] protected bool destroyOnHit = false;

    protected event Action OnHit = null;

    /// <summary>
    /// Performs any setup needed when the projectile is created.
    /// </summary>
    protected virtual void Awake()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * speed;
    }

    /// <summary>
    /// Performs any cleanup or triggers any after-effects when the projectile collides with something.
    /// </summary>
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (destroyOnHit)
        {
            Destroy(gameObject);
            OnHit?.Invoke();
        }
    }
}
