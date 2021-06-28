using System.Collections;
using UnityEngine;

/// <summary>
/// Base class for all health scripts.
/// </summary>
public abstract class Health : MonoBehaviour
{
    [SerializeField] protected int _health = 0;
    public int health { get { return _health; } }

    [SerializeField] protected int _maxHealth = 0;
    public int maxHealth { get { return _maxHealth; } }

    /// <summary>
    /// The flat amount of damage reduced.
    /// </summary>
    [SerializeField] protected int defense = 0;

    /// <summary>
    /// Calls all functions that should occur after taking damage.
    /// </summary>
    protected abstract void DamageTakenEvent();

    /// <summary>
    /// Calls all functions that should occur after health reaches 0.
    /// </summary>
    protected abstract void DeathEvent();

    /// <summary>
    /// Reduces health by the given value.
    /// </summary>
    public void TakeDamage(int damage)
    {
        int netDamage = damage - defense;
        _health -= netDamage > 0 ? netDamage : 1;

        DamageTakenEvent();

        if (_health <= 0)
        {
            DeathEvent();
        }
    }

    /// <summary>
    /// Restores the given amount of health, adjusted to not exceed the maximum health.
    /// </summary>
    public virtual void Heal(int amount)
    {
        _health += _health + amount > _maxHealth ? _maxHealth - _health : amount;
    }

    /// <summary>
    /// Sets health to max.
    /// </summary>
    public virtual void FullHeal()
    {
        _health = _maxHealth;
    }

    /// <summary>
    /// Increases the maximum health by the given amount and fully restores health.
    /// </summary>
    public void IncreaseMaxHealth(int amount)
    {
        _maxHealth += amount;
        _health = _maxHealth;
    }

    /// <summary>
    /// Restores health over time.
    /// </summary>
    /// <param name="healPerTick">The amount of health restored at a time.</param>
    /// <param name="numberOfTicks">The number of times healing is triggered.</param>
    /// <param name="timeBetweenTicks">The amount of time between each healing.</param>
    /// <returns></returns>
    protected IEnumerator RegenHealth(int healPerTick, int numberOfTicks, float timeBetweenTicks)
    {
        yield return new WaitForSeconds(timeBetweenTicks);
        if (numberOfTicks > 0)
        {
            Heal(healPerTick);
            StartCoroutine(RegenHealth(healPerTick, numberOfTicks - 1, timeBetweenTicks));
        }
    }
}
