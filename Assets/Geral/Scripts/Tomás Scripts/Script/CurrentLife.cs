using Unity.VisualScripting;
using UnityEngine;

public class CurrentLife : MonoBehaviour
{

    public int maxLifes = 3;
    public int currentLife;
    private void Start()
    {
        currentLife = maxLifes;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has the "vida" tag
        if (collision.gameObject.CompareTag("vida"))
        {
            // Increase life, but don't exceed maxLifes
            if (currentLife < maxLifes)
            {
                currentLife++;
                print($"Player gained a life! Current Life: {currentLife}");
            }

            Destroy(collision.gameObject);
        }
    }

}
