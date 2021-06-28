using UnityEngine;

/// <summary>
/// Base class for all projectiles.
/// </summary>
public abstract class Projectile : MonoBehaviour
{
    [Header("Generic Projectile Info")]
    [SerializeField] protected float speed = 0;
    [SerializeField] protected int damage = 0;
    [SerializeField] protected bool destroyOnHit = false;

    protected PlayerHealth player = null;

    protected const string playerTag = "Player";

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
        }
    }
}
