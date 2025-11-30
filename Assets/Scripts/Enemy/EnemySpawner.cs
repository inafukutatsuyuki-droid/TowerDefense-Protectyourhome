using UnityEngine;
using TowerDefense.Core;

namespace TowerDefense.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [Tooltip("EnemyData をインスペクタから登録し、WaveManager で使用します。")]
        public EnemyData[] enemyPool;
        public Transform[] spawnPoints;

        private WaveManager waveManager;

        private void Awake()
        {
            waveManager = FindObjectOfType<WaveManager>();
        }

        public void SpawnEnemy(EnemyData data)
        {
            if (data == null || data.prefab == null || spawnPoints == null || spawnPoints.Length == 0)
            {
                return;
            }

            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject enemyObj = Instantiate(data.prefab, spawnPoint.position, Quaternion.identity);
            if (enemyObj.TryGetComponent(out EnemyController controller))
            {
                controller.Setup(data);
            }

            waveManager?.OnEnemySpawned();
        }

        public EnemyData GetRandomEnemyData()
        {
            if (enemyPool == null || enemyPool.Length == 0)
            {
                return null;
            }

            return enemyPool[Random.Range(0, enemyPool.Length)];
        }
    }
}
