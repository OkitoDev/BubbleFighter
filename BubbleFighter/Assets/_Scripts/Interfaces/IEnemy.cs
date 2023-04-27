using UnityEngine;

namespace Interfaces
{
    public interface IEnemy : IDamageable, IKillable
    {
        public Transform Transform {get;}
    }
}