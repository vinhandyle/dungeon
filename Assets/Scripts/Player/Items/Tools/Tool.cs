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

    [Header("Tool Components")]
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
        initialPositionRight = transform.localPosition;

    }

    /// <summary>
    /// Sets the direction of the tool based on where the player is facing.
    /// </summary>
    protected void SetToolDirection()
    {
        transform.localScale.Set(transform.localScale.x, transform.localScale.y, player.transform.localScale.z);
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
