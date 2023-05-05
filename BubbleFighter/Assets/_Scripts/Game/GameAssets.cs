using System;
using System.Collections.Generic;
using Game.Collectibles;
using Game.Projectiles;
using UnityEngine;
using UnityEngine.Audio;

namespace Game
{
    
    [Serializable]
    public class SoundAudioClip
    {
        public AudioClip audioClip;
        public AudioMixerGroup soundMixerGroup;
        [SerializeField] private float cooldown;
        private float _lastTimePlayed;

        public float LastTimePlayed
        {
            set => _lastTimePlayed = value;
        }

        public bool CanPlay => _lastTimePlayed + cooldown < Time.unscaledTime;
    }
    
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
        
        public List<SoundAudioClip> soundAudioClipList;
    }
}