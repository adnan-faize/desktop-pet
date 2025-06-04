using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ContentFieldArrow : Button
    {
        public int direction;
        public ContentField contentField;

        public void SetNotInteractable()
        {
            interactable = false;
            UpdateColor(Color.grey / 2);
        }

        public void SetInteractable()
        {
            interactable = true;
            UpdateColor(Color.white);
        }

        void OnMouseUpAsButton()
        {
            if (interactable)
                contentField.TurnPage(direction);
        }
    }
}