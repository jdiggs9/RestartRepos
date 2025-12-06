using UnityEngine;
using UnityEngine.InputSystem.HID;

public class Attack : MonoBehaviour
{

    public bool isAttacking;
    public GameObject hud;

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == 13 && isAttacking)
        hud.GetComponent<HUD>().Damaged();
    }
}
