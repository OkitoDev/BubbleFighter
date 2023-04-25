using System;
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

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void ChangeColor(Color color)
        {
            _spriteRenderer.color = color;
        }

        public void SetBulletSpeed(Vector3 direction, float bulletSpeed)
        {
            GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
        }

        public void SetSize(float size)
        {
            transform.localScale *= size;
        }

        public void SetDamage(float damage)
        {
            _damage = damage;
            Debug.Log(damage);
        }
    }
}
