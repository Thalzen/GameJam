using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Player _player;
    private bool destroyingHeart;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f,-1f) * speed;
        _player = FindObjectOfType<Player>();
    }
    private void Update()
    {
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

        if (col.gameObject.CompareTag("Player") && _player.EnteringWaterGate == false && _player.IsUnderWater == false && destroyingHeart == false)
        {
            destroyingHeart = true;
            StartCoroutine(DestroyHeart());
        }
    }

    private IEnumerator DestroyHeart()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
