using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Reference to the Pause Menu UI
    private bool isPaused = false;

    void Start()
    {
        pauseMenuUI.SetActive(false); // Hide the pause menu at the start
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Resume game
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Freeze game
        isPaused = true;
    }

    public void Restart()
    {
        Time.timeScale = 1f; // Reset time scale in case it's paused
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current scene
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Load main menu scene (replace with actual name)
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit(); // Only works in a built game
    }
}
