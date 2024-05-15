using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int numberOfSpawnPoints = 10;
    public float spawnRadius = 10f;

    void Start()
    {
        int difficulty = 1;

        if (SceneController.Instance != null)
        {
            difficulty = SceneController.Instance.difficulty;
        }
        for (int i = 0; i < difficulty; i++)
        {
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
            spawnPosition.y = transform.position.y;
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
