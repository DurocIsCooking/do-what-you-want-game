using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    // Used the following tutorial for having enemies rotate towards the player:
    // https://www.youtube.com/watch?v=mKLp-2iseDc&ab_channel=KristerCederlund

    // Speed at which enemy moves forward
    private float _movementSpeed;

    // Speed at which enemy rotates towards player
    private float _rotationSpeed;

    // Movement target is a child object in front of the enemy
    // It is used to move the enemy forward regardless of its rotation.
    private Transform _movementTarget;

    // Player reference
    private GameObject _player;

    // Explosion reference
    [SerializeField]
    private GameObject _explosion;

    private void Start()
    {
        _movementTarget = gameObject.transform.GetChild(0);
        _movementSpeed = EnemyManager.enemySpeed;
        _rotationSpeed = EnemyManager.maximumEnemyRotation;
        _player = EnemyManager.player;

        // Spawn the enemy facing a random direction
        transform.Rotate(0, 0, Random.Range(0, 360), Space.Self);
    }

    private void Update()
    {
        // Rotate towards player
        Vector2 direction = _player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        // Move forward
        transform.position = Vector3.MoveTowards(transform.position, _movementTarget.transform.position, _movementSpeed);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.tag != "Wall")
            Explode();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Explode();
    }

    private void Explode()
    {
        // score++
        Instantiate(_explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


}
