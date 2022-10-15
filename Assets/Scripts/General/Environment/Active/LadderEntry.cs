using UnityEngine;

/// <summary>
/// Represents an entry point to a ladder.
/// </summary>
public class LadderEntry : Interactable
{
    [SerializeField] private GameObject entryPoint = null;

    private PlayerController pMovement = null;

    protected override void Awake()
    {
        base.Awake();

        OnInteract += ClimbLadder;

        OnEnterRange += (collision) =>
        {
            pMovement = player.gameObject.GetComponent<PlayerController>();
        };
    }

    /// <summary>
    /// Puts the player on the ladder and into climbing mode.
    /// </summary>
    private void ClimbLadder(Player player)
    {
        Vector3 entryPos = entryPoint.transform.position;

        /*if (!pMovement.climbing)
        {
            pMovement.ResetMoveDirection();
            pMovement.SetClimbing(true);
            player.transform.position = new Vector3(entryPos.x, entryPos.y, player.transform.position.z);           
        }  */     
    }
}
