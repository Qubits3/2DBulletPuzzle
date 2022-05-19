using UnityEngine;
using Core;

namespace Player
{
    public class PlayerRagdoll : MonoBehaviour
    {
        private PlayerLife _playerLife;
        private ParticleSystem _bleedingParticleSystem;

        private void Awake()
        {
            GameObject parent = transform.parent.gameObject;
            _playerLife = parent.FindGameObjectInParentWithTag("Player").GetComponent<PlayerLife>();
            _bleedingParticleSystem = parent.FindGameObjectInParent("Bleeding").GetComponent<ParticleSystem>();
        }
        private void OnEnable()
        {
            PredictBulletHitPoint();
        }
        private void PredictBulletHitPoint()
        {
            Vector2 bulletPos = _playerLife.bulletPos;
            var hit = Physics2D.BoxCast(bulletPos, new Vector2(0.1f, 0.1f), 0f, Vector2.zero, 0.1f,
                LayerMask.GetMask("Default"));
            if (hit.collider)
            {
                ApplyBleeding(hit.collider.gameObject, bulletPos);
            }
        }
        private void ApplyBleeding(GameObject shootedBodyPart, Vector2 hitPos)
        {
            _bleedingParticleSystem.transform.position = hitPos;
            _bleedingParticleSystem.transform.SetParent(shootedBodyPart.transform);
            _bleedingParticleSystem.Play();
        }
    }

}
