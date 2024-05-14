

using UnityEngine;

public class DestoyObstacle : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Destroy(collision.gameObject);
        }
    }
}
