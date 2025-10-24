using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class MainMenuManager : MonoBehaviour
{



    public TextMeshProUGUI titleText;
    public TextMeshProUGUI startText;
    public TextMeshProUGUI quitText;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }

    }


    void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    
    void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
