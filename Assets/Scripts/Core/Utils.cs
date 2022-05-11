using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public static class Utils
    {
        public static bool IsMainMenu(this Scene scene)
        {
            return scene.buildIndex == 0;
        }

        public static object Print(this object anObject, string message = "")
        {
            if (message.Equals(""))
            {
                Debug.Log($"{anObject}");
                
                return anObject;
            }
            
            Debug.Log($"{message}: {anObject}");

            return anObject;
        }

        public static GameObject FindGameObjectInParent(this GameObject parent, string objectName)
        {
            Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
            foreach (Transform t in trs)
            {
                if (t.name == objectName)
                {
                    return t.gameObject;
                }
            }

            return null;
        }

        public static GameObject FindGameObjectInParentWithTag(this GameObject parent, string tag)
        {
            foreach (Transform t in parent.transform)
            {
                if (t.CompareTag(tag))
                {
                    return t.gameObject;
                }
            }

            return null;
        }
    }
}