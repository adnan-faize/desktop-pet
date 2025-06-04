using UnityEngine;

using Assets.Scripts.Game;

namespace Assets.Scripts.UI
{
    public class MainButton : Button
    {
        float positionX;
        float destinationX;

        public GameObject arrow;

        float panelPositionX;
        float panelDestinationX;
        public GameObject panel;

        const float ANIMATION_DURATION = 0.5f;

        new void Start()
        {
            base.Start();

            Vector2 pos = GameManager._cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height / 2));

            positionX = pos.x - GetComponent<BoxCollider2D>().size.x;
            transform.position = new Vector2(positionX, pos.y);

            panelPositionX = 0.1f + positionX + panel.GetComponent<SpriteRenderer>().size.x / 2;
            panel.transform.position = new Vector2(panelPositionX, pos.y);

            destinationX = positionX - panel.GetComponent<SpriteRenderer>().size.x;
            panelDestinationX = 0.1f + destinationX + panel.GetComponent<SpriteRenderer>().size.x / 2;
        }

        void OnMouseUpAsButton()
        {
            if (transform.position.x != positionX)
            {
                LeanTween.moveX(gameObject, positionX, ANIMATION_DURATION);
                LeanTween.moveX(panel, panelPositionX, ANIMATION_DURATION);
                LeanTween.rotateZ(arrow, 90, ANIMATION_DURATION);
            }
            else
            {
                LeanTween.moveX(gameObject, destinationX, ANIMATION_DURATION);
                LeanTween.moveX(panel, panelDestinationX, ANIMATION_DURATION);
                LeanTween.rotateZ(arrow, -90, ANIMATION_DURATION);
            }
        }
    }
}
