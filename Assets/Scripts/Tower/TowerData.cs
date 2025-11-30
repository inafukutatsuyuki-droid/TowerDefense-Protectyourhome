using UnityEngine;

namespace TowerDefense.Tower
{
    /// <summary>
    /// ScriptableObject that stores tower parameters.
    /// TowerData の nextLevelTower を設定しておくと、Merge でこのデータに進化します。
    /// </summary>
    [CreateAssetMenu(fileName = "TowerData", menuName = "TowerDefense/Tower Data", order = 0)]
    public class TowerData : ScriptableObject
    {
        public string id;
        public string displayName;
        public int level = 1;
        public float damage = 1f;
        public float fireInterval = 1f;
        public float range = 3f;
        public BulletController bulletPrefab;
        public GameObject towerPrefab;
        public TowerData nextLevelTower;
    }
}
