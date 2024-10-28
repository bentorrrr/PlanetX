using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMechanics : MonoBehaviour
{
    public GameObject PauseCanvas; // The Canvas that will be appearing whenever use click on Pause button or Esc
    public string MainMenuSceneName; // The name of your mainmenu scene for unity to load back to it
    private bool isPause = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Whenever user click on Esc button on their keyboard Pause function will be called.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        // Pause function it will determine whather the game isPause or not if not it will pause the game and vice versa
        if (!isPause)
        {
            // Set time scale to 0 (Stop the game time)
            Time.timeScale = 0.0f;
            // SetActive PauseCanvas (Window that contains all the button for pause menu)
            PauseCanvas.SetActive(true);
            // Set isPause boolean to True
            isPause = true;
        }
        else
        {
            // Call Resume
            Resume();
        }
    }

    public void Resume()
    {
        // All Resume do is do all the same step of Pause but in opposite direction
        Time.timeScale = 1.0f;
        PauseCanvas.SetActive(false);
        isPause = false;
    }

    public void Restart()
    {
        // Set isPause back to false (Prevent errors that might happen)
        isPause = false;
        // Set timescale back to 1 (Default speed)
        Time.timeScale = 1.0f;
        // Load Current Scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        // Set isPause back to false (Prevent errors that might happen)
        isPause = false;
        // Set timescale back to 1 (Default speed)
        Time.timeScale = 1.0f;
        // Load MainMenu Scene
        SceneManager.LoadScene(MainMenuSceneName);
    }
}
