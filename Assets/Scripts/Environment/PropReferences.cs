using UnityEngine;
using Assets.Scripts.Environment;

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

        public GameObject GetRandomProp(Props prop)
        {
            return prop switch
            {
                Props.Bush => bushes[Random.Range(0, bushes.Length)],
                Props.Flower => flowers[Random.Range(0, flowers.Length)],
                Props.Grass => grasses[Random.Range(0, grasses.Length)],
                Props.Rock => rocks[Random.Range(0, rocks.Length)],
                Props.Tree => trees[Random.Range(0, trees.Length)],
                _ => null,
            };
        }

        public GameObject GetRandomCloud()
        {
            return clouds[Random.Range(0, clouds.Length)];
        }
    }
}