using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AngryAliens
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        public static GameManager Instance = null;
        private void Awake()
        {
            Instance = this;
        }
        private void OnDestroy()
        {
            Instance = null;
        }
        #endregion 
        
        public void RestartGame()
        {
            print("Game must be over?");
            // Get current scene
            Scene currentScene = SceneManager.GetActiveScene();
            // Reload current scene
            SceneManager.LoadScene(currentScene.buildIndex);
        }
    }
}