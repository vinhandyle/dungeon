/// <summary>
/// Represents any side-facing surfacing, usually perpendicular to the ground.
/// </summary>
public class Wall : TriggerZone
{
    protected override void Awake()
    {
        base.Awake();

        OnPlayerCollideEnter += (collision) =>
        {
            pMovement.canMoveLeft = pMovement.facingLeft ? false : true;
            pMovement.canMoveRight = !pMovement.facingLeft ? false : true;
        };

        OnPlayerCollideExit += (collision) =>
        {
            pMovement.canMoveLeft = true;
            pMovement.canMoveRight = true;
        };
    }    
}
