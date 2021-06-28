using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This is a test class to learn the Unity Input System.
/// </summary>
public class InputTest : MonoBehaviour
{
    public void Move(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<Vector2>());
    }
}
