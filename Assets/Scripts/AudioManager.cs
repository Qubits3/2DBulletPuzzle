using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private BulletThrower _bulletThrower;
    private AudioSource _audioSource;
    private AudioClip _shotSound;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        _shotSound = Resources.Load<AudioClip>("Audio/ShotSound");

        _bulletThrower = FindObjectOfType<BulletThrower>();
        _bulletThrower.OnCreateBullet += PlayShotSound;
    }

    private void PlayShotSound()
    {
        _audioSource.pitch = Random.Range(0.9f, 1.1f);
        _audioSource.PlayOneShot(_shotSound);
    }

    private void OnDestroy()
    {
        _bulletThrower.OnCreateBullet -= PlayShotSound;
    }
}