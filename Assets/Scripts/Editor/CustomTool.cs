using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class CustomTool : EditorWindow
    {
        [MenuItem("Window/Custom Tool")]
        private static void ShowWindow()
        {
            var window = GetWindow<CustomTool>();
            window.titleContent = new GUIContent("Custom Tool");
            window.Show();
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Spawn Essentials"))
            {
                SpawnPrefab("Prefabs/Player");
                SpawnPrefab("Prefabs/Managers");
                SpawnPrefab("Prefabs/Enemy").transform.position = new Vector2(0,1);
                
                SpawnObject("Prefabs/Grid", "Grid");
            }
        }

        private GameObject SpawnPrefab(string path)
        {
            if (!GameObject.Find(Resources.Load(path).name))
            {
                var gameObject = PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>(path)) as GameObject;
                EditorUtility.SetDirty(gameObject);

                return gameObject;
            }

            return null;
        }

        private GameObject SpawnObject(string path, string objectName = "")
        {
            if (!GameObject.Find(Resources.Load(path).name))
            {
                var gameObject = Instantiate(Resources.Load<GameObject>(path));

                if (!objectName.Equals(""))
                {
                    gameObject.name = objectName;
                }

                EditorUtility.SetDirty(gameObject);

                return gameObject;
            }

            return null;
        }
    }
}