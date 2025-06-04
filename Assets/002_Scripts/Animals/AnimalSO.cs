using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.Objects;

namespace Assets.Scripts.Animals
{
    public enum AnimalClass
    {
        Cat = 0,
        Dog = 1,
        Rabbit = 2,
        Ram = 3,
        Chicken = 4,
        Eagle = 5,
        Swan = 6,
        Seagull = 7,
        Pigeon = 8,
        Mouse = 9,
        Goat = 10,
        Butterfly = 11,
        Bat = 12,
    }

    [CreateAssetMenu()]
    public class AnimalSO : ScriptableObject
    {
        public AnimalClass animal;
        public bool canFly;
        public RuntimeAnimatorController[] animatorControllers;

        [Space]
        public float speed;
        public float maxSpeed;

        [Space]
        public int cost;

        [Space]
        public List<FoodClass> LoveFood = new List<FoodClass>();
    }
}