using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    // TODO that's really bad but it works and I won't need to modify it ever so for now that's fine
    public class BackgroundController : MonoBehaviour
    {
        [SerializeField] private Sprite backgroundSprite;
        private GameObject backgroundPrefab;
        private Transform _playerTransform;
        private float _backgroundWidth;
        private float _backgroundHeight;
        private float _cameraWidth;
        private float _cameraHeight;
        private readonly List<Transform> _backgrounds = new List<Transform>();
        private List<Transform> MostLeftBackgrounds => _backgrounds.Where(background => Math.Abs(background.transform.position.x - LowestX) < 0.01f).ToList();
        private List<Transform> MostRightBackgrounds => _backgrounds.Where(background => Math.Abs(background.position.x - HighestX) < 0.01f).ToList();
        private List<Transform> MostUpperBackgrounds => _backgrounds.Where(background => Math.Abs(background.transform.position.y - LowestY) < 0.01f).ToList();
        private List<Transform> MostLowerBackgrounds => _backgrounds.Where(background => Math.Abs(background.position.y - HighestY) < 0.01f).ToList();
        private float LowestX => _backgrounds.Select(background => background.transform.position.x).Min();
        private float HighestX => _backgrounds.Select(background => background.transform.position.x).Max();
        private float LowestY => _backgrounds.Select(background => background.transform.position.y).Min();
        private float HighestY => _backgrounds.Select(background => background.transform.position.y).Max();

        private void Awake()
        {
            backgroundPrefab = new GameObject();
            backgroundPrefab.AddComponent<SpriteRenderer>().sprite = backgroundSprite;
            var backgroundScale = backgroundPrefab.transform.localScale;
            _backgroundWidth = backgroundPrefab.GetComponent<SpriteRenderer>().sprite.bounds.size.x * backgroundScale.x;
            _backgroundHeight = backgroundPrefab.GetComponent<SpriteRenderer>().sprite.bounds.size.y * backgroundScale.y;
            _playerTransform = ObjectFinder.Player.transform;
            _backgrounds.Add(Instantiate(backgroundPrefab, transform).transform);
            var mainCamera = Camera.main;
            _cameraWidth = mainCamera.orthographicSize * mainCamera.aspect;
            _cameraHeight = mainCamera.orthographicSize;
        }
        
        private void LateUpdate()
        {
            while (LowestX > _playerTransform.position.x - _cameraWidth)
            {
                foreach (var background in MostLeftBackgrounds)
                {
                    var position = background.position;
                    var newPosition = new Vector3(position.x - _backgroundWidth, position.y,10);
                    _backgrounds.Add(Instantiate(backgroundPrefab, newPosition, Quaternion.identity, transform).transform);
                }
            }

            while (HighestX < _playerTransform.position.x + _cameraWidth)
            {
                foreach (var background in MostRightBackgrounds)
                {
                    var position = background.position;
                    var newPosition = new Vector3(position.x + _backgroundWidth, position.y,10);
                    _backgrounds.Add(Instantiate(backgroundPrefab, newPosition, Quaternion.identity, transform).transform);
                }
            }
            
            while (LowestY > _playerTransform.position.y - _cameraHeight)
            {
                foreach (var background in MostUpperBackgrounds)
                {
                    var position = background.position;
                    var newPosition = new Vector3(position.x, position.y - _backgroundHeight,10);
                    _backgrounds.Add(Instantiate(backgroundPrefab, newPosition, Quaternion.identity, transform).transform);
                }
            }

            while (HighestY < _playerTransform.position.y + _cameraHeight)
            {
                foreach (var background in MostLowerBackgrounds)
                {
                    var position = background.position;
                    var newPosition = new Vector3(position.x, position.y + _backgroundHeight,10);
                    _backgrounds.Add(Instantiate(backgroundPrefab, newPosition, Quaternion.identity, transform).transform);
                }
            }

            var temp = new List<Transform>();

            foreach (var background in _backgrounds)
            {
                if (background.position.x > _playerTransform.position.x + _cameraWidth + _backgroundWidth
                || background.position.x < _playerTransform.position.x - _cameraWidth - _backgroundWidth
                || background.position.y > _playerTransform.position.y + _cameraHeight + _backgroundHeight
                || background.position.y < _playerTransform.position.y - _cameraHeight - _backgroundHeight)
                {
                    temp.Add(background);
                }
            }

            foreach (var bg in temp)
            {
                _backgrounds.Remove(bg);
                Destroy(bg.gameObject);
            }
        }
    }
}