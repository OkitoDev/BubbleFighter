using Game.Weapons.Projectiles;
using UnityEngine;

namespace Game
{
    public class GameAssets : MonoBehaviour
    {
        private static GameAssets _i;

        public static GameAssets Instance
        {
            get
            {
                if (_i == null)
                {
                    _i = Instantiate(Resources.Load("GameAssets") as GameObject).GetComponent<GameAssets>();
                }
                return _i;
            }
        }
        
        public Projectile prefabDefaultProjectile;
        public Coin prefabCoin;
    }
}