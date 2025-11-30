using System.Collections;
using UnityEngine;

namespace TowerDefense.Core
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField] private Enemy.EnemySpawner enemySpawner;
        [SerializeField] private float spawnInterval = 1.5f;

        public int CurrentWave { get; private set; }
        public int EnemiesToSpawn { get; private set; }
        public int EnemiesSpawned { get; private set; }
        public int AliveEnemies { get; private set; }

        private GameManager gameManager;

        private void Awake()
        {
            gameManager = GetComponent<GameManager>();
        }

        public void StartWave(int waveNumber)
        {
            CurrentWave = waveNumber;
            EnemiesSpawned = 0;
            AliveEnemies = 0;
            EnemiesToSpawn = Mathf.Max(3, waveNumber * 3);

            if (enemySpawner != null)
            {
                StartCoroutine(SpawnRoutine());
            }
        }

        public void RegisterEnemyDeath()
        {
            AliveEnemies = Mathf.Max(0, AliveEnemies - 1);
            CheckWaveEnd();
        }

        public void CheckWaveEnd()
        {
            if (EnemiesSpawned >= EnemiesToSpawn && AliveEnemies <= 0)
            {
                gameManager?.OnWaveClear();
            }
        }

        private IEnumerator SpawnRoutine()
        {
            while (EnemiesSpawned < EnemiesToSpawn)
            {
                enemySpawner.SpawnEnemy();
                EnemiesSpawned++;
                AliveEnemies++;
                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }
}
