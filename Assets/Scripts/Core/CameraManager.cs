using System;
using UnityEngine;

namespace Core
{
    public class CameraManager : MonoBehaviour
    {
        private void Awake()
        {
            SetOrthographicSize();
        }

        private void Update()
        {
            SetOrthographicSize();
        }

        private void SetOrthographicSize()
        {
            var currentAspect = (float) Screen.width / (float) Screen.height;
            Camera.main.orthographicSize = 1920 / currentAspect / 682.6f;
        }
    }
}
