using UnityEngine;
using TowerDefense.UI;
using TowerDefense.Tower;

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
        private bool isAwaitingSelection;

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
                if (selectionUI != null)
                {
                    isAwaitingSelection = true;
                    CurrentState = GameState.Preparing;
                    selectionUI.ShowRandomChoices();
                }
                else
                {
                    StartNextWave();
                }
            }
            else
            {
                OnStageClear();
            }
        }

        public void OnTowerSelected(TowerData selected)
        {
            if (!isAwaitingSelection)
            {
                return;
            }

            isAwaitingSelection = false;
            CurrentState = GameState.Playing;
            StartNextWave();
        }

        public void OnGameOver()
        {
            isAwaitingSelection = false;
            CurrentState = GameState.GameOver;
            Debug.Log("Game Over");
        }

        public void OnStageClear()
        {
            isAwaitingSelection = false;
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
