using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject bulletprefab;
    [SerializeField] private GameObject cannon;
    private GameObject target;
    [SerializeField] private float speed;

    private void Start()
    {
        target = GameObject.Find("Player");
    }

    private void Update()
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.transform.position.x,4f), step);
        if (gameObject.transform.position.y >=4f)
        {
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        
        RaycastHit2D hit = Physics2D.Raycast(cannon.transform.position, -Vector2.up);
        //Debug.DrawRay(cannon.transform.position,-Vector2.up, Color.red);

        if (hit.collider.gameObject.CompareTag("Player"))
        {
            Instantiate(bulletprefab, cannon.transform.position, cannon.transform.rotation);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
        
    }
}
