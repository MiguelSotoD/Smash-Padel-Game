using UnityEngine;

public class Boundary : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // El rebote se maneja automáticamente por la física
            // Este script solo marca el objeto como pared
        }
    }
}

