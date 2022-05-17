using UnityEngine;

namespace Environment
{
    [CreateAssetMenu(fileName = "PropReferences", menuName = "ScriptableObjects/PropReferences", order = 1)]
    public class PropReferences : ScriptableObject
    {
        public GameObject[] bushes;
        public GameObject[] flowers;
        public GameObject[] grasses;
        public GameObject[] rocks;
        public GameObject[] trees;

        public GameObject[] clouds;

        public GameObject GetRandomBush()
        {
            return bushes[Random.Range(0, bushes.Length)];
        }

        public GameObject GetRandomFlower()
        {
            return flowers[Random.Range(0, flowers.Length)];
        }

        public GameObject GetRandomGrass()
        {
            return grasses[Random.Range(0, grasses.Length)];
        }

        public GameObject GetRandomRock()
        {
            return rocks[Random.Range(0, rocks.Length)];
        }

        public GameObject GetRandomTree()
        {
            return trees[Random.Range(0, trees.Length)];
        }

        public GameObject GetRandomCloud()
        {
            return clouds[Random.Range(0, clouds.Length)];
        }
    }
}