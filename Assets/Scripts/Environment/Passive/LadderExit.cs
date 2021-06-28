using UnityEngine;

/// <summary>
/// Represents an exit point from a ladder.
/// </summary>
public class LadderExit : TriggerZone
{
    [SerializeField] private GameObject exitPoint = null;

    protected override void Awake()
    {
        base.Awake();

        OnPlayerTriggerEnter += (collision) =>
        {
            ExitLadder();           
        };
    }

    /// <summary>
    /// Puts the player off the ladder and out of climbing mode.
    /// </summary>
    private void ExitLadder()
    {
        GameObject player = pMovement.gameObject;
        Vector3 exitPos = exitPoint.transform.position;

        if (pMovement.climbing)
        {
            player.transform.position = new Vector3(exitPos.x, exitPos.y, player.transform.position.z);
            pMovement.climbing = false;
            pMovement.SetClimbing(false);
        }
    }
}
