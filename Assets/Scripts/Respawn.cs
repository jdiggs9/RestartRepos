using UnityEngine;

public class Respawn : MonoBehaviour
{

    public GameObject hud;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.transform.position = new Vector3(0f,0f,-5f);
        hud.GetComponent<HUD>().Damaged();
    }
}
