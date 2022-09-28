using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

// Tracks score, displays score and upgrade text
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

    // Score
    [HideInInspector] public int Score;
    // Text elements
    public GameObject InGameUI;
    public Text ScoreText;
    public Text UpgradeText;

    private void Awake()
    {
        // Singleton
        _instance = this;
        ResetScore();
    }

    public void SetUpgradeText(string upgradeText)
    {
            // Display text
            UpgradeText.color = new Color(1, 1, 1, 1);
            UpgradeText.text = upgradeText;

            // Fade text out
            Instance.Invoke("FadeUpgradeText", 1);
        
    }

    // Fades out upgrade text over time
    private void FadeUpgradeText()
    {
        UpgradeText.DOColor(new Color(1, 1, 1, 0), 3);
    }

    public void ShowUI(bool showText)
    {
        InGameUI.SetActive(showText);
    }

    public void IncrementScore()
    {
        Score += 1;

        // Change score text
        ScoreText.text = "Score: " + Score;

        //At certain intervals, increase difficulty
        if (Score == 5 || Score == 15 || Score == 30)
        {
            // Make enemies spawn more quickly
            EnemyManager.SpawnInterval = EnemyManager.SpawnInterval - 1.5f;
            SetUpgradeText("Enemy spawn rate increased!");
        }

        if (Score == 25 || Score == 50)
        {
            // Increase explosion size
            Explosion.UpgradeExplosion();
            SetUpgradeText("Explosion size increased!");
        }

        if (Score == 10)
        {
            // Make enemies more maneuverable
            EnemyManager.EnemyRotationSpeed = EnemyManager.EnemyRotationSpeed + 1f;
            SetUpgradeText("Enemy maneuverability increased!");
        }

        if (Score == 20 || Score == 100)
        {
            // Increase enemy speed
            EnemyManager.EnemySpeed = EnemyManager.EnemySpeed + 0.1f;
            SetUpgradeText("Enemy speed increased!");
        }
    }

    public void ResetScore()
    {
        Score = 0;
        ScoreText.text = "Score: " + 0;
        UpgradeText.text = "";
        ShowUI(true);
    }
}
