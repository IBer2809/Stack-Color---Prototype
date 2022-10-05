using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Menu,
    Playing,
    GameOver,
    Finish
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static event System.Action<GameState, GameState> GameStateChanged = delegate { };
    public GameState GameState
    {
        get
        {
            return _gameState;
        }
        private set
        {
            if (value != _gameState)
            {
                GameState oldState = _gameState;
                _gameState = value;

                GameStateChanged(_gameState, oldState);
            }
        }
    }

    [SerializeField]
    private GameState _gameState;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
        Menu();
    }

    public void StartGame()
    {
        GameState = GameState.Playing;
        Time.timeScale = 1f;
    }

    public void Menu()
    {
        GameState = GameState.Menu;
        Time.timeScale = 1f;
        Debug.Log(GameState);
    }

    public void GameOver()
    {
        GameState = GameState.GameOver;
        Time.timeScale = 0f;
        CoinsManager.Instance.AddPermanentCoinsToAllCash();
    }

    public void Finish()
    {
        GameState = GameState.Finish;
        Time.timeScale = 0f;
        CoinsManager.Instance.AddPermanentCoinsToAllCash();
        ScoreManager.Instance.UpdateLvl();
    }
   


    public void Continue()
    {
        GameState = GameState.Playing;
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /*public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }*/
}
