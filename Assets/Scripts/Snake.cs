using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Todo make a parent class "snake" that has snake1 and snake2
public class Snake : MonoBehaviour
{
    // Snake intially moves up
    private Vector2 direction = Vector2.up;
    // List to keep track of body parts
    private List<Transform> body;
    // Used to check if game is active 
    private bool IsGameOver = false;
    // Parts used to make the snake
    public Transform bodyPrefab;

    public float SnakeSpeed = 16.0f;

    // controls
    

    // Start is called before the first frame update
    void Start()
    {
        body = new List<Transform>();
        body.Add(this.transform);
    }
    // Extends body by one part/square
    private void Grow()
    {
        Transform block = Instantiate(this.bodyPrefab);
        block.position = body[body.Count - 1].position;

        body.Add(block);
    }

    // private void Reset()
    // {
        // Clear the snake
        // Move snake to intial position
        
    // }

    private void GameOver()
    {
        SnakeSpeed = 0.0f;
        IsGameOver = true;
        // disable movement
        // play sound effect 
    }
    // Function to handle collisions
    private void OnTriggerEnter2D(Collider2D other)
    {
        // When snake touches apple increase size
        if (other.tag == "Apple") {
            Grow();
        }
        // If snake touches wall or itself, end the game
        else if ((other.tag == "Collidable") || (other.tag == "Player2"))
        {
            SoundManager.Instance.PlayPlayerDeathSound();
            GameOver();
            // Reset();
        }
    }
    // Update is called once per frame
    private void Update()
    {
        // Handles direction inputs (using WASD)
       if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down)
       {
        direction = Vector2.up;
       } 
       else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up)
       {
        direction = Vector2.down;
       }
       else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right)
       {
        direction = Vector2.left;
       }
       else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left)
       {
        direction = Vector2.right;
       }
    }
    private void FixedUpdate()
    {
        if (!IsGameOver)
        {
        for (int i = body.Count - 1; i > 0; i--)
        {
            body[i].position = body[i - 1].position;
        }
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x + direction.x),
            Mathf.Round(this.transform.position.y + direction.y),
            0.0f
        );
        }
    }
}
