using System;
using UnityEngine;

/// <summary>
/// Base class for all attacks.
/// </summary>
public abstract class Attack : MonoBehaviour
{
    [Header("General Attack Info")]
    [SerializeField] protected GameObject origin = null;
    [SerializeField] protected int damage = 0;
    [SerializeField] protected int stabilityDamage = 0;
    [SerializeField] protected bool blockable = true;
    [SerializeField] protected bool parryable = true;
    public bool parried = false;

    /// <summary>
    /// Sets the origin of this attack to the given object.
    /// </summary>
    /// <param name="origin">The object that this attack came from.</param>
    public void SetOrigin(GameObject origin)
    {
        this.origin = origin;
    }
}
