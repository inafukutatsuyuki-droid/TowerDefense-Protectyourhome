using UnityEngine;
using TowerDefense.UI;

namespace TowerDefense.Core
{
    public enum GameState
    {
        Preparing,
        Playing,
        GameOver,
        Clear
    }

    public class GameManager : MonoBehaviour
    {
        public WaveManager waveManager;
        public StageManager stageManager;
        public BaseHealth baseHealth;
        public SelectionUI selectionUI;
        public int maxWave = 3;

        public GameState CurrentState { get; private set; } = GameState.Preparing;
        private int currentWave = 0;

        private void Start()
        {
            StartGame();
        }

        private void StartGame()
        {
            CurrentState = GameState.Playing;
            stageManager?.InitStage();
            currentWave = 0;
            StartNextWave();
        }

        public void OnWaveClear()
        {
            if (CurrentState != GameState.Playing)
            {
                return;
            }

            if (currentWave < maxWave)
            {
                selectionUI?.ShowRandomChoices();
                StartNextWave();
            }
            else
            {
                OnStageClear();
            }
        }

        public void OnGameOver()
        {
            CurrentState = GameState.GameOver;
            Debug.Log("Game Over");
        }

        public void OnStageClear()
        {
            CurrentState = GameState.Clear;
            Debug.Log("Stage Clear");
        }

        private void StartNextWave()
        {
            currentWave++;
            waveManager?.StartWave(currentWave);
        }
    }
}
