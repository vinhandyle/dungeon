using UnityEngine;

/// <summary>
/// Represents the player's health.
/// </summary>
public class PlayerHealth : Health
{
    [SerializeField] private int _usableHealth = 0;
    /// <summary>
    /// How much of the maximum health is usable.
    /// </summary>
    public int usableHealth { get { return _usableHealth; } }

    [HideInInspector] public Respawner respawnPoint;

    protected override void Awake()
    {
        base.Awake();
        _usableHealth = _maxHealth;
    }

    protected override void OnDamageTakenEvent()
    {
        StartCoroutine(DamageFlash());

        if (invincible) FullHeal();
    }

    protected override void OnDeathEvent()
    {
        respawnPoint.RespawnPlayer(this);
        FullHeal();
    }

    #region Recover Health

    /// <summary>
    /// Restores the given amount of health, adjusted to not exceed the maximum usable health.
    /// </summary>
    public override void Heal(int amount)
    {
        _health += _health + amount > _usableHealth ? _usableHealth - _health : amount;
    }

    /// <summary>
    /// Sets health to the usable max.
    /// </summary>
    public override void FullHeal()
    {
        _health = _usableHealth;
    }

    /// <summary>
    /// Sets usable health to the max. Health is not changed.
    /// </summary>
    public void ResetUsableHealth()
    {
        _usableHealth = _maxHealth;
    }

    #endregion
}
