using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Represents the player.
/// </summary>
public class Player : Entity
{
    [Header("Components")]
    [SerializeField] private PlayerHealth health = null;

    public PlayerInventory inventory = null;

    private float damageMultiplier = 1;
    private bool usingTool = false;

    private void Update()
    {
        if (usingTool)
        {
            UseTool();
        }
        else
        {
            ReleaseTool();
        }
    }

    /// <summary>
    /// Increases the player's health by 1.
    /// </summary>
    public void HealthUp()
    {
        health.IncreaseMaxHealth(1);
    }

    /// <summary>
    /// Indicates whether the tool currently held should be used based on relevant input interactions.
    /// </summary>
    /// <param name="context">The input being read from the button interaction.</param>
    public void UseTool(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            usingTool = true;
        }
        else if (context.canceled)
        {
            usingTool = false;
        };
    }

    /// <summary>
    /// Uses the tool currently held by the player.
    /// </summary>
    private void UseTool()
    {
        if (!GetComponent<PlayerMovement>().climbing)
        {
            inventory?.currentHeldTool?.Use();
        }
    }

    /// <summary>
    /// Releases the tool currently held by the player.
    /// </summary>
    private void ReleaseTool()
    {
        inventory?.currentHeldTool?.Release();
    }

    /// <summary>
    /// Switches to the next tool in the equipped sequence.
    /// </summary>
    /// <param name="context">The input being read from the button interaction.</param>
    public void CycleTool(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (inventory.currentHeldTool != null && !inventory.currentHeldTool.inUse)
            {
                float cycleDirection = context.ReadValue<Vector2>().x;

                if (cycleDirection > 0)
                {
                    inventory?.GetNextTool();
                }
                else
                {
                    inventory?.GetPrevTool();
                }
            }
        }       
    }
}
