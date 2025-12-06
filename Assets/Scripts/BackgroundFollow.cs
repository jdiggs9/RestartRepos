using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BackgroundFollow : MonoBehaviour
{
    public Transform player;      
    public float followSpeed;
    public Vector3 offset;

    void LateUpdate() {
        Vector3 desiredPosition = player.position + offset;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
    }
}
