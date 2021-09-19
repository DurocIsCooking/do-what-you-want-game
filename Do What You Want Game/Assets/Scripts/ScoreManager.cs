using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    // Singleton pattern
    private static ScoreManager _instance;

    public static ScoreManager Instance
    {
        get
        {
            if (_instance == null)
            {
                ScoreManager singleton = GameObject.FindObjectOfType<ScoreManager>();
                if (singleton == null)
                {
                    GameObject go = new GameObject();
                    _instance = go.AddComponent<ScoreManager>();
                }
            }
            return _instance;
        }
    }


    // Text elements
    private static Text _scoreText;
    private static int _score;

    public static int ScoreText
    {
        get
        {
            return _score;
        }

        set
        {
            // Increase score
            _score = value;
            // Display score
            _scoreText.text = "Score: " + _score;
            
            //At certain intervals, increase difficulty
            if(_score%10 == 5)
            {
                // Make enemies spawn more quickly
                EnemyManager.SpawnInterval = EnemyManager.SpawnInterval - 1.5f;
            }

            if(_score%10 == 0)
            {
                // Make enemies more maneuverable
                EnemyManager.EnemyRotationSpeed = EnemyManager.EnemyRotationSpeed + 1f;
            }
        }
    }



    public static Text _gameOverText;
   

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
        _gameOverText.enabled = false;
    }


    public static void GameOver()
    {
        // Remove score text
        _scoreText.text = "";
        // use DOTween to fade in gameover text
        _gameOverText.enabled = true;
        _gameOverText.text += "\nFinal score: " + _score;
    }

}
