using Game;
using UnityEngine;

namespace Menu
{
    public class InGameMenu : MonoBehaviour
    {
        public void PauseGame()
        {
            GameManager.Instance.PauseGame();
        }
        
        public void UnpauseGame()
        {
            GameManager.Instance.UnpauseGame();
        }
    }
}