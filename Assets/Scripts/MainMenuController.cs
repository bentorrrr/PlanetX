using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public string PlayerControl;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void NewGame()
    {
        SceneManager.LoadScene("PlayerControl");
    }
    public void Continue()
    {
        Debug.Log("Continue Game");
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Debug.Log("Exit Game");
        Application.Quit();
#endif
    }


    // Update is called once per frame
    void Update()
    {

    }
}
