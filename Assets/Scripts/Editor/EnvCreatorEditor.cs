#if UNITY_EDITOR
using UnityEditor;
#endif
using Environment;
using UnityEngine;
using Assets.Scripts.Environment;
using System;

namespace Editor
{
#if UNITY_EDITOR
    [CustomEditor(typeof(EnvCreator))]
    public class EnvCreatorEditor : UnityEditor.Editor
    {
        private EnvCreator _creator;

        private int bushCount = 2;
        private int flowerCount = 5;
        private int grassCount = 10;
        private int rockCount = 5;
        private int treeCount = 2;

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
                _creator.SpawnProp(Props.Bush, bushCount);
                _creator.SpawnProp(Props.Flower, flowerCount);
                _creator.SpawnProp(Props.Grass, grassCount);
                _creator.SpawnProp(Props.Rock, rockCount);
                _creator.SpawnProp(Props.Tree, treeCount);
            }

            GUILayout.Space(10);

            bushCount = AddButton(Props.Bush, "Spawn Bushes", bushCount);
            flowerCount = AddButton(Props.Flower, "Spawn Flowers", flowerCount);
            grassCount = AddButton(Props.Grass, "Spawn Grasses", grassCount);
            rockCount = AddButton(Props.Rock, "Spawn Rocks", rockCount);
            treeCount = AddButton(Props.Tree, "Spawn Trees", treeCount);

            var style = new GUIStyle(EditorStyles.toolbarButton);

            style.normal.textColor = Color.red;

            if (GUILayout.Button("CLEAR ALL", style))
            {
                _creator.ClearProps(Enum.GetNames(typeof(Props)));
            }
        }

        private int AddButton(Props prop, string buttonName, int propCount)
        {
            propCount = AddSlider(Enum.GetName(typeof(Props), prop), propCount);

            if (GUILayout.Button(buttonName))
            {
                _creator.SpawnProp(prop, propCount);
            }

            GUILayout.Space(5);

            return propCount;
        }

        private int AddSlider(string label, int scale)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(label);
            scale = (int)EditorGUILayout.Slider(scale, 0, 20);
            GUILayout.EndHorizontal();

            return scale;
        }
    }
#endif
}