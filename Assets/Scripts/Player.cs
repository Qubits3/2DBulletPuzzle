using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject _crosshair;

    private void Awake()
    {
        _crosshair = GameObject.FindWithTag("Crosshair");
    }

    private void Update()
    {
        DrawCrosshair();
    }

    private void DrawCrosshair()
    {
        if (!GameManager.SharedInstance.IsLevelCompleted)
        {
            _crosshair.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
        }
    }
}