using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;


public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    GameOver,
    Victory
}



public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    private GameState _currentState;
    private int _score;
    private int _health;


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



    public GameState CurrentState
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
                SetGameState(GameState.GameOver);
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

        InitGame();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (CurrentState == GameState.Playing)
            {
                PauseGame();
            }
            else if (CurrentState == GameState.Paused)
            {
                ResumeGame();
            }
        }
    }


    private void InitGame()
    {
        _score = 0;
        _health = maxHealth;
        CurrentState = GameState.MainMenu;
    }

    public void SetGameState(GameState newState)
    {
        CurrentState = newState;
    }

    private void OnStateChanged(GameState newState)
    {
        switch (newState)
        {
            case GameState.MainMenu:
                Time.timeScale = 1f;
                break;
            case GameState.Playing:
                Time.timeScale = 1f;
                break;
            case GameState.Paused:
                Time.timeScale = 0f;
                break;
            case GameState.GameOver:
                Time.timeScale = 0f;
                break;
            case GameState.Victory:
                Time.timeScale = 0f;
                break;
        }

    }


    public void StartGame()
    {
        _score = 0;
        _health = maxHealth;
        SetGameState(GameState.Playing);
    }

    public void PauseGame()
    {
        if (CurrentState == GameState.Playing)
        {
            SetGameState(GameState.Paused);
        }
    }

    public void ResumeGame()
    {
        if (CurrentState == GameState.Paused)
        {
            SetGameState(GameState.Playing);
        } 
    }


    public void GameOver()
    {
        SetGameState(GameState.GameOver);
    }

    public void Victory()
    {
        SetGameState(GameState.Victory);
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
