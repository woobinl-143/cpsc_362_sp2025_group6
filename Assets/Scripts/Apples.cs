using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D appleSpawner;

    private void RandomPos()
    {
        Bounds bounds = this.appleSpawner.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            RandomPos();
        }
    }
    void Start()
    {
        RandomPos();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
