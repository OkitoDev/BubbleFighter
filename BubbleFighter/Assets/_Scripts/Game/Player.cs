using Game.Events;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Player : MonoBehaviour
    {
        [SerializeField] [Range(1,10)] private float moveSpeed;
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameEvent fireEvent;
        [SerializeField] private GameEvent damageChangeEvent;
        public Transform FirePoint => firePoint;


        private Vector2 _moveDirection;
        private Vector2 _mousePosition;
        private Rigidbody2D _rigidbody;


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            _moveDirection = new Vector2(moveX, moveY).normalized;
            //_mousePosition = Mouse.GetMouseWorldPosition;
            //_mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetButtonDown("Fire1"))
            {
                fireEvent.Raise();
                _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                damageChangeEvent.Raise();
            }
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = new Vector2(_moveDirection.x * moveSpeed, _moveDirection.y * moveSpeed);
            
            Vector2 aimDirection = _mousePosition - _rigidbody.position;
            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            _rigidbody.rotation = aimAngle;
        }
    }
}