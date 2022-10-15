/// <summary>
/// Represents any down-facing surface.
/// </summary>
public class Ceiling : TriggerZone
{
    protected override void Awake()
    {
        base.Awake();

        /*OnPlayerTriggerEnter += (collision) =>
        {
            pMovement.canStand = false;
        };

        OnPlayerTriggerExit += (collision) =>
        {
            pMovement.canStand = true;
        };*/
    }
}
