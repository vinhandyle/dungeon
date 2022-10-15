using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Input manager for the player's movement.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float checkRadius;
    public bool onGround;
    public bool onWall;

    [Header("Movement Numbers")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float crawlReduction; // Crawl slower than walk
    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpReduction; // For short hops
    [SerializeField] private float fallAcceleration;
    [SerializeField] private float climbingSpeed;

    [Header("Movement Forms")]
    [SerializeField] private GameObject standingForm;
    [SerializeField] private GameObject crawlingForm;

    [HideInInspector] public bool canStand = true;

    private Rigidbody2D rb;
    private Animator anim;

    private const string groundTag = "Ground";
    private const string ladderTag = "Ladder";

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponentInChildren<Animator>();     
    }

    private void Update()
    {
        // Set falling animation
        if (onGround)
        {
            anim.SetBool("Falling", false);
        }
        else if (rb.velocity.y < 0 && !anim.GetBool("Dashing"))
        {
            anim.SetBool("Jumping", false);
            anim.SetBool("Falling", true);
        }

        if (!anim.GetBool("Climbing"))
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

    private void FixedUpdate()
    {
        onGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        onWall = Physics2D.OverlapCircle(wallCheck.position, checkRadius, wallLayer);

        bool jumpPressed = Controls.Instance.Jump()[1];

        if (rb.velocity.y < 0 || !jumpPressed)
            rb.velocity += (fallAcceleration - 1) * Physics2D.gravity.y * Time.fixedDeltaTime * Vector2.up;

        //if (rb.velocity.y < 4 && rb.velocity.y > 0 && jumpPressed && airJumpsLeft > 0)
            //rb.velocity += 2 * Physics2D.gravity.y * Time.fixedDeltaTime * Vector2.up;
    }

    #region Horizontal Movement

    /// <summary>
    /// Sets the player's horizontal velocity based on their horizontal direction.
    /// </summary>
    private void Move()
    {
        // While using a tool, the player cannot turn but they can still move in that direction
        if (!GetComponent<Player>().stunned)
        {
            float direction = Controls.Instance.MoveDirection().x;
            anim.SetBool("Walking", direction != 0);

            if (transform.localScale.x * direction < 0) GetComponent<Player>().Flip();

            rb.velocity =
                new Vector2(
                    direction * walkSpeed / (anim.GetBool("Crawling") ? crawlReduction : 1),
                    rb.velocity.y
                );
        }
        else
        {
            rb.velocity = new Vector2(onGround ? 0 : rb.velocity.x, rb.velocity.y);
        }
    }

    #endregion

    #region Jump

    /// <summary>
    /// Accelerates the player upwards off the ground, disengaging the crawling action.
    /// Early input release to short hop. Continuous input to keep jumping.
    /// </summary>
    private void Jump()
    {
        if (Controls.Instance.Jump()[1])
        {
            anim.SetBool("Jumping", true);
            anim.SetBool("Falling", false);
            Controls.Instance.asyncInputs.receivedJump[0] = true;

            // Regular jump
            if (onGround)
            {
                if (!(anim.GetBool("Crawling") && !canStand))
                {
                    SetCrawling(false);
                    rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                }
            }
        }

        // Short hop
        if (Controls.Instance.Jump()[2])
        {
            Controls.Instance.asyncInputs.receivedJump[1] = true;
            if (rb.velocity.y > 0)
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight / jumpReduction);
        }
    }

    #endregion

    #region Crawling

    /// <summary>
    /// Handles player crawling based on relevant input interactions and environmental contexts.
    /// </summary>
    private void Crawl()
    {
        //StartCrawling();
        //StopCrawling();

        if (Controls.Instance.Crawl())
        {
            if (onGround) SetCrawling(true);
        }
        else
        { 
            if (canStand && anim.GetBool("Crawling")) SetCrawling(false);
        }
    }

    /// <summary>
    /// Sets the player into crawling or standing mode based on the given bool.
    /// </summary>
    /// <param name="isCrawling">Whether the player should be crawling right now.</param>
    private void SetCrawling(bool isCrawling)
    {
        anim.SetBool("Crawling", isCrawling);
        crawlingForm.SetActive(isCrawling);
        standingForm.SetActive(!isCrawling);

        //sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    #endregion

    #region Climbing

    /// <summary>
    /// Sets the player's vertical velocity based on their vertical direction.
    /// </summary>
    private void Climb()
    {
        rb.velocity = new Vector2(0, Controls.Instance.MoveDirection().y * climbingSpeed);
    }

    #endregion

    #region Misc

    /// <summary>
    /// Resets the player's direction of movement and their xy-velocity.
    /// </summary>
    public void ResetMoveDirection()
    {
        //moveDirection = Vector2.zero;
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

    #endregion
}
