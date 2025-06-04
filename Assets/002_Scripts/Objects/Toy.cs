using UnityEngine;

namespace Assets.Scripts.Objects
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class Toy : MonoBehaviour
    {
        public ToySO toySO;

        SpriteRenderer _spriteRenderer;

        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = toySO.sprite;
        }

        void OnTriggerEnter2D(Collider2D collision)
        {

        }
    }
}