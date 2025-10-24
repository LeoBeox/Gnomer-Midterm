using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    private Utilities.GameState _currentState;
    private int _score;
    private int _health;

    private Scene scene;


    private TMP_Text pauseText;


    // Player Stats
    public int maxHealth = 3;

    // Score Settings
    public int pointsPerTile = 10;
    public int pointsPerTreausre = 100;



    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("GameManager is null");
            }
            return _instance;
        }
    }



    public Utilities.GameState CurrentState
    {
        get { return _currentState; }
        private set
        {
            if (_currentState != value)
            {
                _currentState = value;
                OnStateChanged(value);
            }
        }
    }


    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
        }
    }

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = Mathf.Clamp(value, 0, maxHealth);

            if (_health <= 0)
            {
                SetGameState(Utilities.GameState.GameOver);
            }
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;

        InitMenu();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        if (scene.name == "MainMenu")
        {
            InitMenu();
        }
        else if (scene.name == "GameScene")
        {
            StartGame();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        if (CurrentState == Utilities.GameState.MainMenu && SceneManager.GetActiveScene() != scene)
        {
            StartGame();
        }
        
        
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (CurrentState == Utilities.GameState.Playing)
            {
                PauseGame();
            }
            else if (CurrentState == Utilities.GameState.Paused)
            {
                ResumeGame();
            }
        }

        // Debug.Log("Currently in " + CurrentState);
    }


    public void SetGameState(Utilities.GameState newState)
    {
        CurrentState = newState;
    }

    private void OnStateChanged(Utilities.GameState newState)
    {
        switch (newState)
        {
            case Utilities.GameState.MainMenu:
                Time.timeScale = 1f;
                break;
            case Utilities.GameState.Playing:
                Time.timeScale = 1f;
                break;
            case Utilities.GameState.Paused:
                Time.timeScale = 0f;
                break;
            case Utilities.GameState.GameOver:
                Time.timeScale = 0f;
                break;
            case Utilities.GameState.Victory:
                Time.timeScale = 0f;
                break;
        }

    }


    public void InitMenu()
    {
        SetGameState(Utilities.GameState.MainMenu);
    }
    
    
    
    public void StartGame()
    {
        _score = 0;
        _health = maxHealth;

        var tmp = GameObject.FindWithTag("TMPText");
        pauseText = tmp.GetComponent<TextMeshProUGUI>();


        SetGameState(Utilities.GameState.Playing);
    }

    public void PauseGame()
    {
        if (CurrentState == Utilities.GameState.Playing)
        {
            SetGameState(Utilities.GameState.Paused);
            pauseText.enabled = true;
            Debug.Log("pause text is" + pauseText.enabled);
        }
    }

    public void ResumeGame()
    {
        if (CurrentState == Utilities.GameState.Paused)
        {
            SetGameState(Utilities.GameState.Playing);
            pauseText.enabled = false;
        } 
    }


    public void GameOver()
    {
        SetGameState(Utilities.GameState.GameOver);
    }

    public void Victory()
    {
        SetGameState(Utilities.GameState.Victory);
    }

    public void QuitGame()
    {

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }


    public void AddScore(int points)
    {
        Score += points;

    }

    public void AddTileScore()
    {
        AddScore(pointsPerTile);

    }

    public void AddTreasureScore()
    {
        AddScore(pointsPerTreausre);

    }
    
}
