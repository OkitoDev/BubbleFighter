using System;
using UnityEngine;

namespace Game
{
    public enum GameState
    {
        Playing,
        Paused
    }
    
    public class GameManager
    {
        private static GameManager _instance;
        public static GameManager Instance => _instance ??= new GameManager();
        private GameState _currentGameState = GameState.Playing;
        public bool IsGamePaused => _currentGameState == GameState.Paused;
        public event Action OnGamePaused;
        public event Action OnGameUnpaused;
        
        private GameManager(){}

        public void PauseGame()
        {
            if (_currentGameState == GameState.Paused) return;
            
            Time.timeScale = 0;
            _currentGameState = GameState.Paused;
            OnGamePaused?.Invoke();
        }
        
        public void UnpauseGame()
        {
            if (_currentGameState == GameState.Playing) return;
            
            Time.timeScale = 1;
            _currentGameState = GameState.Playing;
            OnGameUnpaused?.Invoke();
        }
    }
}