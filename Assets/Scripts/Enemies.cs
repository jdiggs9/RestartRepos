using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Enemies : MonoBehaviour
{

    public float moveSpeed;
    public float maxDisMoved;
    public float minDisMoved;
    public float lineOfSight;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public Transform wallCheck;
    public Transform airCheck;
    public float attackRange;
    public Transform player;
    public GameObject hud;
    public float moveDelay;
    public bool hasMeleeAttack;
    public GameObject hitBox;

    private float moveDir = 1f;
    private bool inLOS;
    private Rigidbody2D rb;
    private bool isFacingRight;
    private Vector3 targetPos;
    private bool isMoving = false;
    private float pauseTimer = 0f;
    private Animator ani;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        ChooseNewTarget();
        inLOS = false;
        isFacingRight = true;
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (!isMoving && !inLOS) {
            pauseTimer -= Time.deltaTime;
            if (pauseTimer <= 0) {
                ChooseNewTarget();
            }
            return;
        } if (Vector2.Distance(rb.position, player.position) < lineOfSight && !inLOS) {
            inLOS = true;
            ChooseNewTarget();
        } if (Vector2.Distance(rb.position, player.position) < attackRange) {
            if (hasMeleeAttack) {
                MeleeAttack();
            } else {
                RangedAttack();
            }
        }

        rb.MovePosition(Vector2.MoveTowards(rb.position, targetPos, moveSpeed * Time.deltaTime));

        if (Vector2.Distance(rb.position, targetPos) < 0.05f || IsDeadEnd()) {
            StopMovement();
        } if (Vector2.Distance(rb.position, player.position) > lineOfSight) {
            inLOS = false;
        }
    }

    public void MeleeAttack() {
        ani.SetBool("Attack", true);
        hitBox.GetComponent<Attack>().isAttacking = true;
    }
    public void RangedAttack() {

    }

    private void StopMovement() {
        isMoving = false;
        pauseTimer = moveDelay;
        RandomizeDir();
    }
    private void ChooseNewTarget() {
        if (!inLOS) {
            float distance = Random.Range(minDisMoved, maxDisMoved);
            targetPos = new Vector3(transform.position.x + (distance * moveDir), transform.position.y, transform.position.z);
        } else {
            targetPos = new Vector3(transform.position.x + (attackRange * moveDir), transform.position.y, transform.position.z);
        }
        isMoving = true;
    }

   

    private void RandomizeDir() {
        float i = Random.Range(0, 2);
        if (i == 1) {
            moveDir = -1;
        } else {
            moveDir = 1;
        }
        if ((moveDir > 0 && !isFacingRight) || (moveDir < 0 && isFacingRight))
            Flip();
    }

    private bool IsDeadEnd() {
        if (!Physics2D.Raycast(airCheck.position, transform.TransformDirection(Vector2.down), 0.5f, groundLayer, -4, -2) 
            || Physics2D.Raycast(wallCheck.position, transform.forward, 0.5f, wallLayer, -4, -2))
        {
            return true;
        }
        return false;
    }

    private void Flip() {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}
