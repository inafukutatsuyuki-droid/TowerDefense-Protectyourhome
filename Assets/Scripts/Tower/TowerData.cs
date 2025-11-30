using UnityEngine;

namespace TowerDefense.Tower
{
    [CreateAssetMenu(fileName = "TowerData", menuName = "TowerDefense/Tower Data", order = 0)]
    public class TowerData : ScriptableObject
    {
        public string id;
        public int level;
        public float damage = 1f;
        public float fireInterval = 1f;
        public float range = 3f;
        public BulletController bulletPrefab;
        public TowerData nextLevelTower;
    }
}
