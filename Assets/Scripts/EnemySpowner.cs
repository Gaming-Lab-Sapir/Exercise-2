using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private int maxEnemies = 10;
    [SerializeField] private float spawnRadius = 0.9f;

    private int currentEnemies = 0;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            if (currentEnemies < maxEnemies)
            {
                Vector2 pos = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
                GameObject e = Instantiate(enemyPrefab, pos, Quaternion.identity);

                Enemy enemy = e.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.OnEnemyDestroyed += HandleEnemyDestroyed;
                }
                currentEnemies++;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void HandleEnemyDestroyed()
    {
        currentEnemies--;
        if (currentEnemies < 0) currentEnemies = 0;
    }
}
