using UnityEngine;

namespace TowerDefense.Core
{
    public class StageManager : MonoBehaviour
    {
        [SerializeField] private int totalWaves = 3;

        public int TotalWaves => totalWaves;

        public void InitStage()
        {
            // Initialize stage-specific data such as wave configurations.
        }
    }
}
