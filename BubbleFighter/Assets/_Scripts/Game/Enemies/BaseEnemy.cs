using System;
using System.Linq;
using Enums;
using Helpers;
using Interfaces;
using UnityEngine;

namespace Game.Enemies
{
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class BaseEnemy : MonoBehaviour, IEnemy
    {
        private EnemyType _enemyType;
        private float _totalWorth;
        private float _totalHealthPoints;
        private float _totalMovementSpeed;
        private float _totalDamage;
        private float _totalCollisionDamage;
        private float _totalAttackCooldown;
        private SpriteRenderer _spriteRenderer;
        private EnemyData _enemyData;
        private EnemyVariants _enemyVariant;
        private float lastCollisionWithPlayer;

        private bool IsCollisionWithPlayerOffCooldown => Time.time - lastCollisionWithPlayer > _enemyData.collisionCooldown;

        public IEnemy Init(EnemyType enemyType, Vector3 spawnPlace)
        {
            _enemyType = enemyType;
            _enemyVariant = _enemyData.variants.FirstOrDefault(variant => variant.enemyType == _enemyType);
            _spriteRenderer = GetComponent<SpriteRenderer>();
            SetVisuals();
            UpdateStats();
            transform.position = spawnPlace;
            return this;
        }

        private void Update()
        {
            Pathing();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.CompareTag(ProjectSettingsHelper.GetTagName(TagType.Player)) && IsCollisionWithPlayerOffCooldown)
            {
                GlobalValues.DamagePlayer(_totalCollisionDamage);
                lastCollisionWithPlayer = Time.time;
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
            _totalMovementSpeed = _enemyData.movementSpeed * GlobalValues.GlobalEnemyMovementSpeed *
                                  _enemyVariant.movementSpeedMultiplier;
            _totalWorth = _enemyData.worth * GlobalValues.GlobalEnemyWorth * _enemyVariant.worthMultiplier;
            _totalHealthPoints = (_enemyData.healthPoints * GlobalValues.GlobalEnemyHealth) *
                                 (GlobalValues.GlobalEnemyHealthMultiplier) * _enemyVariant.healthPointsMultiplier;
            _totalDamage = (_enemyData.damage + GlobalValues.GlobalEnemyDamage) *
                           GlobalValues.GlobalEnemyDamageMultiplier * _enemyVariant.damageMultiplier;
            _totalAttackCooldown = _enemyData.attackCooldown * GlobalValues.GlobalEnemyCooldownReduction;
            _totalAttackCooldown *= (100f - _enemyVariant.cooldownDecrease) / 100f;
            _totalAttackCooldown = Mathf.Max(_totalAttackCooldown,0.001f);
            _totalCollisionDamage = _enemyData.damage * _enemyData.collisionDamageMultiplier;
            CancelInvoke(nameof(Attack));
            InvokeRepeating(nameof(Attack),_totalAttackCooldown,_totalAttackCooldown);
        }

        private void SetVisuals()
        {
            _spriteRenderer.sprite = _enemyData.sprite;
            _spriteRenderer.color = _enemyVariant.color;
            transform.localScale *= _enemyVariant.sizeMultiplier;
        }

        protected abstract void Pathing();
        protected abstract void Attack();
    }
}