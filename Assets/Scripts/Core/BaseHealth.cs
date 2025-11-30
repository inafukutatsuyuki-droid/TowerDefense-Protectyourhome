using UnityEngine;
using TowerDefense.UI;

namespace TowerDefense.Core
{
    public class BaseHealth : MonoBehaviour
    {
        public int maxHP = 100;
        public HPBarUI hpBarUI;

        private int currentHP;
        private GameManager gameManager;

        private void Start()
        {
            currentHP = maxHP;
            gameManager = FindObjectOfType<GameManager>();
            hpBarUI?.UpdateHP(currentHP, maxHP);
        }

        public void TakeDamage(int amount)
        {
            currentHP -= Mathf.Max(0, amount);
            currentHP = Mathf.Max(0, currentHP);
            hpBarUI?.UpdateHP(currentHP, maxHP);

            if (currentHP <= 0)
            {
                gameManager?.OnGameOver();
            }
        }
    }
}
