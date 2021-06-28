using UnityEngine;

/// <summary>
/// Represents the borders that prevent the player camera from moving in a certain direction.
/// </summary>
public class CameraBorder : MonoBehaviour
{
    private const string barrierTag = "Barrier";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(barrierTag))
        {
            transform.parent.GetComponent<PlayerCamera>().LockDirectionalMovement(gameObject, false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(barrierTag))
        {
            transform.parent.GetComponent<PlayerCamera>().LockDirectionalMovement(gameObject, true);
        }
    }
}
