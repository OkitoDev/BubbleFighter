using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private float _damage;
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

        public void ChangeColor(Color color)
        {
            _spriteRenderer.color = color;
        }

        public void SetBulletSpeed(Vector2 direction, float bulletSpeed)
        {
            GetComponent<Rigidbody2D>().AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);
        }

        public void SetSize(float size)
        {
            transform.localScale *= size;
        }

        public void SetDamage(float damage)
        {
            _damage = damage;
        }
    }
}
