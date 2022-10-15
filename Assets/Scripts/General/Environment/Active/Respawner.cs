/// <summary>
/// Represents warp points that the player can be teleported to after dying.
/// </summary>
public class Respawner : WarpPoint
{
    protected override void Awake()
    {
        base.Awake();

        OnInteract += SetRespawnPoint;
    }

    /// <summary>
    /// Sets the player's respawn point to be this Respawner.
    /// </summary>
    private void SetRespawnPoint(Player player)
    {
        PlayerHealth health = player.GetComponent<PlayerHealth>();

        health.respawnPoint = this;
        health.FullHeal();
        // Restore resources
    }

    /// <summary>
    /// Sets the player's position to be that of this Respawner.
    /// </summary>
    public void RespawnPlayer(PlayerHealth player)
    {
        TeleportPlayer(player.GetComponent<Player>());
    }
}
