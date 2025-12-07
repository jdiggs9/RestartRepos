using UnityEngine;
using UnityEngine.InputSystem.HID;

public class PlayerAttack : MonoBehaviour
{
    
    public int damage;

    private bool hasHit = false;
    private float hitCooldown = 0f;

    void Update()
    {
        if (hitCooldown > 0)
            hitCooldown -= Time.deltaTime;
        else
            hasHit = false;
    }



    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 12 && !hasHit)
        {
            hasHit = true;
            hitCooldown = 1f;

            var enemy = other.GetComponentInParent<Enemies>();
            if (damage > 0)
                enemy.DamageEnemy(damage);
            else
                enemy.StopMovement();
        }
    }
    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.layer == 12 && !hasHit)
    //    {
    //        hasHit = true;
    //        hitCooldown = 0.5f;

    //        var enemy = other.GetComponentInParent<Enemies>();
    //        if (damage > 0)
    //            enemy.DamageEnemy(damage);
    //        else
    //            enemy.StopMovement();
    //    }
    //}
}