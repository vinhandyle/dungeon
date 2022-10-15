using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The control scheme.
/// </summary>
public class Controls : Singleton<Controls>
{
    public ControlsInputClass inputs;
    public ControlsAsyncInputClass asyncInputs;

    private void Update()
    {
        CheckAsyncInputsReceived();
    }

    /// <summary>
    /// Checks whether player controller received any of the async inputs.
    /// Needed for no input holding implementation since context triggers
    ///     don't last long enough for the player controller to read them.
    /// </summary>
    private void CheckAsyncInputsReceived()
    {
        if (inputs.jump[2]) inputs.jump[2] = !asyncInputs.receivedJump[1];

        if (inputs.attack) inputs.attack = !asyncInputs.receivedAttack;
        if (inputs.dash) inputs.dash = !asyncInputs.receivedDash;
    }

    #region Unity Events for Input System

    public void Move(InputAction.CallbackContext context)
    {
        inputs.moveDirection = context.ReadValue<Vector2>();

        if (context.canceled)
        {
            inputs.moveDirection = Vector2.zero;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            inputs.jump[0] = true;
            inputs.jump[1] = true;
            asyncInputs.receivedJump[0] = false;
        }

        if (context.canceled)
        {
            inputs.jump[0] = false;
            inputs.jump[1] = false;
            inputs.jump[2] = true;
            asyncInputs.receivedJump[1] = false;
        }
    }

    public void Crawl(InputAction.CallbackContext context)
    {
        if (context.started) inputs.crawl = true;
        if (context.canceled) inputs.crawl = false;
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            inputs.attack = true;
            asyncInputs.receivedAttack = false;
        }

        if (context.canceled) inputs.attack = false;
    }

    public void Block(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            inputs.block = true;
            asyncInputs.receivedBlock = false;
        }

        if (context.canceled) inputs.block = false;
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            inputs.dash = true;
            asyncInputs.receivedDash = false;
        }

        if (context.canceled) inputs.dash = false;
    }

    #endregion

    #region Player Controls

    /// <summary>
    /// Returns true while the user holds down any key mapped to the left direction.
    /// </summary>
    public Vector2 MoveDirection()
    {
        return new Vector2(
            (inputs.moveDirection.x > 0) ? 1 : (inputs.moveDirection.x < 0) ?-1 : 0,
            (inputs.moveDirection.y > 0) ? 1 : (inputs.moveDirection.y < 0) ? -1 : 0
            );
    }

    /// <summary>
    /// <para>Returns a list of the statuses of the following three inputs by index: </para>
    /// <br>0: True while the player is holding down the mapped key for jump</br>
    /// <br>1: True if the player pressed the mapped key for jump</br>
    /// <br>2: True if the player released the mapped key for jump</br>
    /// </summary>
    public bool[] Jump()
    {
        return inputs.jump;
    }

    public bool Crawl()
    {
        return inputs.crawl;
    }

    /// <summary>
    /// Returns true if the user presses any key mapped to attack.
    /// </summary>
    public bool Attack()
    {
        return inputs.attack;
    }

    /// <summary>
    /// Returns true if the user presses any key mapped to the dash ability.
    /// </summary>
    public bool Dash()
    {
        return inputs.dash;
    }
    #endregion

    #region Menus

    /// <summary>
    /// Returns true if the user presses any key mapped to pause.
    /// </summary>
    public bool Pause()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }

    /// <summary>
    /// Returns true if the user presses the assigned to bring up the level selection.
    /// </summary>
    public bool SelectLevel()
    {
        return Input.GetKeyDown(KeyCode.P);
    }

    #endregion
}

/// <summary>
/// Collapsible section for inputs received from Unity Events.
/// </summary>
[System.Serializable]
public class ControlsInputClass
{
    public Vector2 moveDirection;
    public bool crawl, attack, block, dash;
    public bool[] jump = new bool[3];
}

/// <summary>
/// Collapsible section for inputs received by the player controller.
/// </summary>
[System.Serializable]
public class ControlsAsyncInputClass
{
    public bool[] receivedJump = new bool[2];
    public bool receivedAttack, receivedBlock, receivedDash;
}