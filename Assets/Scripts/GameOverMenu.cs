using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject youDiedPanel;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        youDiedPanel.SetActive(false); // Hide the pause menu at the start
    }
    public void ShowGameOver()
    {
        audioManager.PlaySFX(audioManager.GameOver);
        youDiedPanel.SetActive(true); 
        Time.timeScale = 0f; 
    }

    public void RestartLevel()
    {
        FindObjectOfType<AudioManager>().StopMusic();
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }


    public void MainMenu()
    {
        FindObjectOfType<AudioManager>().StopMusic();
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainMenu"); 
    }

}
