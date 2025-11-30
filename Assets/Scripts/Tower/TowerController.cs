using UnityEngine;
using TowerDefense.Enemy;

namespace TowerDefense.Tower
{
    public class TowerController : MonoBehaviour
    {
        public TowerData data;
        public Transform firePoint;

        private EnemyController currentTarget;
        private float fireTimer;
        private float damage;
        private float fireInterval;
        private float range;

        private void Start()
        {
            if (data != null)
            {
                damage = data.damage;
                fireInterval = data.fireInterval;
                range = data.range;
            }
        }

        private void Update()
        {
            AcquireTarget();
            HandleAttack();
        }

        private void AcquireTarget()
        {
            EnemyController[] enemies = FindObjectsOfType<EnemyController>();
            float closestDist = float.MaxValue;
            EnemyController closest = null;

            foreach (EnemyController enemy in enemies)
            {
                float dist = Vector2.Distance(transform.position, enemy.transform.position);
                if (dist <= range && dist < closestDist)
                {
                    closestDist = dist;
                    closest = enemy;
                }
            }

            currentTarget = closest;
        }

        private void HandleAttack()
        {
            if (data == null || data.bulletPrefab == null || currentTarget == null)
            {
                return;
            }

            fireTimer += Time.deltaTime;
            if (fireTimer < fireInterval)
            {
                return;
            }

            fireTimer = 0f;
            Attack();
        }

        private void Attack()
        {
            BulletController bullet = Instantiate(data.bulletPrefab, firePoint != null ? firePoint.position : transform.position, Quaternion.identity);
            bullet.damage = Mathf.RoundToInt(damage);
            bullet.target = currentTarget;
        }

        public void UpgradeTo(TowerData newData)
        {
            data = newData;
            if (data != null)
            {
                damage = data.damage;
                fireInterval = data.fireInterval;
                range = data.range;
            }
        }
    }
}
