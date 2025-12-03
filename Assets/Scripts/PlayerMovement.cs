using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player_Movement : MonoBehaviour
{

    public float moveSpeed;
    public GameObject cam;
    public float jumpPower;
    public Rigidbody2D playerRB;
    public float airDrag;
    public float wallSlideSpeed;
    public float wallJumpTime;
    public float wallJumpDuration;
    public Vector2 wallJumpPower;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public Transform wallCheck;
    public LayerMask wallLayer;

    private float horizontal;
    private bool isFacingRight;
    private bool isWallSlide;
    private bool isWallJump;
    private float wallJumpDirection;
    private float wallJumpCount;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        inputControl();
        WallSlide();
        WallJump();
        if (!isWallJump)
        {
            Flip();
        }

        cam.transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }

    private void inputControl()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            playerRB.linearVelocity = new Vector2(playerRB.linearVelocity.x, jumpPower);
        }
        if (Input.GetButtonDown("Jump") && playerRB.linearVelocity.y > 0f)
        {
            playerRB.linearVelocity = new Vector2(playerRB.linearVelocity.x, playerRB.linearVelocity.y * 0.5f);
        }
    }
    private void FixedUpdate()
    {
        if (!isWallJump)
        {
            if (!IsGrounded() && ((isFacingRight && horizontal > 0f) || (!isFacingRight && horizontal < 0f)))
            {
                playerRB.linearVelocity = new Vector2(horizontal * moveSpeed * airDrag, playerRB.linearVelocity.y);
            } else
            {
                playerRB.linearVelocity = new Vector2(horizontal * moveSpeed, playerRB.linearVelocity.y);
            }
        }
        
    }
    private void Flip()
    {
        if (IsGrounded() && ((isFacingRight && horizontal > 0f) || (!isFacingRight && horizontal < 0f)))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool isOnWall()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (isOnWall() && !IsGrounded() && horizontal != 0f)
        {
            isWallSlide = true;
            playerRB.linearVelocity = new Vector2(playerRB.linearVelocityX, Mathf.Clamp(playerRB.linearVelocityY, -wallSlideSpeed, float.MaxValue));
        } else
        {
            isWallSlide = false;
        }
    }

    private void WallJump()
    {
        if (isWallSlide)
        {
            isWallJump = false;
            wallJumpDirection = -transform.localScale.x;
            wallJumpCount = wallJumpTime;

            CancelInvoke(nameof(StopWallJump));
        }
        else
        {
            wallJumpCount -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump") && wallJumpCount > 0f)
        {
            isWallJump = true;
            playerRB.linearVelocity = new Vector2(wallJumpDirection * wallJumpPower.x, wallJumpPower.y);
            wallJumpCount = 0f;

            if (transform.localScale.x != wallJumpDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJump), wallJumpDuration);
        }
    }

    private void StopWallJump()
    {
        isWallJump = false;
    }

}
