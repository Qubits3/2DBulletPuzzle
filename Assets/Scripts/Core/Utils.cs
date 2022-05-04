using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public static class Utils
    {
        public static bool IsThisSceneMainMenu()
        {
            return SceneManager.GetActiveScene().buildIndex == 0;
        }

        public static object Print(this object anObject, string message = "")
        {
            Debug.Log($"{message}: {anObject}");

            return anObject;
        }
    }
}