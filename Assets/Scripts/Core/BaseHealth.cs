using UnityEngine;

namespace TowerDefense.Core
{
    public class BaseHealth : MonoBehaviour
    {
        [SerializeField] private int maxHP = 100;
        [SerializeField] private GameManager gameManager;

        public int CurrentHP { get; private set; }
        public int MaxHP => maxHP;

        private void Awake()
        {
            CurrentHP = maxHP;
        }

        public void TakeDamage(int amount)
        {
            CurrentHP = Mathf.Max(0, CurrentHP - Mathf.Max(0, amount));

            if (CurrentHP <= 0)
            {
                gameManager?.OnGameOver();
            }
        }
    }
}
