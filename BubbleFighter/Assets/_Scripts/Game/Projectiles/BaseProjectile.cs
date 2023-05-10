using System;
using Enums;
using Game.MovementPatterns;
using Interfaces;
using UnityEngine;
using Utilities;

namespace Game.Projectiles
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Collider2D))]
    public abstract class BaseProjectile : MonoBehaviour
    {
        private float _lifespan;
        private IMovementPattern _movementPattern;
        private SpriteRenderer _spriteRenderer;
        private float _speedMultiplier = 1f;
        private float _sizeMultiplier = 1f;
        private bool _wasCreatedByPlayer;
        private float _damage;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            _movementPattern?.UpdatePosition();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            switch (_wasCreatedByPlayer)
            {
                case true when other.gameObject.TryGetComponent(out IEnemy enemy):
                    enemy.TakeDamage(_damage);
                    Destroy(gameObject);
                    break;
                case false when other.gameObject.CompareTag(ProjectConfig.GetTagName(TagType.Player)):
                    GlobalValues.DamagePlayer(_damage);
                    Destroy(gameObject);
                    break;
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            switch (_wasCreatedByPlayer)
            {
                case true when other.gameObject.TryGetComponent(out IEnemy enemy):
                    enemy.TakeDamage(_damage);
                    Destroy(gameObject);
                    break;
                case false when other.gameObject.CompareTag(ProjectConfig.GetTagName(TagType.Player)):
                    GlobalValues.DamagePlayer(_damage);
                    Destroy(gameObject);
                    break;
            }
        }

        public void SetMovementPattern(IMovementPattern movementPattern)
        {
            _movementPattern = movementPattern;
            _movementPattern.SetValues(transform, _speedMultiplier);
        }

        public void SetSpeedMultiplier(float speedMultiplier)
        {
            _speedMultiplier = speedMultiplier;
            _movementPattern.SetValues(transform, _speedMultiplier);
        }
        
        public void SetSizeMultiplier(float sizeMultiplier)
        {
            _sizeMultiplier = sizeMultiplier;
            transform.localScale *= _sizeMultiplier;
        }

        public void SetDamage(float damage)
        {
            _damage = damage;
        }

        public void SetBulletCreatedByPlayer(bool wasCreatedByPlayer)
        {
            _wasCreatedByPlayer = wasCreatedByPlayer;
            SetLayer(_wasCreatedByPlayer ? LayerType.ProjectileSpawnedByPlayer : LayerType.ProjectileSpawnedByEnemy);
        }

        private void SetLayer(LayerType layerType)
        {
            gameObject.layer = ProjectConfig.GetLayerId(layerType);
        }

        public void SetColor(Color color)
        {
            _spriteRenderer.color = color;
        }

        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }

        public void SetColliderTrigger(bool triggerValue)
        {
            GetComponent<Collider2D>().isTrigger = triggerValue;
        }

        public void SetLifespan(float lifespan)
        {
            _lifespan = lifespan;
            Invoke(nameof(SelfDestruction), _lifespan);
        }

        private void SelfDestruction()
        {
            Destroy(gameObject);
        }
    }
}