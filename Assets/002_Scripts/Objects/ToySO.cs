using UnityEngine;

namespace Assets.Scripts.Objects
{
    public enum ToyClass
    {

    }

    [CreateAssetMenu()]
    public class ToySO : ScriptableObject
    {
        public Sprite sprite;
        public ToyClass toy;

        public int numberOfUses;

        public int cost;
    }
}