using Enums;
using Game.MovementPatterns;
using Interfaces;
using UnityEngine;
using Utilities;

namespace Game.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        private IMovementPattern _movementPattern;
        private ProjectileData _projectileData;
        private SpriteRenderer _spriteRenderer;
        private float _speedMultiplier = 1f;
        private float _sizeMultiplier = 1f;
        private bool _wasCreatedByPlayer;
        private float _damage;

        private void Start()
        {
            Invoke(nameof(SelfDestruction),_projectileData.lifespan);
            _movementPattern.SetValues(transform, _speedMultiplier);
        }

        private void Update()
        {
            _movementPattern.UpdatePosition();
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

        public Projectile SetProjectileData(ProjectileData projectileData)
        {
            _projectileData = projectileData;
            RefreshVisuals();
            return this;
        }

        public Projectile SetMovementPattern(IMovementPattern movementPattern)
        {
            _movementPattern = movementPattern;
            return this;
        }

        public Projectile SetSpeedMultiplier(float speedMultiplier)
        {
            _speedMultiplier = speedMultiplier;
            return this;
        }
        
        public Projectile SetSizeMultiplier(float sizeMultiplier)
        {
            _sizeMultiplier = sizeMultiplier;
            transform.localScale *= _projectileData.size * _sizeMultiplier;
            return this;
        }

        public Projectile SetDamage(float damage)
        {
            _damage = damage;
            return this;
        }

        public Projectile SetBulletCreatedByPlayer(bool wasCreatedByPlayer)
        {
            _wasCreatedByPlayer = wasCreatedByPlayer;
            SetLayer(_wasCreatedByPlayer ? LayerType.ProjectileSpawnedByPlayer : LayerType.ProjectileSpawnedByEnemy);
            return this;
        }

        private void SetLayer(LayerType layerType)
        {
            gameObject.layer = ProjectConfig.GetLayerId(layerType);
        }

        public Projectile SetColor(Color color)
        {
            _spriteRenderer.color = color;
            return this;
        }

        public void SetColliderTrigger(bool triggerValue)
        {
            GetComponent<Collider2D>().isTrigger = triggerValue;
        }

        private void RefreshVisuals()
        {
            // Awake doesn't get called before this, idc
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = _projectileData.sprite;
        }

        private void SelfDestruction()
        {
            Destroy(gameObject);
        }
    }
}