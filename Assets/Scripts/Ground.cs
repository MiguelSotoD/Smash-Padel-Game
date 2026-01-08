using UnityEngine;

public class Ground : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // El rebote se maneja en BallController
            // Este script solo marca el objeto como suelo
        }
    }
}

