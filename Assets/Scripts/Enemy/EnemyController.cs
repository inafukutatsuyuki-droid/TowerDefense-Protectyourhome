using UnityEngine;
using TowerDefense.Core;

namespace TowerDefense.Enemy
{
    [RequireComponent(typeof(Collider2D))]
    public class EnemyController : MonoBehaviour
    {
        [Header("Runtime data")]
        public EnemyData data;

        [SerializeField] private float moveSpeed = 1.5f;
        [SerializeField] private int maxHP = 10;

        private int currentHP;
        private Transform targetBase;
        private BaseHealth baseHealth;
        private WaveManager waveManager;

        /// <summary>
        /// EnemySpawner から生成後に呼び出して EnemyData をセットしてください。
        /// </summary>
        public void Setup(EnemyData enemyData)
        {
            data = enemyData;
            if (data != null)
            {
                moveSpeed = data.moveSpeed;
                maxHP = data.maxHP;
            }

            currentHP = maxHP;
        }

        private void Start()
        {
            targetBase = GameObject.FindWithTag("Base")?.transform;
            baseHealth = FindObjectOfType<BaseHealth>();
            waveManager = FindObjectOfType<WaveManager>();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            if (targetBase == null)
            {
                return;
            }

            Vector3 direction = (targetBase.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }

        public void TakeDamage(int amount)
        {
            currentHP -= Mathf.Max(0, amount);
            if (currentHP <= 0)
            {
                Die();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject == targetBase?.gameObject || other.TryGetComponent(out BaseHealth target))
            {
                int damage = data != null ? data.damageToBase : 10;
                (baseHealth ?? target)?.TakeDamage(damage);
                Die();
            }
        }

        private void Die()
        {
            waveManager?.OnEnemyDead();
            Destroy(gameObject);
        }
    }
}
