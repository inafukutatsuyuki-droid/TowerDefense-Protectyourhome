using UnityEngine;

namespace TowerDefense.Enemy
{
    /// <summary>
    /// ScriptableObject that holds enemy parameters. Attach it to enemy prefabs via EnemySpawner.
    /// EnemySpawner に EnemyData の配列をセットし、インスペクタから EnemyData を登録してください。
    /// </summary>
    [CreateAssetMenu(fileName = "EnemyData", menuName = "TowerDefense/Enemy Data", order = 0)]
    public class EnemyData : ScriptableObject
    {
        public string id;
        public string displayName;
        public int maxHP = 10;
        public float moveSpeed = 1.5f;
        public int damageToBase = 10;
        public GameObject prefab;
    }
}
