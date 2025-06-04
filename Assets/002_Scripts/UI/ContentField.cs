using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.Game;

namespace Assets.Scripts.UI
{
    public enum Content
    {
        MyPets,
        Adopt,
        Food_Toys
    }

    public class ContentField : MonoBehaviour
    {
        public ContentFieldSlot[] slots;
        [Space]
        public ContentFieldArrow upButton;
        public ContentFieldArrow downButton;

        public List<SlotData> slotsData = new List<SlotData>();

        public Content content;

        int page;
        int maxPages;

        void Start()
        {
            slots = GetComponentsInChildren<ContentFieldSlot>();

            foreach (ContentFieldSlot slot in slots)
                slot.contentField = this;

            switch (content)
            {
                case Content.Adopt:
                    slotsData = AnimalsToSlots(Player.adoptAnimals);
                    break;
                case Content.MyPets:
                    slotsData = AnimalsToSlots(Player.animalsStored);
                    break;
                case Content.Food_Toys:
                    break;
            }

            UpdateContent();
            TurnPage(0);
        }

        List<SlotData> AnimalsToSlots(List<AnimalData> animalsData)
        {
            List<SlotData> slotsData = new List<SlotData>();

            foreach (AnimalData animalData in animalsData)
                slotsData.Add(new SlotData(animalData));

            return slotsData;
        }

        public void TurnPage(int direction)
        {
            page += direction;

            upButton.SetInteractable();
            downButton.SetInteractable();

            if (page <= 0)
            {
                page = 0;
                upButton.SetNotInteractable();
            }
            if (page >= maxPages)
            {
                page = maxPages;
                downButton.SetNotInteractable();
            }

            UpdateContent();
        }

        public void UpdateContent()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                int index = i + page * slots.Length;

                if (slotsData.Count > index)
                {
                    slots[i].gameObject.SetActive(true);
                    slots[i].data = slotsData[index];
                }
                else
                    slots[i].gameObject.SetActive(false);

                slots[i].SetSlot();
            }

            maxPages = slotsData.Count / slots.Length;
        }
    }
}