using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

// Displays score, upgrade, and game over text
public class UIManager : MonoBehaviour
{

    // Singleton pattern
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                UIManager singleton = GameObject.FindObjectOfType<UIManager>();
                if (singleton == null)
                {
                    GameObject go = new GameObject();
                    _instance = go.AddComponent<UIManager>();
                }
            }
            return _instance;
        }
    }

    // Text elements
    private static Text _scoreText;

    private static Text _gameOverText;

    private static Text _upgradeText;


    public static string ScoreText
    {
        get
        {
            return _scoreText.text;
        }
        set
        {
            // UI DOTWEEN
            _scoreText.text = value;
        }
    }


    public static string UpgradeText
    {
        get
        {
            return _upgradeText.text;
        }
        set
        {
            // Display text
            _upgradeText.color = new Color(1, 1, 1, 1);
            _upgradeText.text = value;

            // Fade text out
            Instance.Invoke("FadeUpgradeText", 1);
        }
    }

    private void FadeUpgradeText()
    {
        _upgradeText.DOColor(new Color(1, 1, 1, 0), 3);
    }

    private void Awake()
    {
        // Singleton
        _instance = this;
        DontDestroyOnLoad(gameObject);

        // Find and set text elements
        _scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        _scoreText.text = "Score: " + 0;

        _gameOverText = GameObject.Find("GameOverText").GetComponent<Text>();
        _gameOverText.text = "Game over!";
        _gameOverText.color = new Color(1, 1, 1, 0);

        _upgradeText = GameObject.Find("UpgradeText").GetComponent<Text>();
        _upgradeText.text = "";
    }

    public static void GameOver()
    {
        // Remove score text
        _scoreText.text = "";
        // use DOTween to fade in gameover text
        _gameOverText.DOColor(new Color(1, 1, 1, 1), 3);
        _gameOverText.text += "\nFinal score: " + ScoreManager.Score;
    }

}
