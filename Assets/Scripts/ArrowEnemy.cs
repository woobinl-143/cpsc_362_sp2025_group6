using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowEnemy : MonoBehaviour
{
    public enum Direction { Up, Down, Left, Right }
    // this.tag = "Collidable";
    public float arrowSpeed = 10f;
    public Direction moveDirection = Direction.Right;
    // hopefully this makes sure that it doesn't automatically delete itself w/e it spawns
    public float spawnOffset = 0.5f;
    
    private Bounds movementBounds;
    private Vector2 directionVector;

    private void Start()
    {
        // i don't know how to find the bounds but that should be somewhere here
        
        // adjust spawn position based on direction
        Vector2 spawnPosition = CalculateSpawnPosition();
        transform.position = spawnPosition;
        moveDirection = (Direction)Random.Range(0,4);
        
        // set direction vector
        switch (moveDirection)
        {
            case Direction.Up:
                directionVector = Vector2.up;
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case Direction.Down:
                directionVector = Vector2.down;
                transform.rotation = Quaternion.Euler(0, 0, 270);
                break;
            case Direction.Left:
                directionVector = Vector2.left;
                transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case Direction.Right:
                directionVector = Vector2.right;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
        }
    }

    private Vector2 CalculateSpawnPosition()
    {
        float x = 0f, y = 0f;
        
        switch (moveDirection)
        {
            case Direction.Up:
                x = Random.Range(movementBounds.min.x, movementBounds.max.x);
                y = movementBounds.min.y - spawnOffset;
                break;
            case Direction.Down:
                x = Random.Range(movementBounds.min.x, movementBounds.max.x);
                y = movementBounds.max.y + spawnOffset;
                break;
            case Direction.Left:
                x = movementBounds.max.x + spawnOffset;
                y = Random.Range(movementBounds.min.y, movementBounds.max.y);
                break;
            case Direction.Right:
                x = movementBounds.min.x - spawnOffset;
                y = Random.Range(movementBounds.min.y, movementBounds.max.y);
                break;
        }
        
        return new Vector2(x, y);
    }

    private void Update()
    {
        transform.Translate(directionVector * arrowSpeed * Time.deltaTime);
        
        // check if out of bounds
        if (!movementBounds.Contains(transform.position))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if i can't figure out how to put "collidable" tag on this

        // if (other.CompareTag("Player") || other.CompareTag("Player2"))
        // {
        //     // trigger game over on the snake
        //     other.GetComponent<Snakes>().GameOver();
        //     Destroy(gameObject);
        // }else 
        if (other.CompareTag("Collidable"))
        {
            // destroy arrow if it hits a wall or other collidable
            Destroy(gameObject);
        }
    }
}
