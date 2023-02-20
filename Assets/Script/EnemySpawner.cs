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

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        currentEnemy = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (currentEnemy <= 5f && IsSpawning == false)
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
        
        var position = new Vector2(Random.Range(-3.80f,4.20f),5.77f);
        Instantiate(enemyPrefab, position, Quaternion.identity);
        yield return new WaitForSeconds(spawnTimer);
        IsSpawning = false;
    }
}
