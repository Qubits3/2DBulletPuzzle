using Core;
using UnityEngine;

namespace Enemy
{
    public class EnemyRagdoll : MonoBehaviour
    {
        private Enemy _enemy;
        private ParticleSystem bleedingParticleSystem;
        RaycastHit2D hit;

        private void Awake()
        {
            GameObject parent = transform.parent.gameObject;
            _enemy = parent.FindGameObjectInParentWithTag("Enemy").GetComponent<Enemy>();
            bleedingParticleSystem = parent.FindGameObjectInParent("Bleeding").GetComponent<ParticleSystem>();
        }
        
        private void OnEnable()
        {
            PredictBulletHitPoint();
        }

        private void PredictBulletHitPoint()
        {
            Vector2 bulletPos = _enemy.bulletPos;

            hit = Physics2D.BoxCast(bulletPos, new Vector2(0.1f, 0.1f), 0f, Vector2.zero, 0.1f,
                LayerMask.GetMask("IgnoreBullet"));

            if (hit.collider)
            {
                ApplyForceToHitPoint(hit.collider.gameObject.GetComponent<Rigidbody2D>(), bulletPos);
                ApplyBleeding(hit.collider.gameObject ,bulletPos);
            }
        }
        
        private void ApplyForceToHitPoint(Rigidbody2D rb, Vector2 point)
        {
            if (rb)
            {
                rb.AddForceAtPosition(point.normalized * Random.Range(300,600), point);
            }
        }
        private void ApplyBleeding(GameObject shootedBodyPart,Vector2 hitPos)
        {
            bleedingParticleSystem.transform.position = hitPos;
            bleedingParticleSystem.transform.rotation = Quaternion.FromToRotation(transform.forward, hit.normal) * transform.rotation;
            bleedingParticleSystem.transform.SetParent(shootedBodyPart.transform);
            bleedingParticleSystem.Play();
        }
    }
}