using Enums;
using UnityEngine;
using Utilities;

namespace Game.Collectibles
{
    public class Coin : MonoBehaviour
    {
        private float _worth;
        private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite sprite;
        
        // TODO Quick way to do this, redo later
        private static Transform _coinsParent;

        public void Init(float worth)
        {
            if (_coinsParent == null)
            {
                _coinsParent = new GameObject("Coins").transform;
            }

            transform.parent = _coinsParent;
            _worth = worth;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            SetVisuals();
        }

        private void SetVisuals()
        {
            _spriteRenderer.sprite = sprite;


            if (_worth < 50)
            {
                _spriteRenderer.color = Color.green;
                return;
            }

            if (_worth < 99)
            {
                _spriteRenderer.color = Color.magenta;
                return;
            }

            _spriteRenderer.color = Color.yellow;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag(ProjectConfig.GetTagName(TagType.Player))) return;
            
            GlobalValues.AddPlayerGold(_worth);
            Destroy(gameObject);
        }
    }
}