using UnityEngine;

/// <summary>
/// Represents an interactable object that can teleport the player somewhere.
/// </summary>
public abstract class WarpPoint : Interactable
{
    [SerializeField] protected GameObject destination = null;
    [SerializeField] protected PlayerCamera cam = null;

    /// <summary>
    /// Sets the player's position to that of the WarpPoint's destination.
    /// </summary>
    protected virtual void TeleportPlayer(Player player)
    {     
        Vector3 destPos = destination.transform.position;
        player.transform.position = new Vector3(destPos.x, destPos.y, player.transform.position.z);
        cam.TeleportCamera();
    }
}
