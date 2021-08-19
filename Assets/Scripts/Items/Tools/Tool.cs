using System;
using UnityEngine;

/// <summary>
/// Base class for any tool that the player can use.
/// </summary>
public abstract class Tool : Item
{
    [Header("Player Info")]
    [SerializeField] protected Player player = null;
    [SerializeField] protected PlayerHealth playerHealth = null;
    [SerializeField] protected PlayerMovement playerMovement = null;

    [Header("Tool Components")]
    [SerializeField] protected AttackRange range = null;
    [SerializeField] protected Attack attack = null;
    [SerializeField] protected SpriteRenderer sprite = null;

    [Header("Tool Stats")]
    [SerializeField] protected float useSpeed = 0;

    [Header("Debug")]
    public bool inUse = false;
    [SerializeField] protected string toolState = "";

    protected event Action OnUse = null;
    protected event Action OnRelease = null;
    protected Vector3 initialPositionRight = Vector3.zero;

    protected const int flipped = 180;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponentInParent<Player>();
        playerHealth = GetComponentInParent<PlayerHealth>();
        playerMovement = GetComponentInParent<PlayerMovement>();
        initialPositionRight = transform.localPosition;

        range?.SetAttack(attack);
    }

    /// <summary>
    /// Sets the direction of the tool based on where the player is facing.
    /// </summary>
    protected void SetToolDirection()
    {
        if (playerMovement.facingLeft)
        {
            transform.localPosition = -initialPositionRight;

            transform.rotation = new Quaternion(
                transform.rotation.x,
                transform.rotation.y,
                180,
                transform.rotation.w
            );
        }
        else
        {
            transform.localPosition = initialPositionRight;

            transform.rotation = new Quaternion(
                transform.rotation.x,
                transform.rotation.y,
                0,
                transform.rotation.w
            );
        }
    }

    /// <summary>
    /// Uses the current tool held by the player.
    /// </summary>
    public void Use()
    {
        if (!inUse)
        {
            Debug.Log(this + " Used");
            inUse = true;
            OnUse?.Invoke();
        }
    }

    /// <summary>
    /// Releases the current tool held by the player.
    /// </summary>
    public void Release()
    {
        if (inUse)
        {
            Debug.Log(this + " Released");
            OnRelease?.Invoke();
        }
    }
}
