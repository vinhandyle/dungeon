using System;
using UnityEngine;

/// <summary>
/// Base class for all melee attacks.
/// </summary>
public abstract class Melee : MonoBehaviour
{
    [Header("Generic Melee Info")]
    [SerializeField] protected int damage = 0;

    [SerializeField] protected float knockbackAmount = 0;
    [SerializeField] protected float knockbackDuration = 0;
    [SerializeField] protected int knockbackType = 0;

    [SerializeField] protected bool parryable = true;

    protected event Action OnHit = null;

    [HideInInspector] public bool parried = false;

    protected abstract void OnTriggerEnter2D(Collider2D collision);    
}
