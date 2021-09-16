using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static float _enemySpeed;
    private static float _enemyRotationSpeed;
    private static GameObject _player;

    public static float enemySpeed
    {
        get
        {
            return _enemySpeed;
        }
    }

    public static float maximumEnemyRotation
    {
        get
        {
            return _enemyRotationSpeed;
        }
    }

    public static GameObject player
    {
        get
        {
            return _player;
        }
    }

    private static EnemyManager _instance;

    public static EnemyManager Instance
    {
        get
        {
            if (_instance == null)
            {
                EnemyManager singleton = GameObject.FindObjectOfType<EnemyManager>();
                if (singleton == null)
                {
                    GameObject go = new GameObject();
                    _instance = go.AddComponent<EnemyManager>();
                }
            }
            return _instance;
        }
    }

    

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);

        _enemySpeed = 0.05f;
        _enemyRotationSpeed = 0.8f;
        _player = GameObject.Find("Player");
    }

    
}
