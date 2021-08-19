using System;
using UnityEngine;

/// <summary>
/// Base class for all objects used as a range.
/// </summary>
public abstract class Range : MonoBehaviour
{
    [SerializeField] protected bool debug = false;

    public event Action OnEnter = null;
    public event Action OnExit = null;

    /// <summary>
    /// Trigger effects when a collider enters the range from derived classes.
    /// </summary>
    protected void Enter()
    {
        OnEnter?.Invoke();
    }

    /// <summary>
    /// Trigger effects when a collider exits the range from derived classes.
    /// </summary>
    protected void Exit()
    {
        OnExit?.Invoke();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (debug)
        {
            Debug.Log(collision.gameObject.name + " entered range of " + transform.parent.name);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (debug)
        {
            Debug.Log(collision.gameObject.name + " exited range of " + transform.parent.name);
        }
    }
}
