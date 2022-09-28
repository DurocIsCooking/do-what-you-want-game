using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// This script is on an explosion prefab created by missiles. It controls their expansion and retraction (aka implosion).
public class Explosion : MonoBehaviour
{

    private float _explosionDuration = 2;
    private float _implosionDuration = 1;
    private float _explosionTimer = 0;
    private float _implosionTimer = 0;
    private bool _hasExploded = false;
    private bool _hasImploded = false;

    private float _radius;
    private static float _minRadius = 5;
    private static float _maxRadius = 8;

    private void Awake()
    {
        // Randomly determine explosion size within a range
        _radius = Random.Range(_minRadius, _maxRadius);
        // Add to list of objects to destroy on restart
        MenuManager.Instance.DestroyOnRestart.Add(gameObject);
    }

    // First, expand the explosion (according to explosionTimer), then reduce it (according to implosionTimer)
    private void Update()
    {
        // Use dotween for explosion growth rate
        if(_hasExploded == false)
        {
            transform.DOScale(_radius, _explosionDuration);
            _hasExploded = true;
        }
        else
        {
            _explosionTimer += Time.deltaTime;
            if(_explosionTimer >= _explosionDuration)
            {
                Implosion();
            }
        }
        
    }

    // Reduce explosion size
    private void Implosion()
    {
        // Use dotween for implosion rate
        if (_hasImploded == false)
        {
            transform.DOScale(0, _implosionDuration);
            _hasImploded = true;
        }
        else
        {
            _implosionTimer += Time.deltaTime;
            if (_implosionTimer >= _implosionDuration)
            {
                Destroy(gameObject);
            }
        }
    }

    public static void UpgradeExplosion()
    {
        // Increase explosion radius
        _minRadius += 2;
        _maxRadius += 2;
    }
}
