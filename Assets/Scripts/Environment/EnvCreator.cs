#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System;
using Assets.Scripts.Environment;
using UnityEditor.SceneManagement;

namespace Environment
{
#if UNITY_EDITOR
    public class EnvCreator : MonoBehaviour
    {
        public PropReferences propReferences;

        private BoxCollider2D _collider;
        private GameObject _props;
        private float _spawnRegion;

        private void Awake()
        {
            SetReferences();
        }

        private void OnValidate()
        {
            SetReferences();
        }

        private void SetReferences()
        {
            _collider = GetComponent<BoxCollider2D>();

            _spawnRegion = _collider.bounds.size.x;
        }

        public void SpawnProp(Props prop, int propCount)
        {
            CreatePropsGameObject();

            SpawnPrefab(prop, -(_spawnRegion / 2), _spawnRegion / 2, _props, propCount);
        }

        private void DeleteObjectsWithTag(string tag)
        {
            foreach (var o in GameObject.FindGameObjectsWithTag(tag))
            {
                DestroyImmediate(o);
            }

            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }

        private GameObject[] SpawnPrefab(Props tag, float minPos, float maxPos, GameObject _parent, int spawnCount)
        {
            DeleteObjectsWithTag(Enum.GetName(typeof(Props), tag));

            int i;
            for (i = 0; i < spawnCount; i++)
            {
                var o = PrefabUtility.InstantiatePrefab(propReferences.GetRandomProp(tag), _parent.transform) as GameObject;
                o.transform.position = new Vector3(UnityEngine.Random.Range(minPos, maxPos), gameObject.transform.position.y);
                EditorUtility.SetDirty(o);
            }

            return new GameObject[i];
        }

        public void ClearProps(string[] props)
        {
            foreach (var prop in props)
            {
                DeleteObjectsWithTag(prop);
            }
        }

        private void CreatePropsGameObject()
        {
            if (!GameObject.Find("Props"))
            {
                _props = CreateGameObject("Props");
            }
            else
            {
                _props = GameObject.Find("Props");
            }
        }

        private GameObject CreateGameObject(string _name)
        {
            return new GameObject(_name);
        }
    }
}
#endif
