using System;
using System.Collections;
using Enemy;
using UnityEngine;

namespace Obstacle
{
    public class ObstacleManager : MonoBehaviour
    {
        private EnemyManager _enemyManager;
        private float _activeObstacleCount;

        private void Awake()
        {
            _enemyManager = FindObjectOfType<EnemyManager>();
        }

        public bool AreObstaclesGrounded()
        {
            return _activeObstacleCount == 0;
        }

        public void CheckObstacles()
        {
            if (_activeObstacleCount == 0)
            {
                if (_enemyManager)
                {
                    _enemyManager.IsEnemiesDead();
                }

                return;
            }

            Invoke(nameof(CheckObstacles), 0.5f);
        }

        public void AddObstacle()
        {
            _activeObstacleCount++;
        }

        public void RemoveObstacle()
        {
            _activeObstacleCount--;

            if (_activeObstacleCount == 0 && _enemyManager)
            {
                _enemyManager.IsEnemiesDead();
            }
        }
    }
}