using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            if (GUILayout.Button("Open Save File Path"))
            {
                EditorUtility.RevealInFinder(
                    $"C:\\Users\\{System.Environment.UserName}\\AppData\\LocalLow\\DefaultCompany\\Bullet Puzzle 2D\\");
            }

            SingleScene();

            MultipleScene();
        }

        private void SingleScene()
        {
            if (SceneManager.sceneCount > 1)
            {
                GUILayout.Label("To see single tools open one scene", EditorStyles.boldLabel);
                return;
            }

            CenteredLabel("SINGLE SCENE TOOLS");

            if (GUILayout.Button("Spawn Essentials"))
            {
                if (!GameObject.FindWithTag("Player"))
                {
                    SpawnPrefab("Prefabs/Essentials/Player").transform.position = new Vector2(-1.5f, -0.15f);
                }

                if (!GameObject.FindWithTag("Enemy"))
                {
                    SpawnPrefab("Prefabs/Essentials/Enemy").transform.position = new Vector2(1.5f, -0.15f);
                }

                SpawnPrefab("Prefabs/Essentials/Managers");
                SpawnPrefab("Prefabs/Essentials/InGameUI");
                SpawnPrefab("Prefabs/Essentials/Environment");
            }

            // if (GUILayout.Button("Add Camera"))
            // {
            //     SpawnPrefab("Prefabs/Main Camera");
            // }

            GUILayout.Label("Background");
            {
                BackgroundButton();
            }
        }

        private void MultipleScene()
        {
            // CenteredLabel("MULTIPLE SCENE TOOLS");
            //
            // if (GUILayout.Button("Fix All Background Positions"))
            // {
            //     foreach (var background in GameObject.FindGameObjectsWithTag("Background"))
            //     {
            //         background.transform.position = new Vector3(0, 0, -1);
            //         EditorUtility.SetDirty(background);
            //         Debug.Log($"Fixed position of {background.name}");
            //     }
            // }
        }

        private void CenteredLabel(string text)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(text, EditorStyles.boldLabel);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private void SpawnEssentials()
        {
            SpawnPrefab("Prefabs/Essentials/Player");
            SpawnPrefab("Prefabs/Essentials/Managers");

            var enemy = SpawnPrefab("Prefabs/Essentials/Enemy");
            if (enemy != null) enemy.transform.position = new Vector2(0, 1);
        }

        private void BackgroundButton()
        {
            if (GUILayout.Button(Resources.Load<Texture2D>($"Textures/Environment/Backgrounds/Sky"), GUIStyle.none))
            {
                try
                {
                    SpawnBackground();
                }
                catch (NullReferenceException)
                {
                    SpawnEssentials();
                    SpawnBackground();
                    Debug.Log("Spawned Essentials");
                }
            }
        }

        private void SpawnBackground()
        {
            DestroyImmediate(GameObject.FindWithTag("Background"));
            SpawnPrefab($"Prefabs/Backgrounds/SkyBackground").transform.parent =
                GameObject.FindWithTag("Environment").gameObject.transform;
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