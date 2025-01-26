using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameZone : MonoBehaviour
{
    [SerializeField] private Collider2D zonaCollider;
    [SerializeField] private float reviveRadius = 0.1f;
    [SerializeField] private GameObject gameOverUI; // Assign Game Over UI in the Inspector
    [SerializeField] private GameObject player1WinsUI; // Assign Player 1 Wins UI in the Inspector



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the game zone");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            Debug.Log("Player exited the game zone");


            SaveHealth.Health--;
            if (SaveHealth.Health > 0)
            {
                StartCoroutine(RevivePlayer(other));
            }
            else
            {
                player1WinsUI.SetActive(true); // Activates the UI GameObject.
                Time.timeScale = 0f;
                Debug.Log("Player has no lives left and cannot return to the game.");
                other.gameObject.SetActive(false); // Deactivate the player
            }
        }

        if (other.CompareTag("Player1"))
        {

            Debug.Log("Player exited the game zone");


            SaveHealth.Health1--;
            Debug.Log(SaveHealth.Health1);
            if (SaveHealth.Health1 > 0)
            {
                StartCoroutine(RevivePlayer(other));
            }
            else
            {
                Debug.Log("Player has no lives left and cannot return to the game.");
                gameOverUI.SetActive(true); // Activates the UI GameObject.
                Time.timeScale = 0f;
                other.gameObject.SetActive(false); // Deactivate the player
            }

            if (other.CompareTag("AI1"))
            {
                Debug.Log("Player exited the game zone");


                SaveHealth.HealthIA1--;
                Debug.Log(SaveHealth.HealthIA1);
                if (SaveHealth.HealthIA1 > 0)
                {
                    StartCoroutine(RevivePlayer(other));
                }
                else
                {
                    player1WinsUI.SetActive(true); // Activates the UI GameObject.
                    Time.timeScale = 0f;
                    Debug.Log("Player has no lives left and cannot return to the game.");
                    other.gameObject.SetActive(false); // Deactivate the player
                }

            }

            if (other.CompareTag("AI2"))
            {
                Debug.Log("Player exited the game zone");


                SaveHealth.HealthIA2--;
                Debug.Log(SaveHealth.HealthIA2);
                if (SaveHealth.HealthIA2 > 0)
                {
                    StartCoroutine(RevivePlayer(other));
                }
                else
                {
                    player1WinsUI.SetActive(true); // Activates the UI GameObject.
                    Debug.Log("Player has no lives left and cannot return to the game.");
                    other.gameObject.SetActive(false); // Deactivate the player
                }

            }

            if (other.CompareTag("AI3"))
            {
                Debug.Log("Player exited the game zone");


                SaveHealth.HealthIA3--;
                Debug.Log(SaveHealth.HealthIA3);
                if (SaveHealth.HealthIA3 > 0)
                {
                    StartCoroutine(RevivePlayer(other));
                }
                else
                {
                    player1WinsUI.SetActive(true); // Activates the UI GameObject.
                    Debug.Log("Player has no lives left and cannot return to the game.");
                    other.gameObject.SetActive(false); // Deactivate the player
                }

            }

            SaveHealth.Save();

        }
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
            float randomX = Random.Range(-2f,2f);
            float randomY = Random.Range(-2f, 2f);
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
