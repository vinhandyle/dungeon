using System.Collections;
using UnityEngine;

/// <summary>
/// Base class for all health scripts.
/// </summary>
public abstract class Health : MonoBehaviour
{
    [Header("Debug Tools")]
    [SerializeField] protected bool invincible = false;

    [Header("Damage Indication")]
    [SerializeField] protected int damageFlashDelta;
    [SerializeField] protected float damageFlashTime;
    protected Color initialColor = new Color();

    [SerializeField] protected int defense;
    [SerializeField] protected int _health;
    [SerializeField] protected int _maxHealth;

    public int health { get { return _health; } }
    public int maxHealth { get { return _maxHealth; } }

    protected virtual void Awake()
    {
        Material material = GetComponent<SpriteRenderer>().material;
        initialColor = material.color;
    }

    /// <summary>
    /// Calls all functions that should occur after taking damage.
    /// </summary>
    protected abstract void OnDamageTakenEvent();

    /// <summary>
    /// Calls all functions that should occur after health reaches 0.
    /// </summary>
    protected abstract void OnDeathEvent();

    /// <summary>
    /// Increases the maximum health by the given amount and fully restores health.
    /// </summary>
    public void IncreaseMaxHealth(int amount)
    {
        _maxHealth += amount;
        _health = _maxHealth;
    }

    #region Take Damage

    /// <summary>
    /// Reduces health by the given value.
    /// </summary>
    public void TakeDamage(int damage)
    {
        int netDamage = damage - defense;
        _health -= netDamage > 0 ? netDamage : 1;

        OnDamageTakenEvent();

        if (_health <= 0)
        {
            OnDeathEvent();
        }
    }

    protected void SetBrightness(int delta)
    {
        Material material = transform.Find("Graphic").GetComponent<SpriteRenderer>().material;
        Color newColor = new Color(
            initialColor.r + delta > 255 ? 255 : initialColor.r + delta,
            initialColor.g + delta > 255 ? 255 : initialColor.g + delta,
            initialColor.b + delta > 255 ? 255 : initialColor.b + delta);
        material.SetColor("_Color", newColor);
    }

    protected void ResetBrightness()
    {
        Material material = transform.Find("Graphic").GetComponent<SpriteRenderer>().material;
        material.SetColor("_Color", initialColor);
    }

    protected IEnumerator DamageFlash()
    {
        SetBrightness(damageFlashDelta);
        yield return new WaitForSeconds(damageFlashTime);
        ResetBrightness();
    }

    #endregion

    #region Restore Health

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

    #endregion
}
