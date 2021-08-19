using System;
using UnityEngine;

/// <summary>
/// Base class for all melee attacks.
/// </summary>
public abstract class Melee : Attack
{
    [Header("Generic Melee Info")]
    [SerializeField] protected float knockbackAmount = 0;
    [SerializeField] protected float knockbackDuration = 0;
    [SerializeField] protected int knockbackType = 0;   

    protected event Action OnHit = null;

    protected abstract void OnTriggerEnter2D(Collider2D collision);    
}
