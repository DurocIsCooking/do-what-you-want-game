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
    private static int _score;

    public static Text _gameOverText;

    public static int Score
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

            UIManager.ScoreText = "Score: " + _score;


            //At certain intervals, increase difficulty
            if (_score == 5 || _score == 15 || _score == 30)
            {
                // Make enemies spawn more quickly
                EnemyManager.SpawnInterval = EnemyManager.SpawnInterval - 1.5f;
                UIManager.UpgradeText = "Enemy spawn rate increased!";
            }

            if (_score == 25 || _score == 50)
            {
                // Increase explosion size
                Explosion.UpgradeExplosion();
                UIManager.UpgradeText = "Explosion size increased!";
            }

            if (_score == 10)
            {
                // Make enemies more maneuverable
                EnemyManager.EnemyRotationSpeed = EnemyManager.EnemyRotationSpeed + 1f;

                UIManager.UpgradeText = "Enemy maneuverability increased!";
            }

            if (_score == 20 || _score == 100)
            {
                // Increase enemy speed
                EnemyManager.EnemySpeed = EnemyManager.EnemySpeed + 0.1f;
                UIManager.UpgradeText = "Enemy speed increased!";
            }
        }
    }

    private void Awake()
    {
        // Singleton
        _instance = this;
        DontDestroyOnLoad(gameObject);   
    }

}
