using System.Linq;
using Enums;
using Game.MovementPatterns;
using Helpers;
using Interfaces;
using UnityEngine;

namespace Game.Enemies
{
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class BaseEnemy : MonoBehaviour, IEnemy
    {
        public Transform Transform => transform;

        [SerializeField] private EnemyData enemyData;
        protected EnemyType enemyType;
        private float _totalWorth;
        private float _totalHealthPoints;
        protected float _totalDamage;
        private float _totalCollisionDamage;
        private float _totalAttackCooldown;
        private SpriteRenderer _spriteRenderer;
        private EnemyVariant _enemyVariant;
        private float _lastCollisionWithPlayer;
        private IMovementPattern _movementPattern;

        private bool IsCollisionWithPlayerOffCooldown => Time.time - _lastCollisionWithPlayer > enemyData.collisionCooldown;

        public IEnemy Init(EnemyType enemyType, Vector3 spawnPlace)
        {
            this.enemyType = enemyType;
            _enemyVariant = enemyData.variants.FirstOrDefault(variant => variant.enemyType == this.enemyType);
            _spriteRenderer = GetComponent<SpriteRenderer>();
            SetVisuals();
            UpdateStats();
            transform.position = spawnPlace;
            _movementPattern = GetMovementPattern();
            _movementPattern.SetValues(transform, enemyData.movementSpeed);
            return this;
        }

        private void FixedUpdate()
        {
            _movementPattern?.UpdatePosition();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.CompareTag(ProjectSettingsHelper.GetTagName(TagType.Player)) && IsCollisionWithPlayerOffCooldown)
            {
                GlobalValues.DamagePlayer(_totalCollisionDamage);
                _lastCollisionWithPlayer = Time.time;
            }
        }

        public void TakeDamage(float damageAmount)
        {
            _totalHealthPoints -= damageAmount;
            if (_totalHealthPoints < 0f) Die();
        }

        public void Die()
        {
            GlobalValues.AddPlayerGold(_totalWorth);
            Destroy(gameObject);
        }

        public void UpdateStats()
        {
            _totalWorth = enemyData.worth * GlobalValues.GlobalEnemyWorth * _enemyVariant.worthMultiplier;
            _totalHealthPoints = (enemyData.healthPoints * GlobalValues.GlobalEnemyHealth) *
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
            _spriteRenderer.sprite = enemyData.sprite;
            _spriteRenderer.color = _enemyVariant.color;
            transform.localScale *= _enemyVariant.sizeMultiplier;
        }

        private void RestartAttackPattern()
        {
            CancelInvoke(nameof(Attack));
            InvokeRepeating(nameof(Attack),_totalAttackCooldown,_totalAttackCooldown);
        }
        
        protected abstract IMovementPattern GetMovementPattern();
        
        protected abstract void Attack();
    }
}