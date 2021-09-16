using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    // Used the following tutorial for having enemies rotate towards the player:
    // https://www.youtube.com/watch?v=mKLp-2iseDc&ab_channel=KristerCederlund

    // How fast enemy moves forward
    private float _movementSpeed;

    // Movement target is a child object in front of the enemy
    // It is used to move the enemy forward regardless of its rotation.
    private Transform _movementTarget;

    private float _rotationSpeed;

    private GameObject _player;

    private void Start()
    {
        _movementTarget = gameObject.transform.GetChild(0);
        _movementSpeed = EnemyManager.enemySpeed;
        _rotationSpeed = EnemyManager.maximumEnemyRotation;
        _player = EnemyManager.player;
    }

    private void FixedUpdate()
    {
        ManageMovement();
    }

    private void ManageMovement()
    {
        // Move forward
        transform.position = Vector3.MoveTowards(transform.position, _movementTarget.transform.position, _movementSpeed);

        // Rotate towards player
        Vector2 direction = _player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);

    }

}
