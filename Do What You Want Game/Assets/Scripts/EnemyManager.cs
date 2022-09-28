using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls the spawning of enemies, and stores their global variables (which change as the enemies get upgrades)
public class EnemyManager : MonoBehaviour
{
    // Enemy prefab and global variables
    [SerializeField]
    private GameObject _enemy;
    private static float _enemySpeed;
    private static float _enemyRotationSpeed;
    
    // Used for spawning enemies in random locations
    [SerializeField]
    private static float _spawnZoneHeight = 8;
    [SerializeField]
    private static float _spawnZoneWidth = 10;
    private float _spawnPosX;
    private float _spawnPosY;
    private RangeAttribute _spawnRangeX;
    private RangeAttribute _spawnRangeY;

    // Used to control frequency of enemy spawns
    private static float _spawnInterval;
    private float _spawnTimer;
    [SerializeField] private GameObject _spawnZoneIndicator;
    private GameObject _currentSpawnZone = null;

    [HideInInspector] public GameObject Player;

    // Getters & setters (we want to change these values as the player scores points, to increase difficulty)
    public static float EnemySpeed
    {
        get
        {
            return _enemySpeed;
        }

        set
        {
            _enemySpeed = value;
        }
    }

    public static float EnemyRotationSpeed
    {
        get
        {
            return _enemyRotationSpeed;
        }

        set
        {
            _enemyRotationSpeed = value;
        }
    }

    public static float SpawnInterval
    {
        get
        {
            return _spawnInterval;
        }

        set
        {
            _spawnInterval = value;

            // We never want spawn interval to go below 0.5
            if (_spawnInterval < 0.5f)
                _spawnInterval = 0.5f;
        }
    }

    // Singleton pattern
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
    }

    private void Update()
    {
        // Periodically spawn enemies
        _spawnTimer += Time.deltaTime;
        //Debug.Log("Timer " + _spawnTimer + " Interval: " + SpawnInterval);
        if(_spawnTimer >= _spawnInterval)
        {
            _spawnTimer = 0;

            if(Player != null)
            {
                BeginEnemySpawn();
            }
            
        }
    }

    private void BeginEnemySpawn()
    {
        // If we're already in the process of spawning an enemy, we do not want to spawn more. Otherwise we can get bugs
        if (_currentSpawnZone != null)
            return;

        // For this, we divide the level into nine spawn zones, each one is a rectangle

        // Select random spawn zone
        int spawnZone = (int)Random.Range(0, 9);

        // Generate x position of spawn based on spawn zone
        switch (spawnZone % 3)
        {
            case 0:
                _spawnRangeX = new RangeAttribute(0, _spawnZoneWidth);
                break;
            case 1:
                _spawnRangeX = new RangeAttribute(_spawnZoneWidth, 2 * _spawnZoneWidth);
                break;
            case 2:
                _spawnRangeX = new RangeAttribute(2 * _spawnZoneWidth, 3 * _spawnZoneWidth);
                break;
        }

        // Generate y position of spawn based on spawn zone
        switch (spawnZone / 3)
        {
            case 0:
                _spawnRangeY = new RangeAttribute(0, _spawnZoneHeight);
                break;
            case 1:
                _spawnRangeY = new RangeAttribute(_spawnZoneHeight, 2 * _spawnZoneHeight);
                break;
            case 2:
                _spawnRangeY = new RangeAttribute(2 * _spawnZoneHeight, 3 * _spawnZoneHeight);
                break;
        }
        
        // Check if player is within selected spawnzone
        if (Player.transform.position.x > _spawnRangeX.min && Player.transform.position.x < _spawnRangeX.max && Player.transform.position.y > _spawnRangeY.min && Player.transform.position.y < _spawnRangeY.max)
        {
            // If so, we need to select a different spawnzone. We do not want the spawnzone to appear on top of the player, this is too difficult when the spawn rate is high.
            BeginEnemySpawn();
            return;
        }

        // Signal spawn zone to player with indicator
        _currentSpawnZone = Instantiate(_spawnZoneIndicator, new Vector3(_spawnRangeX.min + 0.5f * _spawnZoneWidth, _spawnRangeY.min + 0.5f * _spawnZoneHeight, 0), Quaternion.identity);

        // Spawn enemy after delay
        Invoke("SpawnEnemy", _spawnInterval - 0.05f);
    }

    private void SpawnEnemy()
    {
        // Set spawn position within selected range
        _spawnPosX = Random.Range(_spawnRangeX.min, _spawnRangeX.max);
        _spawnPosY = Random.Range(_spawnRangeY.min, _spawnRangeY.max);
        // Spawn enemy
        GameObject newMissile = Instantiate(_enemy, new Vector3(_spawnPosX, _spawnPosY, 0), Quaternion.identity);
        // Add enemy to list of objects to destroy on restart
        MenuManager.Instance.DestroyOnRestart.Add(newMissile);
        // Remove spawn zone
        _currentSpawnZone = null;
    }

    public void ResetValues()
    {
        // Set enemy speed values
        _enemySpeed = 0.2f;
        _enemyRotationSpeed = 2f;

        // Set starting timer values
        _spawnTimer = 0;
        _spawnInterval = 5;
    }
}
