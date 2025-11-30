using UnityEngine;
using TowerDefense.Core;

namespace TowerDefense.Enemy
{
    [RequireComponent(typeof(Collider2D))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float maxHp = 10f;
        [SerializeField] private float moveSpeed = 1.5f;
        [SerializeField] private Transform target;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private WaveManager waveManager;

        private float currentHp;

        private void Awake()
        {
            currentHp = maxHp;
        }

        private void Update()
        {
            Move();
        }

        public void Initialize(GameManager manager, WaveManager wave, Transform targetBase)
        {
            gameManager = manager;
            waveManager = wave;
            target = targetBase;
        }

        public void Move()
        {
            if (target == null)
            {
                return;
            }

            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }

        public void TakeDamage(float amount)
        {
            currentHp = Mathf.Max(0f, currentHp - Mathf.Max(0f, amount));
            if (currentHp <= 0f)
            {
                OnDead();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out BaseHealth baseHealth))
            {
                baseHealth.TakeDamage(10);
                Destroy(gameObject);
            }
        }

        private void OnDead()
        {
            waveManager?.RegisterEnemyDeath();
            Destroy(gameObject);
        }
    }
}
