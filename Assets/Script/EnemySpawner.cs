using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPos;

    public GameObject[] enemies;
    
    void Start()
    {
        
    }


    IEnumerator SpawnEnemy()
    {
        yield break;
    }
}
