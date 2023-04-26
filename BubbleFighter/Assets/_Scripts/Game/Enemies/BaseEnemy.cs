using UnityEngine;

namespace Game.Enemies
{
    public abstract class BaseEnemy : MonoBehaviour, IEnemy
    {
        private float _healthPoints;
        
        public void TakeDamage(float damageAmount)
        {
            _healthPoints -= damageAmount;
        }

        public void Die()
        {
            throw new System.NotImplementedException();
        }
    }
}