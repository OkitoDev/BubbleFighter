using System.Linq;
using Enums;
using Game.MovementPatterns;
using Interfaces;
using UnityEngine;
using Utilities;

namespace Game.Enemies
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Collider2D))]
    public abstract class BaseEnemy : MonoBehaviour, IEnemy
    {
        public Transform Transform => transform;

        [SerializeField] private EnemyData enemyData;
        private EnemyType _enemyType;
        private float _totalWorth;
        private float _totalHealthPoints;
        private float _totalDamage;
        private float _totalCollisionDamage;
        private float _totalAttackCooldown;
        private SpriteRenderer _spriteRenderer;
        private EnemyVariant _enemyVariant;
        private float _lastCollisionWithPlayer;
        private IMovementPattern _movementPattern;
        private Rigidbody2D _rigidbody;

        private bool IsCollisionWithPlayerOffCooldown => Time.time - _lastCollisionWithPlayer > enemyData.collisionCooldown;

        public IEnemy Init(EnemyType enemyType, Vector3 spawnPlace)
        {
            this._enemyType = enemyType;
            _enemyVariant = enemyData.variants.FirstOrDefault(variant => variant.enemyType == this._enemyType);
            _spriteRenderer = GetComponent<SpriteRenderer>();
            SetVisuals();
            UpdateStats();
            transform.position = spawnPlace;
            _movementPattern = GetMovementPattern();
            _movementPattern.SetValues(transform, enemyData.movementSpeed);
            _rigidbody = GetComponent<Rigidbody2D>();
            return this;
        }

        private void FixedUpdate()
        {
            _movementPattern?.UpdatePosition();
            _rigidbody.velocity = Vector2.zero;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.CompareTag(ProjectConfig.GetTagName(TagType.Player)) && IsCollisionWithPlayerOffCooldown)
            {
                GlobalValues.DamagePlayer(_totalCollisionDamage);
                _lastCollisionWithPlayer = Time.time;
                Die();
            }
        }
        
        
        public void TakeDamage(float damageAmount)
        {
            _totalHealthPoints -= damageAmount;
            if (!(_totalHealthPoints < 0f)) return;
            
            SpawnACoin();
            Die();
        }

        public void Die()
        {
            Destroy(gameObject);
        }

        private void SpawnACoin()
        {
            Instantiate(GameAssets.Instance.prefabCoin, transform.position, Quaternion.identity).Init(Mathf.Ceil(_totalWorth));
        }

        public void UpdateStats()
        {
            _totalWorth = enemyData.worth * GlobalValues.GlobalEnemyWorth * _enemyVariant.worthMultiplier;
            _totalHealthPoints = (enemyData.healthPoints + GlobalValues.GlobalEnemyHealth) *
                                 (GlobalValues.GlobalEnemyHealthMultiplier) * _enemyVariant.healthPointsMultiplier;
            _totalDamage = (enemyData.damage + GlobalValues.GlobalEnemyDamage) *
                           GlobalValues.GlobalEnemyDamageMultiplier * _enemyVariant.damageMultiplier;
            _totalAttackCooldown = enemyData.attackCooldown * GlobalValues.GlobalEnemyCooldownReduction;
            _totalAttackCooldown *= (100f - _enemyVariant.cooldownDecrease) / 100f;
            _totalAttackCooldown = Mathf.Max(_totalAttackCooldown,0.001f);
            _totalCollisionDamage = enemyData.damage * enemyData.collisionDamageMultiplier;
            RestartAttackPattern();
        }

        private void SetVisuals()
        {
            _spriteRenderer.color = _enemyVariant.color;
            transform.localScale *= _enemyVariant.sizeMultiplier;
        }

        private void RestartAttackPattern()
        {
            CancelInvoke(nameof(Attack));
            InvokeRepeating(nameof(Attack),_totalAttackCooldown,_totalAttackCooldown);
        }

        protected virtual IMovementPattern GetProjectileMovementPattern()
        {
            return new MovementPatternMoveTowardsPlayer();
        }
        
        protected abstract IMovementPattern GetMovementPattern();
        
        protected abstract void Attack();
    }
}