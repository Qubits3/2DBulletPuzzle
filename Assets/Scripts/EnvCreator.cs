#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;


#if UNITY_EDITOR
    public class EnvCreator : MonoBehaviour
    {
        private BoxCollider2D _collider;
        private GameObject _props;
        private float _spawnSize;

        private void Awake()
        {
            SetReferences();
        }

        private void SetReferences()
        {
            _collider = GetComponent<BoxCollider2D>();

            _spawnSize = _collider.bounds.size.x;
        }

        public void Create()
        {
            if (!GameObject.Find("Props"))
            {
                _props = CreateGameObject("Props");
            }

            SpawnPrefab("Prefabs/Environment/Trees/Tree-1", 5).transform.position = gameObject.transform.position;
        }

        private GameObject SpawnPrefab(string path, int spawnCount = 1)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                var o = PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>(path)) as GameObject;
                EditorUtility.SetDirty(o);

                return o;
            }

            return null;
        }

        private GameObject CreateGameObject(string _name)
        {
            return new GameObject(_name);
        }

        private void OnValidate()
        {
            SetReferences();
        }
    }
#endif
