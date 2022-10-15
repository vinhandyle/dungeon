using System.Collections;
using UnityEngine;

/// <summary>
/// Represents the shield tool used to block and parry incoming attacks.
/// </summary>
public class Shield : Tool
{
    [SerializeField] private bool firstBlock = true;
    [SerializeField] private int stability = 0;
    [SerializeField] private int maxStability = 0;
    [SerializeField] private float stunDuration = 0;

    [Header("Shield Info")]
    [SerializeField] private GameObject parryingHitBox = null;
    [SerializeField] private GameObject blockingHitBox = null;
    [SerializeField] private float parryDuration = 0;

    protected override void SetUpItem()
    {
        itemName = "Shield";
        itemDescription = "This is a shield.";
    }

    protected override void Awake()
    {
        base.Awake();

        OnUse += Block;
        OnRelease += Lower;

        // Initialize player vars for hitboxes
        stability = maxStability;
        Lower();
    }

    private void Update()
    {
        if (stability <= 0)
        {
            GuardBreak();
        }
    }

    /// <summary>
    /// Sets the direction of the shield and sets its components active.
    /// </summary>
    private void Raise()
    {
        SetToolDirection();

        sprite.gameObject.SetActive(true);
    }

    /// <summary>
    /// Transitions to blocking.
    /// </summary>
    private void Hold()
    {
        toolState = "blocking";
        blockingHitBox.SetActive(true);
    }

    /// <summary>
    /// Sets active the blocking hitbox. If this is the first block,
    /// the parrying hitbox is set active first.
    /// </summary>
    private void Block()
    {
        Raise();

        if (firstBlock)
        {
            StartCoroutine(Parry());
            firstBlock = false;
        }
        else
        {
            Hold();
        }
    }

    /// <summary>
    /// Sets the shield's components inactive.
    /// </summary>
    private void Lower()
    {
        firstBlock = true;

        parryingHitBox.SetActive(false);
        blockingHitBox.SetActive(false);
        sprite.gameObject.SetActive(false);
        inUse = false;
    }

    /// <summary>
    /// Stuns the player for a certain duration, disables the shield, and fully restores stability.
    /// </summary>
    private void GuardBreak()
    {
        Debug.Log("Guard Broken!");
        player.Knockback(0, stunDuration, 0, Vector3.zero);
        Lower();
        stability = maxStability;
    }

    /// <summary>
    /// Sets active the parrying hitbox for a certain duration then sets it inactive.
    /// </summary>
    IEnumerator Parry()
    {
        toolState = "parrying";
        parryingHitBox.SetActive(true);

        yield return new WaitForSeconds(parryDuration);

        parryingHitBox.SetActive(false);
        Hold();
    }
}
