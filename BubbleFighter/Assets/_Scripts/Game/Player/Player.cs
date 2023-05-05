using Enums;
using Game.Events;
using Game.Weapons;
using Game.Weapons.Guns;
using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Player : MonoBehaviour
    {
        [SerializeField] [Range(1,10)] private float moveSpeed;
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameEvent fireEvent;
        [SerializeField] private GameEvent statsChangeEvent;
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
                GlobalValues.DecreaseCooldown(99f);
                GlobalValues.AddDamage(1f);
                GlobalValues.AddDamageMultiplier(1f);
                GlobalValues.AddProjectileSize(10f);
                GlobalValues.AddProjectileSpeed(5000f);
                statsChangeEvent.Raise();
                GlobalValues.AddEnemyHealthMultiplier(10000f);
                FindObjectOfType<BubbleGun>().EnableAutoFire();
            }
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                GlobalValues.DecreaseCooldown(5f);
                GlobalValues.AddDamage(100f);
                GlobalValues.AddDamageMultiplier(100f);
                GlobalValues.AddProjectileSize(5f);
                GlobalValues.AddProjectileSpeed(500f);
                statsChangeEvent.Raise();
                FindObjectOfType<BubbleGun>().EnableAutoFire();
                FindObjectOfType<BubbleGun>().AddUpgrade(new WeaponUpgrade(WeaponUpgradeType.BaseDamage, 1f));
            }
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = new Vector2(_moveDirection.x * moveSpeed, _moveDirection.y * moveSpeed);
            
            Vector2 aimDirection = _mousePosition - _rigidbody.position;
            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            //_rigidbody.rotation = aimAngle;
        }
    }
}