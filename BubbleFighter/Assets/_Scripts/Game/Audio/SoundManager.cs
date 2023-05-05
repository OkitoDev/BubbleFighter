using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace Game.Audio
{
    public static class SoundManager
    {
        // TODO Quick way to do this, redo later
        private static Transform _oneShootGameObjectParent;
        private static GameObject _oneShotGameObject;
        private static AudioSource _oneShotAudioSource;
        

        static SoundManager()
        {
            SetupOneShotEntities();
        }

        public static void PlaySound(string soundName)
        {
            var soundData = GetAudioClipData(soundName);
            if (!soundData.CanPlay) return;
            
            _oneShotGameObject.transform.position = new Vector2(Screen.width/2f, Screen.height/2f);
            _oneShotAudioSource.outputAudioMixerGroup = soundData.soundMixerGroup;
            
            _oneShotAudioSource.SetupAudioSource(soundData.soundMixerGroup);
            _oneShotAudioSource.PlayOneShot(soundData.audioClip);
            soundData.LastTimePlayed = Time.unscaledTime;
        }
        
        public static void PlaySound(string soundName, Vector3 position)
        {
            var soundData = GetAudioClipData(soundName);
            if (!soundData.CanPlay) return;

            SetupSoundGameObject(soundData, position);
            soundData.LastTimePlayed = Time.unscaledTime;
        }

        private static void SetupSoundGameObject(SoundAudioClip soundData, Vector3 position)
        {
            if (_oneShootGameObjectParent == null)
            {
                _oneShootGameObjectParent = new GameObject("Sounds").transform;
            }
            
            var soundGameObject = new GameObject("Sound");
            soundGameObject.AddComponent<AudioSource>().SetupAudioSource(soundData.soundMixerGroup, soundData.audioClip).Play();
            soundGameObject.transform.position = position;
            soundGameObject.transform.parent = _oneShootGameObjectParent;
            
            Object.Destroy(soundGameObject, soundData.audioClip.length);
        }

        private static SoundAudioClip GetAudioClipData(string soundName)
        {
            foreach (var soundAudioClip in GameAssets.Instance.soundAudioClipList.
                Where(soundAudioClip => soundAudioClip.audioClip.name == soundName))
            {
                return soundAudioClip;
            }

            Debug.LogError("Audio clip " + soundName + " not found!");
            return new SoundAudioClip();
        }
        
        private static void SetupOneShotEntities()
        {
            _oneShootGameObjectParent = new GameObject("Sounds").transform;
            _oneShotGameObject = new GameObject("One Shot Sound");
            // ??
            // _oneShotGameObject.transform.localPosition -= Vector3.left * 10f;
            _oneShotAudioSource = _oneShotGameObject.AddComponent<AudioSource>();
            _oneShotAudioSource.playOnAwake = false;
            _oneShotAudioSource.panStereo = -1f;
            Object.DontDestroyOnLoad(_oneShotGameObject);
        }

        private static AudioSource SetupAudioSource(this AudioSource audioSource, AudioMixerGroup audioMixerGroup, AudioClip audioClip = null)
        {
            if (audioClip != null)
            {
                audioSource.clip = audioClip;
            }
            audioSource.outputAudioMixerGroup = audioMixerGroup;
            audioSource.dopplerLevel = 0f;
            audioSource.spatialBlend = 0.5f;
            audioSource.rolloffMode = AudioRolloffMode.Linear;

            return audioSource;
        }
    }
}
