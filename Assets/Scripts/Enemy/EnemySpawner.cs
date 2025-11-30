using UnityEngine;
using TowerDefense.Core;

namespace TowerDefense.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private EnemyController enemyPrefab;
        [SerializeField] private Transform baseTarget;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private WaveManager waveManager;

        public void SpawnEnemy()
        {
            if (enemyPrefab == null || spawnPoints == null || spawnPoints.Length == 0)
            {
                return;
            }

            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            EnemyController enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            enemy.Initialize(gameManager, waveManager, baseTarget);
        }
    }
}
