using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for objects that can attack or be attacked.
/// </summary>
public abstract class Entity : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected float knockbackResistance;

    [Header("Status Effects")]
    public bool stunned;

    /// <summary>
    /// Flips the entity across the y-axis;
    /// </summary>
    public virtual void Flip()
    {
        Vector3 oppDirection = transform.localScale;
        oppDirection.x *= -1;
        transform.localScale = oppDirection;
    }

    /// <summary>
    /// Moves the object by the given amount in a certain way relative to the given origin.
    /// </summary>
    /// <param name="amount">How much the object will be pushed from the origin.</param>
    /// <param name="duration">How long before the object can recover.</param>
    /// <param name="type"><para>How knockback should be applied. </para>
    ///                     0 = omni-directional, 1 = horizontal only, 2 = vertical only.</param>
    /// <param name="origin">The location of the knockback epicenter.</param>
    public virtual void Knockback(float amount, float duration, int type, Vector3 origin)
    {
        float netAmount = amount * (1 - knockbackResistance);
        Vector2 difference = (transform.position - origin).normalized;

        StartCoroutine(Stun(duration));

        switch (type)
        {
            case 0:
                GetComponent<Rigidbody2D>().velocity = difference * amount;
                break;

            case 1:
                GetComponent<Rigidbody2D>().velocity = 
                    new Vector2(difference.x * amount, GetComponent<Rigidbody2D>().velocity.y);
                break;

            case 2:
                GetComponent<Rigidbody2D>().velocity =
                    new Vector2(GetComponent<Rigidbody2D>().velocity.x, difference.y * amount);
                break;
        }
    }

    /// <summary>
    /// Inflicts the stunned effect on this entity for the given duration.
    /// </summary>
    /// <param name="duration">How long the stun will last.</param>
    IEnumerator Stun(float duration)
    {
        stunned = true;

        yield return new WaitForSeconds(duration);

        stunned = false;
    }
}
