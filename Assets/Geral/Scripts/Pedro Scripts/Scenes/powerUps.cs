using System.Collections;
using UnityEngine;

public class powerUps : MonoBehaviour
{
    public GameObject[] powerUpsPrefabs;
    public float[] spawnTimes;

    private bool[] powerUpsSpawned;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (powerUpsPrefabs.Length != spawnTimes.Length)
        {
            Debug.LogError("powerUpsPrefabs and spawnTimes must have the same length");
            return;
        }

        powerUpsSpawned = new bool[powerUpsPrefabs.Length];

        foreach(var powerUp in powerUpsPrefabs)
        {
            powerUp.SetActive(false);
        }

        StartCoroutine(SpawnPowerUps());
    }

    IEnumerator SpawnPowerUps()
    {
        for (int i = 0; i < spawnTimes.Length; i++)
        {
            yield return new WaitForSeconds(spawnTimes[i]);
            if (!powerUpsSpawned[i])
            {
                GameObject powerUp = Instantiate(powerUpsPrefabs[i], powerUpsPrefabs[i].transform.position, Quaternion.identity);
                powerUp.SetActive(true);
                powerUpsSpawned[i] = true;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        // Verifica se algum power-up foi capturado e o desativa
        for (int i = 0; i < powerUpsPrefabs.Length; i++)
        {
            if (powerUpsSpawned[i] && powerUpsPrefabs[i] == null)
            {
                powerUpsSpawned[i] = false;
            }
        }
    }
}
  
