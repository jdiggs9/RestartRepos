using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class Element : MonoBehaviour
{

    public bool fire;
    public bool earth;

    public GameObject player;
    public LayerMask enemyLayer;
    public GameObject earthLA;
    public GameObject earthWall;
    public GameObject earthWallHB;
    public GameObject earthSpike;
    public GameObject hud;

    private float cooldown = 0;
    private float waitTime = 3f;
    private Collider2D enemy;
    private bool isAttacking;
    private float moveDir;

    void Update()
    {
        moveDir = player.GetComponent<Player_Movement>().isFacingRight ? -1 : 1;

        InputManager();
    }

    private void InputManager()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            StartCoroutine(Dash());

        if (Input.GetButtonDown("Fire1"))
            StartCoroutine(LightAttack());

        if (Input.GetButtonDown("Fire2"))
            StartCoroutine(HeavyAttack());

        if (Input.GetKeyDown(KeyCode.Q))
            StartCoroutine(Special());
    }

    // -----------------------------
    // EARTH DASH
    // -----------------------------
    private IEnumerator Dash()
    {
        if (!earth) yield break;

        earthSpike.SetActive(true);
        yield return new WaitForSeconds(1f);
        earthSpike.SetActive(false);
        yield return new WaitForSeconds(10f);
    }

    // -----------------------------
    // LIGHT ATTACK (EARTH COMBO)
    // -----------------------------
    private IEnumerator LightAttack()
    {
        if (!earth) yield break;

        // attack 1
        earthLA.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        earthLA.SetActive(false);
        yield return new WaitForSeconds(0.5f);

        //// attack 2
        //earthLA.SetActive(true);
        //yield return new WaitForSeconds(0.5f);
        //earthLA.SetActive(false);
        //yield return new WaitForSeconds(1f);

        //// finisher
        //earthLA.SetActive(true);
        //yield return new WaitForSeconds(0.5f);
        //earthLA.SetActive(false);
    }

    // -----------------------------
    // HEAVY ATTACK (EARTH PUNCH)
    // -----------------------------
    private IEnumerator HeavyAttack()
    {
        if (!earth) yield break;

        Vector3 start = new Vector3(player.transform.position.x + .4f, player.transform.position.y + .05f, player.transform.position.z);
        Vector3 target = start + new Vector3(moveDir * 10f, 0, 0);

        earthWallHB.SetActive(true);
        earthWall.SetActive(true);

        // move forward over time
        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime;
            earthWall.transform.position = Vector3.Lerp(start, target, t / 1f);
            yield return null;
        }

        // disable and reset
        earthWallHB.SetActive(false);
        earthWall.SetActive(false);
        earthWall.transform.position = start;

        yield return new WaitForSeconds(10f);
    }

    // -----------------------------
    // SPECIAL ATTACK
    // -----------------------------
    private IEnumerator Special()
    {
        if (!earth) yield break;

        hud.GetComponent<HUD>().AddShield();
        yield return new WaitForSeconds(5);

        // if shield is full, remove 1 hp
        HUD h = hud.GetComponent<HUD>();
        if (h.hp[h.FindLastNull() - 1] == 4)
            h.Damaged();

        yield return new WaitForSeconds(20);
    }


}
