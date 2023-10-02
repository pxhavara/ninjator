using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public Transform player;
    public float spawnInterval = 10f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true) // Infinite loop to keep spawning enemies
        {
            // Spawn an enemy at the spawn point
            GameObject go = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            go.GetComponent<EnemyAI>().player = player;
            // Wait for the specified interval before spawning the next enemy
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
