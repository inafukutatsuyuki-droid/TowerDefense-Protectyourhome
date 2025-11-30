using System.Collections;
using UnityEngine;
using TowerDefense.Enemy;

namespace TowerDefense.Core
{
    public class WaveManager : MonoBehaviour
    {
        public EnemySpawner spawner;
        public int currentWave = 1;
        public int enemiesToSpawn = 5;
        public int aliveEnemies;
        public float spawnInterval = 1.5f;

        private GameManager gameManager;
        private bool isSpawning;

        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        public void StartWave(int waveNumber)
        {
            StopAllCoroutines();
            currentWave = waveNumber;
            enemiesToSpawn = Mathf.Max(3, waveNumber * 3);
            aliveEnemies = 0;
            isSpawning = true;
            StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine()
        {
            int spawned = 0;
            while (spawned < enemiesToSpawn)
            {
                EnemyData data = spawner != null ? spawner.GetRandomEnemyData() : null;
                spawner?.SpawnEnemy(data);
                spawned++;
                yield return new WaitForSeconds(spawnInterval);
            }

            isSpawning = false;
            CheckWaveClear();
        }

        public void OnEnemySpawned()
        {
            aliveEnemies++;
        }

        public void OnEnemyDead()
        {
            aliveEnemies = Mathf.Max(0, aliveEnemies - 1);
            CheckWaveClear();
        }

        private void CheckWaveClear()
        {
            if (!isSpawning && aliveEnemies <= 0)
            {
                gameManager?.OnWaveClear();
            }
        }
    }
}
