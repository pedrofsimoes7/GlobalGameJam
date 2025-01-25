using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class zonaJogo : MonoBehaviour

{
    [SerializeField] private Collider2D zonaCollider;
    [SerializeField] private float reviveRadious = 0.1f;
    private Dictionary<GameObject, int> playerExitCount = new Dictionary<GameObject, int>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            print("Player entrou na zona de jogo");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            print("Player saiu da zona de jogo");
            if (!playerExitCount.ContainsKey(other.gameObject))
            {
                playerExitCount[other.gameObject] = 0;
            }

            playerExitCount[other.gameObject]++;

            if (playerExitCount[other.gameObject] >= 3)
            {
                print("Player não pode mais retornar ao jogo");
                other.gameObject.SetActive(false);
            }
            else
            {
                StartCoroutine(revivePlayer(other));
            }
        }
    }
    

    private IEnumerator revivePlayer(Collider2D player)
    {
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        Vector2 randomPosition = GetValidRandomPositionInZone();
        player.transform.position = randomPosition;
        player.gameObject.SetActive(true);
        print("Player reviveu");
    }

    private Vector2 GetValidRandomPositionInZone()
    {
        if (zonaCollider == null)
        { 
            return Vector2.zero;
        }
        Bounds bounds = zonaCollider.bounds;
        Vector2 randomPosition;
        int attempts = 0;

        do
        {
            float randomX = Random.Range(bounds.min.x - reviveRadious, bounds.max.x - reviveRadious);
            float randomY = Random.Range(bounds.min.y - reviveRadious, bounds.max.y - reviveRadious);
            randomPosition = new Vector2(randomX, randomY);
            attempts++;
        } while (Physics2D.OverlapCircle(randomPosition, reviveRadious) != null && attempts < 100);
        
        if(attempts >= 100)
        {
            print("Não foi possível encontrar uma posição válida para reviver o jogador");
        }

        return randomPosition;
    }

  
    


}
