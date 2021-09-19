using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Explosion : MonoBehaviour
{

    
    private float _radius;
    [SerializeField]
    private float _minRadius;
    [SerializeField]
    private float _maxRadius;

    private float _explosionDuration = 2;

    private float _explosionTimer = 0;

    private bool _hasExploded = false;

    private void Awake()
    {
        // Randomly determine explosion size within a range
        _radius = Random.Range(_minRadius, _maxRadius);
    }

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
                Destroy(gameObject);
            }
        }
        
    }
}
