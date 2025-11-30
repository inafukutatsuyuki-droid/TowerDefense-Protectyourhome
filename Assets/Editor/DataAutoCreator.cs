#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using TowerDefense.Tower;
using TowerDefense.Enemy;

public static class DataAutoCreator
{
    private const string AssetFolder = "Assets/ScriptableObjects";

    [MenuItem("Tools/Create Prototype Data")]
    public static void CreatePrototypeData()
    {
        if (!AssetDatabase.IsValidFolder(AssetFolder))
        {
            AssetDatabase.CreateFolder("Assets", "ScriptableObjects");
        }

        CreateEnemy("Enemy_SlowTank", 30, 0.8f, 15);
        CreateEnemy("Enemy_FastRunner", 10, 2.5f, 8);
        CreateTower("Tower_Lv1", 1, 2f, 1.2f, 3.5f);
        CreateTower("Tower_Lv2", 2, 4f, 1f, 4f);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Prototype data created under Assets/ScriptableObjects.");
    }

    private static void CreateEnemy(string name, int hp, float speed, int baseDamage)
    {
        EnemyData asset = ScriptableObject.CreateInstance<EnemyData>();
        asset.id = name;
        asset.displayName = name;
        asset.maxHP = hp;
        asset.moveSpeed = speed;
        asset.damageToBase = baseDamage;
        AssetDatabase.CreateAsset(asset, $"{AssetFolder}/{name}.asset");
    }

    private static void CreateTower(string name, int level, float damage, float interval, float range)
    {
        TowerData asset = ScriptableObject.CreateInstance<TowerData>();
        asset.id = name;
        asset.displayName = name;
        asset.level = level;
        asset.damage = damage;
        asset.fireInterval = interval;
        asset.range = range;
        AssetDatabase.CreateAsset(asset, $"{AssetFolder}/{name}.asset");
    }
}
#endif
