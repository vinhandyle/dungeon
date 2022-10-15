using System;
using UnityEngine;

/// <summary>
/// Base class for all attacks.
/// </summary>
public abstract class Attack : MonoBehaviour
{
    [Header("Base Info")]
    [SerializeField] protected Transform origin = null;
    [SerializeField] protected int damage = 0;
    [SerializeField] protected int healAmt;

    [Header("Block and Parry")]
    [SerializeField] protected int stabilityDamage = 0;
    [SerializeField] protected bool blockable = true;
    [SerializeField] protected bool parryable = true;
    public bool parried = false;

    [Header("Knockback")]
    [SerializeField] protected float knockbackAmount = 0;
    [SerializeField] protected float knockbackDuration = 0;
    [SerializeField] protected int knockbackType = 0;

    protected event Action OnHit;

    /// <summary>
    /// Sets the origin of this attack to the given object.
    /// </summary>
    /// <param name="origin">The object that this attack came from.</param>
    public void SetOrigin(Transform origin)
    {
        this.origin = origin;
    }
}
