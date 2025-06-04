using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.Animals;

namespace Assets.Scripts.Game
{
    public class Player : MonoBehaviour
    {
        public static string playerName;
        public static int currency;

        public static GameObject animalPrefab;

        public static List<AnimalData> animalsStored = new List<AnimalData>();
        public static List<AnimalData> animalsInScene = new List<AnimalData>();

        public static List<AnimalData> adoptAnimals = new List<AnimalData>()
        {
            new AnimalData(AnimalClass.Bat),
            new AnimalData(AnimalClass.Butterfly),
            new AnimalData(AnimalClass.Cat),
            new AnimalData(AnimalClass.Chicken),
            new AnimalData(AnimalClass.Dog),
            new AnimalData(AnimalClass.Eagle),
            new AnimalData(AnimalClass.Goat),
            new AnimalData(AnimalClass.Mouse),
            new AnimalData(AnimalClass.Pigeon),
            new AnimalData(AnimalClass.Rabbit),
            new AnimalData(AnimalClass.Ram),
            new AnimalData(AnimalClass.Seagull),
            new AnimalData(AnimalClass.Swan)
        };

        public static List<GameObject> FoodInScene = new List<GameObject>();
        public static List<GameObject> ToysInScene = new List<GameObject>();

        void Awake()
        {
            LoadData();
            StartCoroutine(CurrencyUpdater());
        }

        void LoadData()
        {
            animalPrefab = (GameObject)Resources.Load("Animals/Animal Prefab");

            PlayerData data = SaveSystem.LoadPlayer();

            playerName = data.name;

            // TODO: send message asking for name

            animalsInScene = data.animalsInScene;
            animalsStored = data.animalsStored;

            if (animalsStored == null)
                animalsStored = new List<AnimalData>();
            if (animalsInScene == null)
                animalsInScene = new List<AnimalData>();

            foreach (AnimalData animalData in animalsInScene)
                SpawnAnimal(animalData);
        }

        IEnumerator CurrencyUpdater()
        {
            yield return new WaitForSeconds(2);
        }

        public static Animal SpawnAnimal(AnimalData animalData)
        {
            Animal animal = Instantiate(animalPrefab, new Vector2(animalData.positionX, animalData.positionY), Quaternion.identity).GetComponent<Animal>();
            animal.animalData = animalData;
            
            animalsInScene.Add(animalData);

            if (animalsStored.Contains(animalData))
                animalsStored.Remove(animalData);

            return animal;
        }

        public static PlayerData GetPlayerData()
        {
            return new PlayerData(playerName, currency, animalsInScene, animalsStored);
        }

        void OnApplicationQuit()
        {
            SaveSystem.SavePlayer();
        }
    }
}