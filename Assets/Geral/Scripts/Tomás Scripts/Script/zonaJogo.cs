using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameZone : MonoBehaviour
{
    [SerializeField] private Collider2D zonaCollider;
    [SerializeField] private float reviveRadius = 0.1f;
    //[SerializeField] private int maxLives = 3; // Max lives for each player

    // Text field


    //private Dictionary<GameObject, int> playerLives = new Dictionary<GameObject, int>(); // Track remaining lives for each player

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the game zone");

            // Initialize lives for the player if not already present
            /*if (!playerLives.ContainsKey(other.gameObject))
            {
                playerLives[other.gameObject] = maxLives;
            }*/
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            Debug.Log("Player exited the game zone");


            //playerLives[other.gameObject]--;
            SaveHealth.Health--;
            if (SaveHealth.Health > 0)
            {
                //Debug.Log($"Player has {playerLives[other.gameObject]} lives remaining.");
                StartCoroutine(RevivePlayer(other));
            }
            else
            {
                Debug.Log("Player has no lives left and cannot return to the game.");
                other.gameObject.SetActive(false); // Deactivate the player
                //playerLives.Remove(other.gameObject); // Optionally remove the player from tracking
            }
        }

        if (other.CompareTag("AI1")) { }
        if (other.CompareTag("AI1")) { }
        if (other.CompareTag("AI1")) { }

        SaveHealth.Save();
    }

    private IEnumerator RevivePlayer(Collider2D player)
    {
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);

        Vector2 randomPosition = GetValidRandomPositionInZone();
        player.transform.position = randomPosition;
        player.gameObject.SetActive(true);

        Debug.Log("Player revived");
    }

    private Vector2 GetValidRandomPositionInZone()
    {
        if (zonaCollider == null)
        {
            Debug.LogError("zonaCollider is not assigned!");
            return Vector2.zero;
        }

        Bounds bounds = zonaCollider.bounds;
        Vector2 randomPosition;
        int attempts = 0;

        do
        {
            float randomX = Random.Range(bounds.min.x, bounds.max.x);
            float randomY = Random.Range(bounds.min.y, bounds.max.y);
            randomPosition = new Vector2(randomX, randomY);

            attempts++;
        }
        while (Physics2D.OverlapCircle(randomPosition, reviveRadius) != null && attempts < 100);

        if (attempts >= 100)
        {
            Debug.LogError("Failed to find a valid position to revive the player.");
        }

        return randomPosition;
    }
}
