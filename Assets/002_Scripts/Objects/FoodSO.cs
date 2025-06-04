using UnityEngine;

namespace Assets.Scripts.Objects
{
    public enum FoodClass
    {

    }

    [CreateAssetMenu()]
    public class FoodSO : ScriptableObject
    {
        public Sprite sprite;
        public FoodClass food;

        public int cost;
    }
}