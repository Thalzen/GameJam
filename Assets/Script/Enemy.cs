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
    private Player _player;
    private bool IsAlreadyFiring;
    private Animator _animator;
    private bool isDestroyed;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _animator = GetComponent<Animator>();
        target = GameObject.Find("Player");
    }

    private void Update()
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.transform.position.x,4f), step);
        Fire();
    }

    private void FixedUpdate()
    {
        if (gameObject.transform.position.y >=4f)
        {
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bullet") && isDestroyed == false)
        {
            isDestroyed = true;
            gameObject.GetComponent<Collider2D>().enabled = false;
            StartCoroutine(Destruction());
            
        }
        
    }

    private void Fire()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(cannon.transform.position, -Vector2.up);
        if (hit.collider.gameObject.CompareTag("Player") && _player.IsUnderWater == false == IsAlreadyFiring == false)
        {
            IsAlreadyFiring = true;
            StartCoroutine(OpenFire());
        }

    }

    private IEnumerator OpenFire()
    {
        Instantiate(bulletprefab, cannon.transform.position, cannon.transform.rotation);
        yield return new WaitForSeconds(0.6f);
        IsAlreadyFiring = false;
    }

    private IEnumerator Destruction()
    {
        _animator.Play("Destruction");
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
    }
}
