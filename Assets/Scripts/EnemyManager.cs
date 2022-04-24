using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private GameManager _gameManager;
    
    private GameObject[] _enemy;
    private int _numberOfEnemies;

    private void Awake()
    {
        _enemy = GameObject.FindGameObjectsWithTag("Enemy");
        _gameManager = FindObjectOfType<GameManager>();

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
            _gameManager.CompleteLevel();
        }
    }
}