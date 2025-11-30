using UnityEngine;
using TowerDefense.Enemy;

namespace TowerDefense.Tower
{
    public class BulletController : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public int damage = 1;
        public EnemyController target;

        private void Update()
        {
            if (target == null)
            {
                Destroy(gameObject);
                return;
            }

            Vector3 direction = (target.transform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
            {
                target.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
