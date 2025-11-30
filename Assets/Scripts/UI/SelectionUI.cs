using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TowerDefense.Core;
using TowerDefense.Tower;

namespace TowerDefense.UI
{
    public class SelectionUI : MonoBehaviour
    {
        [SerializeField] private Button[] selectionButtons;
        [SerializeField] private List<TowerData> towerPool;
        [SerializeField] private GameManager gameManager;

        private readonly List<TowerData> currentChoices = new();

        public void ShowRandomChoices()
        {
            currentChoices.Clear();
            if (towerPool == null || towerPool.Count == 0 || selectionButtons == null)
            {
                return;
            }

            for (int i = 0; i < selectionButtons.Length; i++)
            {
                TowerData choice = towerPool[Random.Range(0, towerPool.Count)];
                currentChoices.Add(choice);
                int index = i;
                selectionButtons[i].onClick.RemoveAllListeners();
                selectionButtons[i].onClick.AddListener(() => OnSelect(index));
                if (selectionButtons[i].TryGetComponent(out Text buttonText))
                {
                    buttonText.text = choice != null ? choice.name : "";
                }
            }
        }

        public void OnSelect(int index)
        {
            if (index < 0 || index >= currentChoices.Count)
            {
                return;
            }

            TowerData selected = currentChoices[index];
            // Notify GameManager or other system about the selected tower.
            gameManager?.SendMessage("OnTowerSelected", selected, SendMessageOptions.DontRequireReceiver);
        }
    }
}
