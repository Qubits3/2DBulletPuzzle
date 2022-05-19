#if UNITY_EDITOR
using UnityEditor;
#endif
using Environment;
using UnityEngine;

namespace Editor
{
#if UNITY_EDITOR
    [CustomEditor(typeof(EnvCreator))]
    public class EnvCreatorEditor : UnityEditor.Editor
    {
        private EnvCreator _creator;

        private void OnEnable()
        {
            _creator = (EnvCreator)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(10);

            if (GUILayout.Button("SPAWN ALL"))
            {
                _creator.SpawnBushes();
                _creator.SpawnFlower();
                _creator.SpawnGrasses();
                _creator.SpawnRocks();
                _creator.SpawnTrees();
            }

            GUILayout.Space(10);

            if (GUILayout.Button("Spawn Bushes"))
            {
                _creator.SpawnBushes();
            }

            GUILayout.Space(5);

            if (GUILayout.Button("Spawn Flowers"))
            {
                _creator.SpawnFlower();
            }

            GUILayout.Space(5);

            if (GUILayout.Button("Spawn Grasses"))
            {
                _creator.SpawnGrasses();
            }

            GUILayout.Space(5);

            if (GUILayout.Button("Spawn Rocks"))
            {
                _creator.SpawnRocks();
            }

            GUILayout.Space(5);

            if (GUILayout.Button("Spawn Trees"))
            {
                _creator.SpawnTrees();
            }

            GUILayout.Space(10);

            var style = new GUIStyle(EditorStyles.toolbarButton);

            style.normal.textColor = Color.red;

            if (GUILayout.Button("CLEAR ALL", style))
            {
                _creator.ClearScene(new string[] { "Bush", "Flower", "Grass", "Rock", "Tree" });
            }
        }
    }
#endif
}