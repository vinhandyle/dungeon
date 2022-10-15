using System;
using UnityEngine;

/// <summary>
/// Represents a region that triggers an event (external or internal)
/// when objects of a particular type contact it.
/// </summary>
public class TriggerZone : MonoBehaviour
{
    protected PlayerController pMovement = null;

    // Every trigger zone will have one of the two pairs below.
    // Non-generic cases should be added in the override.
    protected Action<Collider2D> OnPlayerTriggerEnter = null;
    protected Action<Collider2D> OnPlayerTriggerExit = null;

    protected Action<Collision2D> OnPlayerCollideEnter = null;
    protected Action<Collision2D> OnPlayerCollideExit = null;

    protected const string playerTag = "Player";

    protected virtual void Awake()
    {
        OnPlayerTriggerEnter += (collision) =>
        {
            pMovement = collision.gameObject.GetComponentInParent<PlayerController>();
        };

        OnPlayerCollideEnter += (collision) =>
        {
            pMovement = collision.gameObject.GetComponentInParent<PlayerController>();
        };
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case playerTag:
                OnPlayerTriggerEnter?.Invoke(collision);
                break;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case playerTag:
                OnPlayerTriggerExit?.Invoke(collision);
                break;
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case playerTag:
                OnPlayerCollideEnter?.Invoke(collision);
                break;
        }
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case playerTag:
                OnPlayerCollideExit?.Invoke(collision);
                break;
        }
    }
}
