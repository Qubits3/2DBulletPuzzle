using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    private GameObject[] enemy;
    private int numberOfEnemies;
    private void Awake()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        EnemyLife.OnEnemyDestroy += EnemyDead;
    }
    private void Start()
    {
        numberOfEnemies = enemy.Length;
    }
    void EnemyDead()
    {
        numberOfEnemies--;
        if(numberOfEnemies == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
