using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the player camera.
/// </summary>
public class PlayerCamera : MonoBehaviour
{
    [SerializeField] GameObject player = null;

    private bool anchored = false;

    private bool canMoveUp = true;
    private bool canMoveDown = true;
    private bool canMoveLeft = true;
    private bool canMoveRight = true;

    private const string anchorTag = "Anchor";

    private void Update()
    {
        MoveCamera();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(anchorTag))
        {

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(anchorTag))
        {

        }
    }

    /// <summary>
    /// Moves the camera along with the player.
    /// The camera is centered onto the player unless a camera border is in the way.
    /// </summary>
    private void MoveCamera()
    {
        if (!anchored)
        {
            Vector3 playerPos = player.transform.position;
            Vector3 camPos = transform.position;
            float posX = 0, posY = 0;

            if ((playerPos.x > camPos.x && canMoveRight) || (playerPos.x < camPos.x && canMoveLeft))
            {
                posX = playerPos.x;
            }
            else
            {
                posX = camPos.x;
            }

            if ((playerPos.y > camPos.y && canMoveUp) || (playerPos.y < camPos.y && canMoveDown))
            {
                posY = playerPos.y;
            }
            else
            {
                posY = camPos.y;
            }

            transform.position = new Vector3(posX, posY, camPos.z);
        }        
    }

    /// <summary>
    /// Sets the position of the camera to the player's position, ignoring camera borders.
    /// </summary>
    public void TeleportCamera()
    {
        Vector3 playerPos = player.transform.position;
        transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);
    }

    /// <summary>
    /// Changes the directional lock(s) of the camera based on interaction(s) with the camera border(s).
    /// </summary>
    /// <param name="border">The camera border that is being interacted with.</param>
    /// <param name="lockState">The state a lock will be set to.</param>
    public void LockDirectionalMovement(GameObject border, bool lockState)
    {
        switch (border.name)
        {
            case "Up":
                canMoveUp = lockState;
                break;

            case "Down":
                canMoveDown = lockState;
                break;

            case "Right":
                canMoveRight = lockState;
                break;

            case "Left":
                canMoveLeft = lockState;
                break;
        }
    }
}
