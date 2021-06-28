/// <summary>
/// Represents warp points that the player can be teleported to or from after an interaction.
/// </summary>
public class Teleporter : WarpPoint
{
    protected override void Awake()
    {
        base.Awake();

        OnInteract += TeleportPlayer;
    }
}
