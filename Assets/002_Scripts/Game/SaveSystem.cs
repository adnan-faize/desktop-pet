using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

using Assets.Scripts.Animals;

using Random = UnityEngine.Random;

namespace Assets.Scripts.Game
{
    public static class SaveSystem
    {
        public static void SavePlayer()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/player.file";
            FileStream stream = new FileStream(path, FileMode.Create);
            PlayerData data = Player.GetPlayerData();
            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static PlayerData LoadPlayer()
        {
            string path = Application.persistentDataPath + "/player.file";

            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                PlayerData data = (PlayerData)formatter.Deserialize(stream);
                stream.Close();
                return data;
            }
            else
                return new PlayerData();
        }
    }

    [Serializable]
    public struct PlayerData
    {
        public readonly string name;
        public readonly int currency;
        public readonly List<AnimalData> animalsInScene;
        public readonly List<AnimalData> animalsStored;

        public PlayerData(string name, int currency, List<AnimalData> animalsInScene, List<AnimalData> animalsStored)
        {
            this.name = name;
            this.currency = currency;
            this.animalsInScene = animalsInScene;
            this.animalsStored = animalsStored;
        }
    }

    [Serializable]
    public struct AnimalData
    {
        public readonly string name;
        public readonly AnimalClass animal;
        public readonly int variant;

        public readonly float positionX;
        public readonly float positionY;

        public AnimalData(string name, AnimalClass animal, int variant, Vector2 position)
        {
            this.name = name;
            this.animal = animal;
            this.variant = variant;
            positionX = position.x;
            positionY = position.y;
        }

        public AnimalData(string name, AnimalClass animal)
        {
            this.name = name;
            this.animal = animal;

            AnimalSO animalSO = (AnimalSO)Resources.Load($"Animals/{animal}");
            variant = Random.Range(0, animalSO.animatorControllers.Length);

            Vector2 pos = GameManager.GetRandomPos();
            positionX = pos.x;
            positionY = pos.y;
        }

        public AnimalData(AnimalClass animal)
        {
            name = animal.ToString();
            this.animal = animal;

            variant = 0;
            positionX = 0;
            positionY = 0;
        }
    }
}