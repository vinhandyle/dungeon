using UnityEngine;

/// <summary>
/// Test enemy melee attack.
/// </summary>
public class Stick : EnemyMelee
{
    [HideInInspector] public float swingSpeed = 0;
    [HideInInspector] public bool swingingForward = false;

    private const int quarterCircle = 45;

    protected override void Awake()
    {
        base.Awake();

        OnHitPlayer += (Collider2D collision) =>
        {
            playerHealth.TakeDamage(damage);
            player.Knockback(knockbackAmount, knockbackDuration, knockbackType, transform.position);
        };
    }

    private void Update()
    {
        Swing();
    }

    /// <summary>
    /// Rotates the stick.
    /// </summary>
    public void Swing()
    {
        if (swingingForward)
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * quarterCircle * swingSpeed);
        }        
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        HitPlayer(collision);
    }
}
