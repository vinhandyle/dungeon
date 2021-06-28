using UnityEngine;

/// <summary>
/// Represents the player's health.
/// </summary>
public class PlayerHealth : Health
{
    [SerializeField] private PlayerHealthUI ui = null;

    [SerializeField] private int _usableHealth = 0;
    /// <summary>
    /// How much of the maximum health is usable.
    /// </summary>
    public int usableHealth { get { return _usableHealth; } }

    [Header("Debug Tools")]
    [SerializeField] private bool invincible = false;

    [HideInInspector] public Respawner respawnPoint = null;

    private void Awake()
    {
        _usableHealth = _maxHealth;
        ui.UpdateHealthUI();
    }

    protected override void DamageTakenEvent()
    {
        if (invincible)
            FullHeal();

        ui.UpdateHealthUI();
    }

    protected override void DeathEvent()
    {
        respawnPoint.RespawnPlayer(this);
        FullHeal();
    }

    /// <summary>
    /// Restores the given amount of health, adjusted to not exceed the maximum usable health.
    /// </summary>
    public override void Heal(int amount)
    {
        _health += _health + amount > _usableHealth ? _usableHealth - _health : amount;
        ui.UpdateHealthUI();
    }

    /// <summary>
    /// Sets health to the usable max.
    /// </summary>
    public override void FullHeal()
    {
        _health = _usableHealth;
        ui.UpdateHealthUI();
    }

    /// <summary>
    /// Sets usable health to the max. Health is not changed.
    /// </summary>
    public void ResetUsableHealth()
    {
        _usableHealth = _maxHealth;
        ui.UpdateHealthUI();
    }
}
