using UnityEngine;
using UnityEngine.UI;
using TowerDefense.Core;

namespace TowerDefense.UI
{
    public class HPBarUI : MonoBehaviour
    {
        [SerializeField] private Slider hpSlider;
        [SerializeField] private BaseHealth baseHealth;

        private void Update()
        {
            UpdateHP();
        }

        public void UpdateHP()
        {
            if (hpSlider == null || baseHealth == null)
            {
                return;
            }

            hpSlider.maxValue = baseHealth.MaxHP;
            hpSlider.value = baseHealth.CurrentHP;
        }
    }
}
