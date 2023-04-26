using Enums;
using Helpers;
using Interfaces;
using UnityEngine;

namespace Game.Weapons
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private float _damage;
        private bool _wasCreatedByPlayer;
        private Vector3 _initialPosition;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _initialPosition = transform.position;
        }

        private void Update()
        {
            if (Vector3.Distance(_initialPosition, transform.position) > 8f)
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if(_wasCreatedByPlayer && other.gameObject.TryGetComponent(out IEnemy enemy))
            {
                enemy.TakeDamage(_damage);
            }
            
            if (!_wasCreatedByPlayer && other.gameObject.CompareTag(ProjectSettingsHelper.GetTagName(TagType.Player)))
            {
                GlobalValues.DamagePlayer(_damage);
            }
        }

        public Projectile ChangeColor(Color color)
        {
            _spriteRenderer.color = color;
            return this;
        }

        public Projectile SetProjectileSpeed(Vector2 direction, float projectileSpeed)
        {
            GetComponent<Rigidbody2D>().AddForce(direction.normalized * projectileSpeed, ForceMode2D.Impulse);
            return this;
        }

        public Projectile SetSize(float size)
        {
            transform.localScale *= size;
            return this;
        }

        public Projectile SetDamage(float damage)
        {
            _damage = damage;
            return this;
        }

        public Projectile SetCreator(bool isPlayer)
        {
            _wasCreatedByPlayer = isPlayer;
            return this;
        }
    }
}
