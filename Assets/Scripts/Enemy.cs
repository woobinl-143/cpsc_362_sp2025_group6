using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector2 direction = Vector2.zero;
    public float speed = 2f;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    private void FixedUpdate()
    {
        transform.Translate(direction * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Deleter"))
        {
            Destroy(gameObject);
        }
    }
}
