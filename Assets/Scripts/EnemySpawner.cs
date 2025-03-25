using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 2f; // Time between spawns
    [SerializeField] private float spawnRadius = 10f; // Radius within which enemies will spawn
    [SerializeField] private float playerSafeRadius = 3f;
    [SerializeField] private Transform playerTransform;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true) // Spawn continuously
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        // Generate a random position within the spawn radius
        Vector3 randomPosition;
        bool positionValid = false;
        while (!positionValid)
        {
            randomPosition = Random.insideUnitSphere * spawnRadius;
            randomPosition.z = 0;
            randomPosition += transform.position; // Offset by the spawner's position

            if (Vector3.Distance(randomPosition, playerTransform.position) > playerSafeRadius)
            {
                Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
                positionValid = true;
            }
        }
    }

    // Optional: Draw a gizmo to visualize the spawn radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(playerTransform.position, playerSafeRadius);
    }
}
