using UnityEngine;
using TowerDefense.Core;

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
        [SerializeField] private WaveManager waveManager;
        [SerializeField] private StageManager stageManager;
        [SerializeField] private BaseHealth baseHealth;
        [SerializeField] private UI.SelectionUI selectionUI;

        [SerializeField] private int maxWaves = 3;

        public GameState CurrentState { get; private set; } = GameState.Preparing;
        private int currentWave = 0;

        private void Start()
        {
            StartGame();
        }

        public void StartGame()
        {
            CurrentState = GameState.Playing;
            stageManager?.InitStage();
            currentWave = 0;
            StartNextWave();
        }

        public void StartNextWave()
        {
            if (CurrentState != GameState.Playing)
            {
                return;
            }

            currentWave++;
            if (currentWave > maxWaves)
            {
                OnGameClear();
                return;
            }

            waveManager?.StartWave(currentWave);
        }

        public void OnWaveClear()
        {
            if (CurrentState != GameState.Playing)
            {
                return;
            }

            // In a fuller implementation, selection UI would appear here.
            StartNextWave();
        }

        public void OnGameOver()
        {
            if (CurrentState == GameState.GameOver)
            {
                return;
            }

            CurrentState = GameState.GameOver;
        }

        public void OnGameClear()
        {
            if (CurrentState == GameState.Clear)
            {
                return;
            }

            CurrentState = GameState.Clear;
        }
    }
}
