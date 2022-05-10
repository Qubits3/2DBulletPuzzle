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

            if (GUILayout.Button("Create"))
            {
                _creator.Create();
            }
        }
    }
#endif
}