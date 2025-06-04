using UnityEngine;

using TMPro;

using Assets.Scripts.Game;
using Assets.Scripts.Animals;
using Assets.Scripts.Objects;

namespace Assets.Scripts.UI
{
    public class ContentFieldSlot : Button
    {
        public SlotData data;

        public SpriteRenderer previewImage;
        public TMP_Text slotName;
        public TMP_Text slotCost;

        [HideInInspector]
        public ContentField contentField;

        public void SetSlot()
        {
            slotName.text = data.slotName;
            previewImage.sprite = data.sprite;

            switch (contentField.content)
            {
                case Content.Adopt:
                case Content.Food_Toys:
                    slotCost.text = $"${data.slotCost}";
                    break;
            }
        }

        void OnMouseUpAsButton()
        {
            switch (contentField.content)
            {
                case Content.Adopt:
                    Player.SpawnAnimal(new AnimalData("new animal", data.animalData.animal));
                    break;
                case Content.MyPets:
                    Player.SpawnAnimal(data.animalData);
                    contentField.UpdateContent();
                    break;
            }
        }
    }

    public struct SlotData
    {
        public readonly string slotName;
        public readonly Sprite sprite;
        public readonly int slotCost;

        public readonly AnimalData animalData;

        public SlotData(AnimalData animalData)
        {
            this.animalData = animalData;

            slotName = animalData.name;
            sprite = (Sprite)Resources.LoadAll($"Animals/Sprites/{animalData.animal}{animalData.variant + 1}")[2];
            slotCost = Resources.Load<AnimalSO>($"Animals/{animalData.animal}").cost;
        }

        public SlotData(FoodSO foodSO)
        {
            animalData = new AnimalData();

            slotName = foodSO.food.ToString();
            sprite = foodSO.sprite;
            slotCost = foodSO.cost;
        }

        public SlotData(ToySO toySO)
        {
            animalData = new AnimalData();

            slotName = toySO.toy.ToString();
            sprite = toySO.sprite;
            slotCost = toySO.cost;
        }
    }
}