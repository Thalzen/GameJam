using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGate : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
     private Player _player;
    private void Start()
    {
        _player = FindObjectOfType<Player>();
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f,-1f) * speed;
    }

    private void Update()
    {
        // if (_player.EnteringWaterGate == true)
        // {
        //     GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0) * speed;
        // }
        // else
        // {
        //      GetComponent<Rigidbody2D>().velocity = new Vector2(0f,-1f) * speed;
        // }
        if (_player.EnteringWaterGate == true)
        {
            speed = 1.5f;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f,-1f) * speed;
        }
        else
        {
            speed = 5f;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f,-1f) * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
