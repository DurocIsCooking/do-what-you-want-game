using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

// Handles the main menu, pause menu and death menu. Sets up the game when the player decides to play.
public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _deathMenu;
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _respawnPoint;

    // Whether the game is paused
    public bool isPaused = false;

    // A list of gameobjects to destroy when the player restarts the game (missiles, spawnindicators, and explosions)
    public List<GameObject> DestroyOnRestart;

    // Singleton pattern
    private static MenuManager _instance;

    public static MenuManager Instance
    {
        get
        {
            if (_instance == null)
            {
                MenuManager singleton = GameObject.FindObjectOfType<MenuManager>();
                if (singleton == null)
                {
                    GameObject go = new GameObject();
                    _instance = go.AddComponent<MenuManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        OpenPauseMenu(false);
    }

    private void Start()
    {
        if(_deathMenu != null)
        {
            StartGame();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    // Open / close pause menu
    public void PauseGame()
    {
        OpenPauseMenu(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void ResumeGame()
    {
        OpenPauseMenu(false);
        Time.timeScale = 1;
        isPaused = false;
    }
    public void OpenPauseMenu(bool isActive)
    {
        // Since not all scenes contain a pause menu
        if (_pauseMenu != null)
        {
            _pauseMenu.SetActive(isActive);
        }

    }

    // Death menu
    public void OpenDeathMenu()
    {
        if (_deathMenu != null)
        {
            _deathMenu.SetActive(true);
        }
    }

    public void StartGame()
    {
        // We're about to destroy stuff with tweens, so clear them
        DOTween.Clear();
        // Prevent leftover missile spawns
        EnemyManager.Instance.CancelInvoke();
        // Destroy leftover objects from last attempt
        foreach(GameObject leftover in DestroyOnRestart)
        {
            Destroy(leftover);
        }
        // Respawn player, reset score, reset missile properties
        _deathMenu.SetActive(false);
        EnemyManager.Instance.Player = Instantiate(_player, _respawnPoint.position, Quaternion.identity);
        ScoreManager.Instance.ResetScore();
        EnemyManager.Instance.ResetValues();
    }

    // Load scenes and quit game
    public void LoadGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        // Hide text
        ScoreManager.Instance.ShowUI(false);
        OpenDeathMenu();
    }


}
