using UnityEngine;
using TowerDefense.Enemy;

namespace TowerDefense.Tower
{
    public class TowerController : MonoBehaviour
    {
        [SerializeField] private TowerData data;
        [SerializeField] private Transform firePoint;

        private EnemyController currentTarget;
        private float attackTimer;

        public TowerData Data => data;

        private void Update()
        {
            UpdateTarget();
            TryAttack();
        }

        public void UpdateTarget()
        {
            EnemyController[] enemies = FindObjectsOfType<EnemyController>();
            float closestDistance = float.MaxValue;
            EnemyController closestEnemy = null;

            foreach (EnemyController enemy in enemies)
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);
                if (distance <= data.range && distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }

            currentTarget = closestEnemy;
        }

        public void TryAttack()
        {
            if (currentTarget == null || data == null || data.bulletPrefab == null)
            {
                return;
            }

            attackTimer += Time.deltaTime;
            if (attackTimer < data.fireInterval)
            {
                return;
            }

            attackTimer = 0f;
            BulletController bullet = Instantiate(data.bulletPrefab, firePoint != null ? firePoint.position : transform.position, Quaternion.identity);
            bullet.SetTarget(currentTarget, data.damage);
        }

        public void UpgradeTo(TowerData newData)
        {
            data = newData;
        }
    }
}
