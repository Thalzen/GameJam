using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _Mine;
    [SerializeField] private Transform[] spawnPos;
    private bool Spawning;
    private float spawnRate = 2.5f;
    private float startTimer = 1.2f;
    private bool waiting = true;
    

    void Start()
    {
        StartCoroutine(WaitTimer());
    }

    private void FixedUpdate()
    {
        if (!Spawning && !waiting)
        {
            
            StartCoroutine(SpawnWaterMine());
            Spawning = true;
        }

    }

    IEnumerator SpawnWaterMine()
    {
        var position = new Vector2(Random.Range(spawnPos[0].position.x, spawnPos[1].position.x), 7f);
        Instantiate(_Mine,position, gameObject.transform.rotation);
        yield return new WaitForSeconds(spawnRate);
        Spawning = false;
    }

    IEnumerator WaitTimer()
    {
        yield return new WaitForSeconds(startTimer);
        waiting = false;
    }
}
