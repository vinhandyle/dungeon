using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Use for all melee attacks.
/// </summary>
public class Melee : Attack
{
    private BoxCollider2D hitbox;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private List<string> targetTags;

    [Header("Attack Phase Visualizer")]
    [SerializeField] private List<Sprite> sprites;

    public bool inProcess;

    private void Awake()
    {
        hitbox = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        hitbox.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (targetTags.Any(tag => collision.CompareTag(tag)))
        {
            Health target = collision.GetComponent<Health>();
            target.TakeDamage(damage);
            GetComponentInParent<Health>().Heal(healAmt);
        }
    }

    /// <summary>
    /// The segment before the attack lands.
    /// </summary>
    public void Foreswing()
    {
        inProcess = true;
        //spriteRenderer.sprite = sprites[0]; // Comment out if not testing
    }

    /// <summary>
    /// The segment during which the attack deals damage.
    /// </summary>
    public void Hit()
    {
        hitbox.enabled = true;
        //spriteRenderer.sprite = sprites[1]; // Comment out if not testing
    }

    /// <summary>
    /// The segment after the attack but before control is returned to the player.
    /// </summary>
    public void Backswing()
    {
        hitbox.enabled = false;
        //spriteRenderer.sprite = sprites[2]; // Comment out if not testing
    }

    /// <summary>
    /// Return control back to the player.
    /// </summary>
    public void Finish()
    {
        spriteRenderer.sprite = null;
        inProcess = false;
    }
}
