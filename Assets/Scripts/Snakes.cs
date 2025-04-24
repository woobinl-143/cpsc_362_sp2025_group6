using System.Collections.Generic;
using UnityEngine;

public class Snakes : MonoBehaviour
{
    private Vector2 direction = Vector2.up;
    private List<Transform> body;
    private bool IsGameOver = false;
    public Transform bodyPrefab;
    public float SnakeSpeed = 16.0f;

    void Start()
    {
        Time.timeScale = 1f;
        body = new List<Transform>();
        body.Add(this.transform);
    }

    private void Grow()
    {
        Transform block = Instantiate(this.bodyPrefab);
        block.position = body[body.Count - 1].position;
        body.Add(block);
    }

    private void GameOver()
    {
        SnakeSpeed = 0.0f;
        IsGameOver = true;
        FindObjectOfType<GameOver>().TriggerGameOver();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Apple")
        {
            Grow();
        }
        else if (other.tag == "Collidable")
        {
            GameOver();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && direction != Vector2.down)
            direction = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.DownArrow) && direction != Vector2.up)
            direction = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && direction != Vector2.right)
            direction = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.RightArrow) && direction != Vector2.left)
            direction = Vector2.right;
    }

    private void FixedUpdate()
    {
        if (!IsGameOver)
        {
            for (int i = body.Count - 1; i > 0; i--)
                body[i].position = body[i - 1].position;

            this.transform.position = new Vector3(
                Mathf.Round(this.transform.position.x + direction.x),
                Mathf.Round(this.transform.position.y + direction.y),
                0.0f
            );
        }
    }
}
