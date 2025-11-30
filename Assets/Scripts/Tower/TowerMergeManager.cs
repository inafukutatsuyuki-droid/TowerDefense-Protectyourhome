using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Tower
{
    public class TowerMergeManager : MonoBehaviour
    {
        [SerializeField] private Vector2Int gridSize = new Vector2Int(2, 3);
        [SerializeField] private float cellSize = 1.5f;
        [SerializeField] private TowerController towerPrefab;

        private readonly Dictionary<Vector2Int, TowerController> placedTowers = new();

        public bool PlaceTower(TowerController tower, Vector2Int gridPosition)
        {
            if (placedTowers.ContainsKey(gridPosition))
            {
                return false;
            }

            placedTowers[gridPosition] = tower;
            tower.transform.position = GridToWorld(gridPosition);
            MergeTowers();
            return true;
        }

        public void MergeTowers()
        {
            var toMerge = new Dictionary<TowerData, List<Vector2Int>>();

            foreach (var pair in placedTowers)
            {
                TowerController controller = pair.Value;
                if (controller == null || controller.Data == null)
                {
                    continue;
                }

                if (!toMerge.ContainsKey(controller.Data))
                {
                    toMerge[controller.Data] = new List<Vector2Int>();
                }

                toMerge[controller.Data].Add(pair.Key);
            }

            foreach (var group in toMerge)
            {
                if (group.Value.Count >= 2 && group.Key.nextLevelTower != null)
                {
                    Vector2Int first = group.Value[0];
                    Vector2Int second = group.Value[1];

                    Destroy(placedTowers[first].gameObject);
                    Destroy(placedTowers[second].gameObject);
                    placedTowers.Remove(first);
                    placedTowers.Remove(second);

                    TowerController newTower = Instantiate(towerPrefab);
                    newTower.UpgradeTo(group.Key.nextLevelTower);
                    newTower.transform.position = GridToWorld(first);
                    placedTowers[first] = newTower;
                    break;
                }
            }
        }

        private Vector3 GridToWorld(Vector2Int gridPos)
        {
            return new Vector3(gridPos.x * cellSize, gridPos.y * cellSize, 0f);
        }
    }
}
