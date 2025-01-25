using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyController : MonoBehaviour
{

    public enum EnemyState { Chasing, Retreating, Recharging }

    [SerializeField] private Transform target; // Player or target to follow
    [SerializeField] private float speed = 5f; // Movement speed
    [SerializeField] private float retreatDistance = 2f; // Distance to retreat after collision
    [SerializeField] private float rechargeTime = 1f; // Time to wait before charging again
    [SerializeField] private float deceleration = 0.95f; // Smooth stopping

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private EnemyState currentState = EnemyState.Chasing;
    private float rechargeTimer = 0f;


    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (target == null) return;

        switch (currentState)
        {
            case EnemyState.Chasing:
                // Move toward the target
                moveDirection = (target.position - transform.position).normalized;
                break;

            case EnemyState.Retreating:
             // Move away from the target
               moveDirection = (transform.position - target.position).normalized;

                // Check if retreat distance is reached
                if (Vector2.Distance(transform.position, target.position) >= retreatDistance)
                {
                    moveDirection = Vector2.zero;
                   currentState = EnemyState.Recharging;
                  rechargeTimer = rechargeTime; // Start recharge timer
                }
            break;

            case EnemyState.Recharging:
                // Stay idle while recharging
                moveDirection = Vector2.zero;

                // Countdown the recharge timer
                rechargeTimer -= Time.deltaTime;
                if (rechargeTimer <= 0f)
                {
                    currentState = EnemyState.Chasing; // Resume chasing
                }
                break;
        }
    }

    private void FixedUpdate()
    {
        /*    if (moveDirection != Vector2.zero)
            {
                rb.linearVelocity = moveDirection * speed;
            }*/

        /*
                else
                {
                    // Smooth deceleration
                    rb.linearVelocity *= deceleration;
                }*/

        Vector2 movement = moveDirection * speed;
        rb.AddForce(movement);

        if (moveDirection == Vector2.zero)
        {
            rb.linearVelocity *= deceleration;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            currentState = EnemyState.Retreating;
        }
    }
}
