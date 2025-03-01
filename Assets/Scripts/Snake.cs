using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 direction = Vector2.up;
    private List<Transform> body;
    private bool IsGameOver = false;
    public Transform bodyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        body = new List<Transform>();
        body.Add(this.transform);
    }

    private void Grow()
    {
        Transform block = Instantiate(this.bodyPrefab);
        block.position = body[body.Count - 1].position;

        body.Add(block);
    }

    // private void Reset()
    // {
    // }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Apple") {
            Grow();
        }
        else if (other.tag == "Collidable")
        {
            IsGameOver = true;
            // GameOver();
            // Reset();
        }
    }
    // Update is called once per frame
    private void Update()
    {
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
