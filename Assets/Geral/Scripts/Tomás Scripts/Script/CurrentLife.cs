using Unity.VisualScripting;
using UnityEngine;

public class CurrentLife : MonoBehaviour
{
    public int maxLifes = 3;
    public int currentLife;
    public void Start()
    {
        currentLife = maxLifes;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has the "PowerUp" tag
        if (collision.gameObject.CompareTag("vida"))
        {
            // Trigger the speed boost and destroy the power-up object
            currentLife += 1;

            Destroy(collision.gameObject);
        }
    }


}
