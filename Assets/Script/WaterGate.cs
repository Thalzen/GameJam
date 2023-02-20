using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGate : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f,-1f) * speed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
