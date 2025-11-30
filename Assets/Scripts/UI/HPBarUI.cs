using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.UI
{
    public class HPBarUI : MonoBehaviour
    {
        public Slider slider;

        /// <summary>
        /// BaseHealth から呼び出して拠点の HP を表示します。
        /// </summary>
        public void UpdateHP(int current, int max)
        {
            if (slider == null)
            {
                return;
            }

            slider.maxValue = max;
            slider.value = current;
        }
    }
}
