using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region Singleton

    public static LevelManager instance;
    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion
    

    public void PlayGame() {
        SceneManager.LoadScene("SampleScene");
    }
    
    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
