using UnityEngine;

/// <summary>
/// Represents the range of an attack.
/// </summary>
public class AttackRange : Range
{
    // For the attack and attack range to collide with each other,
    // add a rigidbody on the attack but not on the attack range.

    [SerializeField] private Attack attack = null;

    /// <summary>
    /// Sets the corresponding attack of this attack range.
    /// </summary>
    /// <param name="attack">The attack that this range is for.</param>
    public void SetAttack(Attack attack)
    {
        this.attack = attack;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.GetComponent<Attack>() == attack)
        {
            Enter();
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);

        if (collision.GetComponent<Attack>() == attack)
        {
            Exit();
        }
    }
}
