using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f;
    private Vector2 movedirection;
    private Rigidbody2D rb;
    [SerializeField] private GameObject bulletprefab;
    [SerializeField] private GameObject cannon;
    
    
    void Start()
    {
        Physics.IgnoreLayerCollision(6,2);
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {

        movedirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletprefab, cannon.transform.position, cannon.transform.rotation);
        }
        
    }

    private void FixedUpdate()
    {
        rb.velocity = movedirection * speed;
    }
}
