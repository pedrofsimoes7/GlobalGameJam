using UnityEngine;

public class Movimentos : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    private float deceleration = 0.95f;
    private float recoilMultiplier = 2.0f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
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
        Rigidbody2D otherRb = collision.rigidbody;
        if (otherRb != null)
        {
            Vector2 relativeVelocity = rb.linearVelocity - otherRb.linearVelocity;
            Vector2 recoilDirection = relativeVelocity.normalized;
            float recoilForce = relativeVelocity.magnitude * recoilMultiplier;

            rb.AddForce(-recoilDirection * recoilForce, ForceMode2D.Impulse);
            otherRb.AddForce(recoilDirection * recoilForce, ForceMode2D.Impulse);
        }
    }
}
