using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPos;
    [SerializeField] private int currentEnemy;
    [SerializeField] private GameObject enemyPrefab;
    private bool IsSpawning;
    [SerializeField] private float spawnTimer = 1f;
    [SerializeField] private float maxSpawn = 6f;

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        currentEnemy = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (currentEnemy <= maxSpawn && IsSpawning == false)
        {
            StartCoroutine(SpawnEnemy());
            IsSpawning = true;
        }

        if (currentEnemy <= 2)
        {
            spawnTimer = 0f;
        }
        else
        {
            spawnTimer = 1f;
        }
    }


    IEnumerator SpawnEnemy()
    {
        
        var position = new Vector2(Random.Range(spawnPos[0].position.x,spawnPos[1].position.x),5.77f);
        Instantiate(enemyPrefab, position, Quaternion.identity);
        yield return new WaitForSeconds(spawnTimer);
        IsSpawning = false;
    }
}
