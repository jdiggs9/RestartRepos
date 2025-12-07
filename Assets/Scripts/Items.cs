using UnityEngine;

public class Items : MonoBehaviour
{

    public bool healFull;
    public bool healHalf;
    public bool healComplete;
    public bool shield;
    public bool incHP;
    public bool upgrade;
    public bool coin;

    public GameObject hud;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == 13) {
            if (CanPickUp()) {
                if (healFull) {
                    hud.GetComponent<HUD>().HealFull();
                    Destroy(gameObject);
                } else if (healHalf) {
                    hud.GetComponent<HUD>().HealHalf();
                    Destroy(gameObject);
                } else if (healComplete) {
                    hud.GetComponent<HUD>().HealComplete();
                    Destroy(gameObject);
                } else if (shield) {
                    hud.GetComponent<HUD>().AddShield();
                    Destroy(gameObject);
                } else if (incHP) {
                    hud.GetComponent<HUD>().IncreaseHP();
                    Destroy(gameObject);
                } else if (upgrade) {

                } else if (coin) {
                    hud.GetComponent<HUD>().coins++;
                    Destroy(gameObject);
                }
            }
        }
    }

    public bool CanPickUp() {
        if ((healFull || (healHalf || healComplete)) && !hud.GetComponent<HUD>().IsFullHP()) {
            return true;
        } else if (incHP && !hud.GetComponent<HUD>().IsMaxHP()) {
            return true;
        } else if (shield && !hud.GetComponent<HUD>().IsFullSlots()) {
            return true;
        } else if (coin) {
            return true;
        }
            return false;
    }
}
