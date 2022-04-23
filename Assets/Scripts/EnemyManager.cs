using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private GameObject[] _enemy;
    private int _numberOfEnemies;

    private void Awake()
    {
        _enemy = GameObject.FindGameObjectsWithTag("Enemy");
        Enemy.OnEnemyDestroy += OnEnemyDead;
    }

    private void Start()
    {
        _numberOfEnemies = _enemy.Length;
    }

    private void OnEnemyDead()
    {
        _numberOfEnemies--;
        if (_numberOfEnemies == 0)
        {
            LevelManager.Instance.NextScene();
        }
    }
}