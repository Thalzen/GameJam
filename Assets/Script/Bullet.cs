using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{ 
    [SerializeField] private float BulletSpeed = 10f;

    private int LayerIgnoreRaycast;
    private int LayerIgnoreBullet;
    
    // Start is called before the first frame update
    void Start()
    {
        LayerIgnoreBullet = LayerMask.NameToLayer("Bullet");
        LayerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
        GetComponent<Rigidbody2D>().velocity = transform.up * BulletSpeed;
        Physics.IgnoreLayerCollision(6,2);
        Physics.IgnoreLayerCollision(6,6);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer != LayerIgnoreRaycast && col.gameObject.layer != LayerIgnoreBullet)
        {
            Destroy(gameObject);
        }
        
    }
}
