using UnityEngine;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class Button : MonoBehaviour
    {
        public Color defaultColor = Color.white;
        public Color hoverColor = Color.gray;
        public Color pressColor = Color.gray / 2;

        [Space]
        public SpriteRenderer spriteRenderer;

        [Space]
        public bool interactable = true;

        protected void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            interactable = true;
        }

        public void UpdateColor(Color color)
        {
            if(!spriteRenderer)
                spriteRenderer = GetComponent<SpriteRenderer>();

            defaultColor = color;
            spriteRenderer.color = defaultColor;
        }

        void OnMouseDown()
        {
            if (interactable)
                spriteRenderer.color = pressColor;
        }

        void OnMouseUp()
        {
            if (interactable)
                spriteRenderer.color = defaultColor;
        }

        void OnMouseEnter()
        {
            if (interactable)
                spriteRenderer.color = hoverColor;
        }

        void OnMouseExit()
        {
            if (interactable)
                spriteRenderer.color = defaultColor;
        }
    }
}