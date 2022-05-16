using System;
using Core;
#if UNITY_EDITOR
using UnityEditor;
#endif
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
            _creator = (EnvCreator) target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Spawn Trees"))
            {
                _creator.SpawnTrees();
            }

            GUILayout.Space(10);

            if (GUILayout.Button("Spawn Grasses"))
            {
                _creator.SpawnGrasses();
            }
        }
    }
#endif
}