using UnityEngine;

namespace Assets.Scripts.Objects
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class Food : MonoBehaviour
    {
        public FoodSO foodSO;

        SpriteRenderer _spriteRenderer;

        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = foodSO.sprite;
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            
        }
    }
}