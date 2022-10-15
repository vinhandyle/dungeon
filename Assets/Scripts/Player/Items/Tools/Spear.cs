using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the spear as an item. 
/// </summary>
public class Spear : Tool
{
    [Header("Spear Info")]
    [SerializeField] private float standingYPos = 0;
    [SerializeField] private float crawlingYPos = 0;
    private Vector3 initialAttackPosition = Vector3.zero;

    protected override void SetUpItem()
    {
        itemName = "Spear";
        itemDescription = "This is a spear.";
    }

    protected override void Awake()
    {
        /*base.Awake();

        OnUse += Ready;
        range.OnExit += Return;

        initialAttackPosition = attack.transform.localPosition;
        Return();*/
    }   

    private void Update()
    {
        /*if (toolState == "thrusting")
        {
            Thrust();
            Reposition();
        }*/
    }

    /// <summary>
    /// Sets the direction of the spear and sets its components active.
    /// </summary>
    private void Ready()
    {
        /*SetToolDirection();

        attack.gameObject.SetActive(true);
        sprite.gameObject.SetActive(true);
        toolState = "thrusting";*/
    }

    /// <summary>
    /// The spear travels forward to its maximum range relative to the player.
    /// </summary>
    private void Thrust()
    {
        //transform.localPosition += Vector3.right * useSpeed * (playerMovement.facingLeft ? -1 : 1);
    }

    /// <summary>
    /// Repositions the spear so that it is always level with the player.
    /// </summary>
    private void Reposition()
    {
        /*gameObject.transform.localPosition =
            new Vector3(
                transform.localPosition.x,
                player.GetComponent<PlayerMovement>().crawling ? crawlingYPos : standingYPos,
                transform.localPosition.z
            );

        attack.transform.localPosition = initialAttackPosition;*/
    }

    /// <summary>
    /// Returns the spear to its initial position and sets its components inactive.
    /// </summary>
    private void Return()
    {
        /*toolState = "returning";

        attack.gameObject.SetActive(false);
        sprite.gameObject.SetActive(false);

        transform.localPosition = Vector3.zero;
        Reposition();
        inUse = false;*/
    }        
}
