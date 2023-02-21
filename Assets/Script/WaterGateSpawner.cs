using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaterGateSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] waterGate;
    [SerializeField] private Transform[] spawnPos;
    private bool Spawning;
    private float spawnRate = 2f;
    

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (!Spawning)
        {
            
            StartCoroutine(SpawnWaterGate());
            Spawning = true;
        }
        
    }

    IEnumerator SpawnWaterGate()
    {
        var position = new Vector2(Random.Range(spawnPos[0].position.x, spawnPos[1].position.x), 7f);
        Instantiate(waterGate[Random.Range(0,waterGate.Length)],position, gameObject.transform.rotation);
        yield return new WaitForSeconds(spawnRate);
        Spawning = false;
    }
}

