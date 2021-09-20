using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{

    private bool _movingUp = false;
    private bool _movingDown = false;
    private bool _movingLeft = false;
    private bool _movingRight = false;

    public float speed;

    private void Update()
    {
        ManageVerticalInputs();
        ManageHorizontalInputs();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void ManageVerticalInputs()
    {
        // On key press, start moving and cancel out other direction
        if (Input.GetKeyDown(KeyCode.W))
        {
            _movingUp = true;
            _movingDown = false;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _movingDown = true;
            _movingUp = false;
        }

        // On key release, stop moving and check if player wants to move in other direction
        if (Input.GetKeyUp(KeyCode.W))
        {
            _movingUp = false;
            if(Input.GetKey(KeyCode.S))
            {
                _movingDown = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            _movingDown = false;
            if (Input.GetKey(KeyCode.W))
            {
                _movingUp = true;
            }
        }
    }

    private void ManageHorizontalInputs()
    {
        // Same logic as vertical inputs
        if (Input.GetKeyDown(KeyCode.A))
        {
            _movingLeft = true;
            _movingRight = false;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _movingRight = true;
            _movingLeft = false;
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            _movingLeft = false;
            if(Input.GetKey(KeyCode.D))
            {
                _movingRight = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            _movingRight = false;
            if (Input.GetKey(KeyCode.A))
            {
                _movingLeft = true;
            }
        }
    }

    private void PlayerMovement()
    {
        if(_movingUp)
        {
            transform.position += new Vector3(0, speed, 0);
        }

        if(_movingDown)
        {
            transform.position += new Vector3(0, -speed, 0);
        }

        if (_movingLeft)
        {
            transform.position += new Vector3(-speed, 0, 0);
        }

        if (_movingRight)
        {
            transform.position += new Vector3(speed, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // If the player touches an explosion, game over

        // Game over UI
        UIManager.GameOver();
        // Destroy player
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // If the player is hit by a missile, game over
        if (col.collider.tag == "Enemy")
        {
            // Game over UI
            UIManager.GameOver();
            // Destroy player
            Destroy(gameObject);
        }
    }
}
