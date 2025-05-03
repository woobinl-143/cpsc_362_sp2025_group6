using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Serialization;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public BoxCollider2D TopSpawner;
    public BoxCollider2D BottomSpawner;
    public BoxCollider2D LeftSpawner;
    public BoxCollider2D RightSpawner;

    public GameObject enemyV;
    public GameObject enemyH;

    private string[] locations = { "top", "bottom", "left", "right" };

    public float spawnInterval = 10.0f;

    void Start()
    {
        bool spawn = GameHandler.Spawn;
        if (spawn)
            InvokeRepeating("SpawnEnemy", 1f, spawnInterval);
    }

    private void SpawnEnemy()
    {
        // Declares location, enemy, and direction enemy moves
        Bounds bounds;
        GameObject prefab;
        Vector2 direction;
        // Used to determine which location to spawn
        int num = Random.Range(0, locations.Length);
        string location = locations[num];
        // Set direction to move across screen
        if (location == "top") {
            bounds = TopSpawner.bounds;
            prefab = enemyV;
            direction = Vector2.down;
        } else if (location == "bottom") {
            bounds = BottomSpawner.bounds;
            prefab = enemyV;
            direction = Vector2.up;
        } else if (location == "left") {
            bounds = LeftSpawner.bounds;
            prefab = enemyH;
            direction = Vector2.right;
        } else {
            bounds = RightSpawner.bounds;
            prefab = enemyH;
            direction = Vector2.left;
        }
        // Get random position in the spawner
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        Vector3 spawnPos = new Vector3(Mathf.Round(x), Mathf.Round(y), 0f);
        // Create enemy object
        GameObject enemy = Instantiate(prefab, spawnPos, Quaternion.identity);
        enemy.GetComponent<Enemy>().SetDirection(direction);
    }
}