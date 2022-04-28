using UnityEngine.SceneManagement;

namespace Core
{
    public static class Utils
    {
        public static bool IsThisSceneMainMenu()
        {
            return SceneManager.GetActiveScene().buildIndex == 0;
        }
    }
}