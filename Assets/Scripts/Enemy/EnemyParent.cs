using UnityEngine;

namespace Enemy
{
    public class EnemyParent : MonoBehaviour
    {
        private GameObject _ragdollEnemy;
        private GameObject _enemy;
        
        public delegate void OnDestroyAction();

        public static event OnDestroyAction OnEnemyDestroy;

        private void Awake()
        {
            _ragdollEnemy = FindObjectInParent(gameObject, "RagdollFox");
            _enemy = FindObjectInParent(gameObject, "Enemy");

            _enemy.GetComponent<Enemy>().OnTriggerEnemy += OnEnemyHit;
        }

        private void OnEnemyHit()
        {
            _enemy.SetActive(false);
            _ragdollEnemy.SetActive(true);

            DestroyEnemy();
        }

        private GameObject FindObjectInParent(GameObject parent, string objectName)
        {
            Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
            foreach (Transform t in trs)
            {
                if (t.name == objectName)
                {
                    return t.gameObject;
                }
            }

            return null;
        }

        private void DestroyEnemy()
        {
            // gameObject.GetComponent<SpriteRenderer>().enabled = false;
            // gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            
            OnEnemyDestroy?.Invoke();
        }
    }
}