using Game;
using UnityEngine;

namespace Menu
{
    public enum ElementState
    {
        Enabled,
        Disabled
    }
    
    public class MenuElement :  MonoBehaviour
    {
        public ElementState onPauseBehavior = ElementState.Enabled;
        public ElementState onPlayBehavior = ElementState.Disabled;
        private bool _initialized;
        
        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            if (_initialized) return;
            
            GameManager.Instance.OnGamePaused += OnPauseBehavior;
            GameManager.Instance.OnGameUnpaused += OnPlayBehavior;
            _initialized = true;
        }
        
        private void SetGameObjectActive(ElementState state)
        {
            switch (state)
            {
                case ElementState.Enabled:
                    gameObject.SetActive(true);
                    break;
                case ElementState.Disabled:
                    gameObject.SetActive(false);
                    break;
            }
        }

        private void OnPauseBehavior()
        {
            SetGameObjectActive(onPauseBehavior);
        }
        
        private void OnPlayBehavior()
        {
            SetGameObjectActive(onPlayBehavior);
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnGamePaused -= OnPauseBehavior;
            GameManager.Instance.OnGameUnpaused -= OnPlayBehavior;
        }
    }
}