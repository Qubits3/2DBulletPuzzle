﻿using System;
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
                if (!GameObject.FindWithTag("Player"))
                {
                    SpawnPrefab("Prefabs/Player").transform.position = new Vector2(-1.5f, -0.15f);
                }

                if (!GameObject.FindWithTag("Enemy"))
                {
                    SpawnPrefab("Prefabs/Enemy").transform.position = new Vector2(1.5f, -0.15f);
                }

                SpawnPrefab("Prefabs/Managers");
                SpawnPrefab("Prefabs/InGameUI");

                SpawnObject("Prefabs/Grid", "Grid");
            }

            GUILayout.Label("Background");

            GUILayout.BeginHorizontal();
            {
                BackgroundButton("Blue");
                BackgroundButton("Brown");
                BackgroundButton("Gray");
                BackgroundButton("Green");
                BackgroundButton("Pink");
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            {
                BackgroundButton("Purple");
                BackgroundButton("Yellow");
            }
            EditorGUILayout.EndHorizontal();
        }

        private void SpawnEssentials()
        {
            SpawnPrefab("Prefabs/Player");
            SpawnPrefab("Prefabs/Managers");

            var enemy = SpawnPrefab("Prefabs/Enemy");
            if (enemy != null) enemy.transform.position = new Vector2(0, 1);

            SpawnObject("Prefabs/Grid", "Grid");
        }

        private void BackgroundButton(string color)
        {
            if (GUILayout.Button(Resources.Load<Texture2D>($"Textures/{color}"), GUIStyle.none))
            {
                try
                {
                    SpawnBackground(color);
                }
                catch (NullReferenceException)
                {
                    SpawnEssentials();
                    SpawnBackground(color);
                    Debug.Log("Spawned Essentials");
                }
            }
        }

        private void SpawnBackground(string color)
        {
            DestroyImmediate(GameObject.FindWithTag("Background"));
            SpawnPrefab($"Prefabs/Backgrounds/{color}Background").transform.parent =
                GameObject.Find("Grid").gameObject.transform;
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