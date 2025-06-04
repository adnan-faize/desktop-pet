using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.Game;

namespace Assets.Scripts.Animals
{
    public enum Stats
    {
        Love,
        Happyness,
        Sadness,
        Madness,
        Hunger
    }

    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Animator))]
    public class Animal : MonoBehaviour
    {
        enum Directions
        {
            Down = 0,
            Left = 1,
            Right = 2,
            Up = 3
        }

        const float DIR_CHANGE_RATE = .9f;
        const float MOVE_CHANGE_RATE = .7f;
        const float FLY_CHANGE_RATE = .4f;
        const float MIN_CHANGE_WAIT_TIME = 1f;
        const float Max_CHANGE_WAIT_TIME = 10f;

        const float STATS_INFLUENCE_MULTIPLIER = .1f;
        const float STATS_INFLUENCE_OVER_TIME = 20f;

        Directions direction;
        Vector2Int dir;
        float speed = 10;
        float maxSpeed = 1;
        bool moving;
        bool flying;

        bool canChangeState = true;

        public AnimalData animalData;
        AnimalSO animalSO;

        Rigidbody2D _rb;
        BoxCollider2D _collider;
        Animator _animator;

        [SerializeField]
        ParticleSystem loveParticle;

        float hunger; // influences statsInfluenceOverTime (hungry --> minimum time, not hungry, maximum time)
        float love;
        float happyness;
        float sadness;
        float madness;

        float pacience;
        bool statsCoroutineStarted;
        
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<BoxCollider2D>();
            _animator = GetComponent<Animator>();

            gameObject.name = animalData.name;
            animalSO = (AnimalSO)Resources.Load($"Animals/{animalData.animal}");

            _animator.runtimeAnimatorController = animalSO.animatorControllers[animalData.variant];

            switch (animalData.animal)
            {
                case AnimalClass.Bat:
                case AnimalClass.Butterfly:
                case AnimalClass.Eagle:
                case AnimalClass.Seagull:
                    flying = true;
                    break;
                case AnimalClass.Cat:
                case AnimalClass.Chicken:
                case AnimalClass.Dog:
                case AnimalClass.Goat:
                case AnimalClass.Mouse:
                case AnimalClass.Pigeon:
                case AnimalClass.Rabbit:
                case AnimalClass.Ram:
                case AnimalClass.Swan:
                    flying = false;
                    break;
            }

            speed = animalSO.speed;
            maxSpeed = animalSO.maxSpeed;

            canChangeState = true;
        }

        void Update()
        {
            if(canChangeState)
                StartCoroutine(ChangeState());

            CheckScreenBounds();
            ChangeDir(dir);
            UpdateAnimator();
        }

        IEnumerator ChangeState()
        {
            canChangeState = false;

            if (Random.value < MOVE_CHANGE_RATE)
                dir = Vector2Int.zero;

            if(Random.value < DIR_CHANGE_RATE)
            {
                if (Random.value < .5f)
                    dir = Vector2Int.right * Random.Range(-1, 2);
                else
                    dir = Vector2Int.up * Random.Range(-1, 2);
            }

            if (animalSO.canFly)
                if (Random.value < FLY_CHANGE_RATE)
                    flying = !flying;

            yield return new WaitForSeconds(Random.Range(MIN_CHANGE_WAIT_TIME, Max_CHANGE_WAIT_TIME));
            canChangeState = true;
            yield break;
        }

        void CheckScreenBounds()
        {
            Vector2Int baseDir = GameManager.BounceFromScreen(transform.position, _collider.size / 2);

            if (baseDir != Vector2Int.zero)
                dir = baseDir;
        }

        void ChangeDir(Vector2Int dir)
        {
            _animator.speed = 1;

            if (dir == Vector2Int.zero)
                moving = false;
            else
                moving = true;

            if (dir == Vector2Int.right)
                direction = Directions.Right;
            else if (dir == Vector2Int.left)
                direction = Directions.Left;
            else if (dir == Vector2Int.up)
                direction = Directions.Up;
            else if (dir == Vector2Int.down)
                direction = Directions.Down;

            if (moving || flying)
                Move(dir);
            else
            {
                AnimatorClipInfo clipInfo = _animator.GetCurrentAnimatorClipInfo(0)[0]; // get current animation
                _animator.Play(clipInfo.clip.name, 0, .5f); // play current animation in the middle
                _animator.speed = 0;
            }
        }

        void Move(Vector2Int dir)
        {
            Vector2 velocity = _rb.velocity;
            velocity.x = Mathf.MoveTowards(velocity.x, dir.x * maxSpeed, speed * Time.deltaTime);
            velocity.y = Mathf.MoveTowards(velocity.y, dir.y * maxSpeed, speed * Time.deltaTime);
            _rb.velocity = velocity;
        }

        void UpdateAnimator()
        {
            _animator.SetInteger("Direction", (int)direction);

            if (animalSO.canFly)
                _animator.SetBool("Flying", flying);
        }

        void OnMouseDown()
        {
            pacience = STATS_INFLUENCE_OVER_TIME * Random.value;

            loveParticle.Play();
        }

        void OnMouseDrag()
        {
            Vector2 mousePosition = GameManager._cam.ScreenToWorldPoint(Input.mousePosition);
            transform.localPosition = mousePosition;
            _animator.speed = 2;

            if (Time.deltaTime > pacience && !statsCoroutineStarted)
            {
                statsCoroutineStarted = true;
                StartCoroutine(UpdateStats(Stats.Madness));
            }
        }

        void OnMouseUp()
        {
            _animator.speed = 1;

            if (statsCoroutineStarted)
            {
                statsCoroutineStarted = false;
                StopCoroutine(UpdateStats(Stats.Madness));
            }
        }

        IEnumerator UpdateStats(Stats stat)
        {
            yield return new WaitForSeconds(STATS_INFLUENCE_OVER_TIME * hunger);

            float value = 1;

            while (statsCoroutineStarted)
            {
                value = Random.value * STATS_INFLUENCE_MULTIPLIER;
                yield return null;
            }

            switch (stat)
            {
                case Stats.Madness:
                    madness = Mathf.Clamp01(value);
                    break;
            }

            Debug.Log(madness);
        }
    }
}
