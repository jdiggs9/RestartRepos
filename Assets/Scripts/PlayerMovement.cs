using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player_Movement : MonoBehaviour
{

    public float moveSpeed;
    public GameObject cam;
    //public GameObject player;
    public float jumpPower;
    private float horizontal;
    private bool isFacingRight;
    public Rigidbody2D playerRB;
    //public Rigidbody2D camRB;
    public Transform groundCheck;
    public LayerMask groundLayer;
    
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        inputControl();
        Flip();
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
        playerRB.linearVelocity = new Vector2(horizontal * moveSpeed, playerRB.linearVelocity.y);
    }
    private void Flip()
    {
        if ((isFacingRight && horizontal > 0f) || (!isFacingRight && horizontal < 0f))
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
}
