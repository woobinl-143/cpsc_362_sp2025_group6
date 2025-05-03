using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    // Dprite used for body
    public Transform bodyPrefab;
    // public float snakeSpeed = 16.0f;
    // Directional inputs set to default
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    // Default direction
    private Vector2 direction = Vector2.up;
    // Used to keep track of snake
    private List<Transform> body;
    // Used to make snake stationary on start
    private bool moving = false;

    private bool isGameOver = false;
    void Start()
    {
        body = new List<Transform> { this.transform };
    }

    void Update()
    {
        // Gets input from player and sets direction
        if (Input.GetKeyDown(upKey) && direction != Vector2.down)
        {
            direction = Vector2.up;
            moving = true;
        }
        else if (Input.GetKeyDown(downKey) && direction != Vector2.up)
        {
            direction = Vector2.down;
            moving = true;
        }
        else if (Input.GetKeyDown(leftKey) && direction != Vector2.right)
        {
            direction = Vector2.left;
            moving = true;
        }
        else if (Input.GetKeyDown(rightKey) && direction != Vector2.left)
        {
            direction = Vector2.right;
            moving = true;
        }
    }

    void FixedUpdate()
    {
        // Update each body part
        if (isGameOver || !moving) return;

        for (int i = body.Count - 1; i > 0; i--)
        {
            body[i].position = body[i - 1].position;
        }
        // Create new vector for updated movement
        transform.position = new Vector3(
            Mathf.Round(transform.position.x + direction.x),
            Mathf.Round(transform.position.y + direction.y),
            0.0f
        );
    }

    private void Grow()
    {
        Transform block = Instantiate(bodyPrefab);
        block.position = body[body.Count - 1].position;
        body.Add(block);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If snake touches apple execute grow
        if (other.tag == "Apple")
        {
            Grow();
        }
        // When snake should lose game
        else if (other.CompareTag("Collidable") || other.CompareTag("Player") || other.CompareTag("Player2"))
        {

            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayPlayerDeathSound();
            }
            GameOver();
        }
    }

    private void GameOver()
    {
        // snakeSpeed = 0.0f;
        isGameOver = true;
        // Get gameover function
        GameOver gameOverScript = FindObjectOfType<GameOver>();
        if (gameOverScript != null)
        {
            Debug.Log("Calling GameOver Trigger");
            gameOverScript.TriggerGameOver();
        }
        else
        {
            Debug.LogWarning("GameOver script not found in scene!");
        }
    }
}
