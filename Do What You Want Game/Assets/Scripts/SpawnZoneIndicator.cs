using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// The spawnZoneIndicator is a sprite that appears when an enemy is about to spawn, warning the player. This script controls its visual fade and destroys it.
public class SpawnZoneIndicator : MonoBehaviour
{
    private float _lifespan;
    private float _lifeTimer;

    private bool _hasStartedColourChange = false;

    private void Awake()
    {
        // Set lifespan values
        _lifespan = EnemyManager.SpawnInterval;
        _lifeTimer = 0;

        // Set colour to transparent
        transform.GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0.5f, 0);
        transform.Find("OuterEdge").GetComponent<SpriteRenderer>().color = new Color();
        // Add to list of objects to destroy on restart
        MenuManager.Instance.DestroyOnRestart.Add(gameObject);
    }

    private void Update()
    {
        // Use dotween to change alpha over lifespan
        if (_hasStartedColourChange == false)
        {
            transform.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0.35f), _lifespan - 0.05f);
            transform.Find("OuterEdge").GetComponent<SpriteRenderer>().DOColor(new Color(1, 0.5f, 0.5f, 0.5f), _lifespan - 0.05f);
            _hasStartedColourChange = true;
        }


        // Track object lifespan
        _lifeTimer += Time.deltaTime;
        if(_lifeTimer >= _lifespan)
        {
            Destroy(gameObject);
        }
    }
}
