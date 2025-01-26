using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float deceleration = 0.95f;
    [SerializeField] private float speedBoostMultiplier = 2.0f; // Multiplier for speed boost
    [SerializeField] private float speedBoostDuration = 5.0f; // Duration of the speed boost in seconds

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private bool isSpeedBoostActive = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");

        //código para movimentação com W, A, S, D  as setas do teclado ao invés de as setas do teclado
        //para testar a movimentação. 
        float moveHorizontal = 0f;
        float moveVertical = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            moveVertical = 1f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveVertical = -1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveHorizontal = -1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveHorizontal = 1f;
        }

        moveDirection = new Vector2(moveHorizontal, moveVertical).normalized;
        
    }

    private void FixedUpdate()
    {   

        Vector2 movement = moveDirection * speed;
        rb.AddForce(movement);

        if (moveDirection == Vector2.zero)
        {
            rb.linearVelocity *= deceleration;
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has the "PowerUp" tag
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            // Trigger the speed boost and destroy the power-up object
            StartCoroutine(SpeedBoost());
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("vida"))
        {
            // Trigger the speed boost and destroy the power-up object
            //vida += 1;
            SaveHealth.Health++;
            SaveHealth.Save();
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator SpeedBoost()
    {
        if (!isSpeedBoostActive)
        {
            isSpeedBoostActive = true;
            speed *= speedBoostMultiplier; // Increase the player's speed
            print("ESTOU TODO BOOST");
            yield return new WaitForSeconds(speedBoostDuration); // Wait for the duration of the speed boost

            speed /= speedBoostMultiplier; // Reset the speed back to normal
            isSpeedBoostActive = false;
        }
    }

}
