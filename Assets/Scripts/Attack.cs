using UnityEngine;
using UnityEngine.InputSystem.HID;

public class Attack : MonoBehaviour
{

    public bool isAttacking;
    public GameObject hud;
    public float moveDir;
    private GameObject player;
    private bool hasHit;
    private float hitCooldown = 0f;

    private void Start() {
        isAttacking = false;
        hasHit = false;
    }
    void Update() {
        if (hitCooldown > 0) {
            hitCooldown -= Time.deltaTime;
        } else {
            hasHit = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.layer == 13 && isAttacking && !hasHit) {
            hasHit = true;
            hitCooldown = .5f;
            hud.GetComponent<HUD>().Damaged();
            player = other.gameObject;
            DamagedDir(moveDir);
        }
    }
    public void DamagedDir(float i) {
        //Vector2 force = new Vector2( 50000f, 200f);
        //player.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        player.GetComponent<Player_Movement>().isHit = true;
        player.GetComponent<Rigidbody2D>().linearVelocityX = moveDir * 15000f;
        player.GetComponent<Rigidbody2D>().linearVelocityY = 30f;
        isAttacking = false;
    }
}
