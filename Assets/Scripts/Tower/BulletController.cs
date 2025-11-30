using UnityEngine;
using TowerDefense.Enemy;

namespace TowerDefense.Tower
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        private EnemyController target;
        private float damage;

        public void SetTarget(EnemyController enemy, float damageAmount)
        {
            target = enemy;
            damage = damageAmount;
        }

        private void Update()
        {
            if (target == null)
            {
                Destroy(gameObject);
                return;
            }

            Vector3 direction = (target.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
            {
                target.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
