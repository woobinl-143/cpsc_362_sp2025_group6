using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public Transform bodyPrefab;
    public float snakeSpeed = 16.0f;
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;

    private Vector2 direction = Vector2.up;
    private List<Transform> body;
    private bool moving = false;

    private bool isGameOver = false;
    void Start()
    {
        body = new List<Transform> { this.transform };
    }

    void Update()
    {
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
        if (isGameOver || !moving) return;

        for (int i = body.Count - 1; i > 0; i--)
        {
            body[i].position = body[i - 1].position;
        }

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
        if (other.tag == "Apple")
        {
            Grow();
        }
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
        snakeSpeed = 0.0f;
        isGameOver = true;

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
