using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Tower
{
    public class TowerMergeManager : MonoBehaviour
    {
        public List<TowerController> placedTowers = new();

        /// <summary>
        /// 生成したタレットを登録してください。
        /// </summary>
        public void RegisterTower(TowerController tower)
        {
            if (tower != null && !placedTowers.Contains(tower))
            {
                placedTowers.Add(tower);
            }
        }

        /// <summary>
        /// baseTower を右クリックなどで指定し、同じ TowerData を持つ別タレットを探してマージします。
        /// </summary>
        public void TryMerge(TowerController baseTower)
        {
            if (baseTower == null || baseTower.data == null)
            {
                return;
            }

            TowerController match = placedTowers.Find(t => t != baseTower && t.data == baseTower.data);
            if (match == null || baseTower.data.nextLevelTower == null)
            {
                return;
            }

            Vector3 spawnPos = baseTower.transform.position;
            placedTowers.Remove(baseTower);
            placedTowers.Remove(match);
            Destroy(baseTower.gameObject);
            Destroy(match.gameObject);

            TowerData nextData = baseTower.data.nextLevelTower;
            if (nextData != null && nextData.towerPrefab != null)
            {
                GameObject obj = Instantiate(nextData.towerPrefab, spawnPos, Quaternion.identity);
                if (obj.TryGetComponent(out TowerController controller))
                {
                    controller.data = nextData;
                    RegisterTower(controller);
                }
            }
        }
    }
}
