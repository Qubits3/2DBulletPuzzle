using System.Collections;
using UnityEngine;

namespace Obstacle
{
    public class Obstacle : MonoBehaviour
    {
        private bool _isMoving;

        private ObstacleManager _obstacleManager;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _obstacleManager = FindObjectOfType<ObstacleManager>();

            StartCoroutine(IsThisObjectMoving());
        }

        private IEnumerator IsThisObjectMoving()
        {
            var firstPos = gameObject.transform.position;
            yield return new WaitForSeconds(0.1f);
            var secondPos = gameObject.transform.position;

            if ((firstPos != secondPos) != _isMoving)
            {
                if (_isMoving)
                {
                    _obstacleManager.RemoveObstacle();
                }
                else
                {
                    _obstacleManager.AddObstacle();
                }
            }

            _isMoving = firstPos != secondPos;

            StartCoroutine(IsThisObjectMoving());
        }
    }
}