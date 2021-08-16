using System;
using UnityEngine;

/// <summary>
/// Base class for any tool that the player can use.
/// </summary>
public abstract class Tool : Item
{
    [Header("Tool Components")]
    [SerializeField] protected GameObject hitbox = null;
    [SerializeField] protected GameObject sprite = null;

    [Header("Tool Stats")]
    [SerializeField] protected int damage;
    [SerializeField] protected float knockback;
    [SerializeField] protected float range;  
    [SerializeField] protected float useSpeed;
    [SerializeField] protected float travelSpeed;

    protected event Action OnUse = null;

    public bool inUse = false;

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
}
