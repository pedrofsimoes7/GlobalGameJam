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
    [SerializeField] private float rayDistance = 0.2f; // Distance for raycasts

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
                AvoidObstacles();
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

    private void AvoidObstacles()
    {
        // Cast rays in 4 directions (forward, backward, left, right)
        Vector2[] directions = new Vector2[]
        {
            moveDirection,                          // Forward
            -moveDirection,                         // Backward
            new Vector2(-moveDirection.y, moveDirection.x), // Left
            new Vector2(moveDirection.y, -moveDirection.x)  // Right
        };

        for (int i = 0; i < directions.Length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], rayDistance);

            if (hit.collider != null && hit.collider.CompareTag("Obstacle") && !hit.collider.CompareTag("Player"))
            {
                // Avoid the obstacle by choosing a new direction
                if (i == 0) // Forward blocked, try left or right
                {
                    moveDirection = directions[2]; // Prefer left
                }
                else if (i == 2 || i == 3) // Left or right blocked, try backward
                {
                    moveDirection = directions[1]; // Go backward
                }

                // Break out of the loop once we adjust direction
                break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (moveDirection != Vector2.zero)
        {
            // Draw raycasts for debugging
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, moveDirection * rayDistance);
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, -moveDirection * rayDistance);
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, new Vector2(-moveDirection.y, moveDirection.x) * rayDistance);
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, new Vector2(moveDirection.y, -moveDirection.x) * rayDistance);
        }
    }
}
