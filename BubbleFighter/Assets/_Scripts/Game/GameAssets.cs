using UnityEngine;
using UnityEngine.Serialization;

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
        
        public GameObject prefabBasicBullet;
        public GameObject prefabBlueBackgroundMap;
        public GameObject prefabRedBackgroundMap;
    }
}