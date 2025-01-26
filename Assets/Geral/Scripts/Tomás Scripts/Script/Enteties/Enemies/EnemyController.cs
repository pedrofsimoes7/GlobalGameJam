using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyController : MonoBehaviour
{

    public enum EnemyState { Chasing, Retreating, Recharging }

    [SerializeField] private Transform[] potentialTargets; // Array of potential targets
    [SerializeField] private float speed = 3f; // Movement speed
    [SerializeField] private float retreatDistance = 0.5f; // Distance to retreat after collision
    [SerializeField] private float rechargeTime = 1f; // Time to wait before charging again
    [SerializeField] private float deceleration = 0.95f; // Smooth stopping
    [SerializeField] private float rayDistance = 0.10f; // Distance for raycasts
    [Header("Power-Up Settings")]
    [SerializeField] private float speedBoostMultiplier = 2f; // Multiplier for speed boost
    [SerializeField] private float speedBoostDuration = 5f; // Duration of the speed boost

    private Rigidbody2D rb;
    private Transform target; // Current closest target
    private Vector2 moveDirection;
    private EnemyState currentState = EnemyState.Chasing;
    private float rechargeTimer = 0f;

    private bool isSpeedBoostActive = false;


    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Update the closest target dynamically
        FindClosestTarget();

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
        Vector2 movement = moveDirection * speed;
        rb.AddForce(movement);

        if (moveDirection == Vector2.zero)
        {
            rb.linearVelocity *= deceleration;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            currentState = EnemyState.Retreating;
        }

        // Check if the collided object is a PowerUp
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            StartCoroutine(SpeedBoost());
            Destroy(collision.gameObject);
        }

        // Check if the collided object is a "vida" item
        if (collision.gameObject.CompareTag("vida"))
        {
            // Implement the life-increasing logic here
            Debug.Log("Life increased by 1!");
            Destroy(collision.gameObject);
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

        // Recalculate moveDirection toward the target after dodging
        moveDirection = (target.position - transform.position).normalized;
       }


    private void FindClosestTarget()
    {
        // Find the closest target
        if (potentialTargets.Length == 0) return;

        // Find the closest target
        target = potentialTargets
            .OrderBy(t => Vector2.Distance(transform.position, t.position))
            .FirstOrDefault();
    }

    private IEnumerator SpeedBoost()
    {
        if (!isSpeedBoostActive)
        {
            isSpeedBoostActive = true;
            speed *= speedBoostMultiplier;
            Debug.Log("Speed boost activated!");

            yield return new WaitForSeconds(speedBoostDuration);

            speed /= speedBoostMultiplier;
            isSpeedBoostActive = false;
            Debug.Log("Speed boost ended.");
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
