using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject youDiedPanel;
    void Start()
    {
        youDiedPanel.SetActive(false); // Hide the pause menu at the start
    }
    public void ShowGameOver()
    {
        youDiedPanel.SetActive(true); 
        Time.timeScale = 0f; 
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    
    public void MainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainMenu"); 
    }

}
