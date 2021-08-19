using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Input manager for the player's movement.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Numbers")]
    [SerializeField] private float walkSpeed = 0;
    [SerializeField] private float crawlReduction = 1; // Crawl slower than walk
    [SerializeField] private float jumpHeight = 0;
    [SerializeField] private float jumpReduction = 1; // For short hops
    [SerializeField] private float climbingSpeed = 0;

    [Header("Movement Forms")]
    [SerializeField] private GameObject standingForm = null;
    [SerializeField] private GameObject crawlingForm = null;
    [SerializeField] private GameObject climbingForm = null;

    [Header("Debug Tools")]
    [SerializeField] private Vector2 moveDirection = Vector2.zero;
    [SerializeField] private bool jumpInputted = false;
    [SerializeField] private bool crawlInputted = false;

    [HideInInspector] public bool facingLeft = true;
     public bool canMoveLeft = true;
    [HideInInspector] public bool canMoveRight = true;

    public bool climbing = false;

    private bool _midair = true;
    public bool midair { get { return _midair; } }
    private bool fallingAfterJump = false;

    private bool _crawling = false;
    public bool crawling { get { return _crawling; } }
    [HideInInspector] public bool canStand = true;

    private Rigidbody2D rb = null;
    private SpriteRenderer sprite = null;

    private const string groundTag = "Ground";
    private const string ladderTag = "Ladder";

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponentInChildren<SpriteRenderer>();     
    }

    private void Update()
    {
        if (!climbing)
        {
            SetGravity(1);
            Move();
            Jump();
            Crawl();
        }
        else
        {
            SetGravity(0);
            Climb();
        }
    }

    /// <summary>
    /// Sets the player's direction based on relevant input interactions.
    /// </summary>
    /// <param name="context">The input being read from the button interaction.</param>
    public void Move(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();

        if (context.canceled)
        {
            moveDirection = Vector2.zero;
        }
    }

    /// <summary>
    /// Sets the player's horizontal velocity based on their horizontal direction.
    /// </summary>
    private void Move()
    {
        // While using a tool, the player cannot turn but they can still move in that direction
        if (!GetComponent<Player>().stunned)
        {
            int xDir = 0;
            bool usingTool = GetComponent<PlayerInventory>().currentHeldTool.inUse;

            if (moveDirection.x < 0)
            {
                xDir = -1;
                facingLeft = usingTool ? facingLeft : true;
            }
            else if (moveDirection.x > 0)
            {
                xDir = 1;
                facingLeft = usingTool ? facingLeft : false;
            }

            if ((xDir == -1 && !canMoveLeft) || (xDir == 1 && !canMoveRight))
            {
                xDir = 0;
            }

            sprite.flipX = facingLeft;

            rb.velocity =
                new Vector2(
                    xDir * walkSpeed / (_crawling ? crawlReduction : 1),
                    rb.velocity.y
                );
        }
        else
        {
            rb.velocity = new Vector2(_midair ? rb.velocity.x : 0, rb.velocity.y);
        }
    }

    /// <summary>
    /// Sets the player's vertical velocity based on their vertical direction.
    /// </summary>
    private void Climb()
    {
        int yDir = 0;

        if (moveDirection.y < 0)
        {
            yDir = -1;
        }
        else if (moveDirection.y > 0)
        {
            yDir = 1;
        }

        rb.velocity = new Vector2(0, yDir * climbingSpeed);
    }

    /// <summary>
    /// Registers relevant input interactions for jumping.
    /// </summary>
    /// <param name="context">The input being read from the button interaction.</param>
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpInputted = true;
        }
        else if (context.canceled)
        {
            jumpInputted = false;
        }
    }

    /// <summary>
    /// Accelerates the player upwards off the ground, disengaging the crawling action.
    /// Early input release to short hop. Continuous input to keep jumping.
    /// </summary>
    private void Jump()
    {
        if (jumpInputted && !_midair)
        {
            if (!(_crawling && !canStand))
            {
                SetCrawling(false);
                crawlInputted = false;
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            }
            fallingAfterJump = false;
        }
        PostJump();
    }

    /// <summary>
    /// Handles the player reaching the jump's peak or stopping before then.
    /// </summary>
    private void PostJump()
    {
        if (rb.velocity.y <= 0)
        {
            fallingAfterJump = true;
        }

        if (!jumpInputted && !fallingAfterJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight / jumpReduction);
            fallingAfterJump = true;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public void Crawl(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            crawlInputted = true;
        }
        else if (context.canceled)
        {
            crawlInputted = false;
        }
    }

    /// <summary>
    /// Handles player crawling based on relevant input interactions and environmental contexts.
    /// </summary>
    private void Crawl()
    {
        StartCrawling();
        StopCrawling();
    }

    /// <summary>
    /// Engages the crawling action.
    /// </summary>
    private void StartCrawling()
    {
        if (crawlInputted && !midair)
        {
            SetCrawling(true);
            sprite.flipX = facingLeft;
        }
    }

    /// <summary>
    /// Disengages the crawling action.
    /// </summary>
    private void StopCrawling()
    {
        if (!crawlInputted && crawling && canStand)
        {
            SetCrawling(false);
        }
    }

    /// <summary>
    /// Resets the player's direction of movement and their xy-velocity.
    /// </summary>
    public void ResetMoveDirection()
    {
        moveDirection = Vector2.zero;
        rb.velocity = Vector2.zero;
    }

    /// <summary>
    /// Sets how much the player is affected by gravity.
    /// </summary>
    /// <param name="amount">Gravity strength expressed as a decimal (1 -> 100% strength).</param>
    private void SetGravity(float amount)
    {
        rb.gravityScale = amount;
    }

    /// <summary>
    /// Sets the player into climbing or standing mode based on the given bool.
    /// </summary>
    /// <param name="isClimbing">Whether the player should be climbing right now.</param>
    public void SetClimbing(bool isClimbing)
    {
        SetCrawling(false);

        climbing = isClimbing;
        climbingForm.SetActive(isClimbing);
        standingForm.SetActive(!isClimbing);

        sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    /// <summary>
    /// Sets the player into crawling or standing mode based on the given bool.
    /// </summary>
    /// <param name="isCrawling">Whether the player should be crawling right now.</param>
    private void SetCrawling(bool isCrawling)
    {
        _crawling = isCrawling;

        crawlingForm.SetActive(isCrawling);
        standingForm.SetActive(!isCrawling);

        sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case groundTag:
                _midair = false;
                break;
        }     
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case groundTag:
                _midair = true;
                break;           
        }
    }
}
