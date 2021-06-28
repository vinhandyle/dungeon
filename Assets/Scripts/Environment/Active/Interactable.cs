using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Represents any object that the player can use the Interact action on.
/// </summary>
public class Interactable : MonoBehaviour
{
    protected Player player = null;
    protected bool playerInRange = false;

    protected event Action<Player> OnInteract;
    protected event Action<Collider2D> OnEnterRange;
    protected event Action<Collider2D> OnExitRange;

    protected const string playerTag = "Player";

    protected virtual void Awake()
    {
        OnEnterRange += (collision) =>
        {
            player = collision.GetComponentInParent<Player>();
            playerInRange = true;
        };

        OnExitRange += (collision) =>
        {
            playerInRange = false;
        };
    }

    /// <summary>
    /// If the player is in range of the object and the relevant input is received,
    /// any interaction effects are triggered.
    /// </summary>
    /// <param name="context">The input being read from the button interaction.</param>
    public void Interact(InputAction.CallbackContext context)
    {
        if (context.started && playerInRange)
        {
            Debug.Log("Interacted with " + gameObject.name + " at " + Time.time);
            OnInteract?.Invoke(player);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            OnEnterRange?.Invoke(collision);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            OnExitRange?.Invoke(collision);
        }
    }
}
