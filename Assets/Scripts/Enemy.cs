using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector2 direction = Vector2.zero;
    public float speed = 2f;

    public void SetDirection(Vector2 dir)
    {
        // Set direction to constant
        direction = dir.normalized;
    }

    private void FixedUpdate()
    {
        // Move enemy
        transform.Translate(direction * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // When enemy hits deleter object, it destroys itself
        if (other.CompareTag("Deleter"))
        {
            Destroy(gameObject);
        }
    }
}
