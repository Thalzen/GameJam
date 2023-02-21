using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private Vector2 movedirection;
    private Rigidbody2D rb;
    private SpriteRenderer _renderer;
    [SerializeField] private GameObject bulletprefab;
    [SerializeField] private GameObject cannon1;
    [SerializeField] private GameObject cannon2;
    [SerializeField] private Sprite tiltleft;
    [SerializeField] private Sprite tiltright;
    [SerializeField] private Sprite tiltnormal;
    private Animator _animator;
    private int LayerIgnoreRaycast;
    private int LayerDefault;
    public bool IsUnderWater = false;
    public bool EnteringWaterGate = false;
    public bool WaterGateCD = false;
    
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        LayerDefault = LayerMask.NameToLayer("Default");
        LayerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
        _renderer = GetComponent<SpriteRenderer>();
        Physics.IgnoreLayerCollision(6,2);
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {

        float Magnitude = Mathf.Clamp(movedirection.x,-1,1);
        _animator.SetFloat("Magnitude",Magnitude);
        

        if (EnteringWaterGate == true)
        {
            movedirection = new Vector2(0, 0);
        }
        else
        {
            movedirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        
        OpenFire();
        
    }

    private void FixedUpdate()
    {
        rb.velocity = movedirection * speed;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (other.CompareTag("WaterGate"))
            {
           
                UnderWater();
            }
        }
        
    }

    private void OpenFire()
    {
        if(Input.GetKeyDown(KeyCode.Space) && IsUnderWater == false && EnteringWaterGate == false)
        {
            Instantiate(bulletprefab, cannon1.transform.position, cannon1.transform.rotation);
            //Instantiate(bulletprefab, cannon2.transform.position, cannon2.transform.rotation);
        }
    }

    private void UnderWater()
    {
        
            Debug.Log("WaterGate");
            if (!WaterGateCD)
            {
                WaterGateCD = true;
                EnteringWaterGate = true;
                StartCoroutine(EnterExitWater());

            
            }

    }

    private IEnumerator EnterExitWater()
    {
        if (!IsUnderWater)
        {
            
            gameObject.layer = LayerIgnoreRaycast;
            _animator.Play("Entering Water");
            yield return new WaitForSeconds(0.3f);
            EnteringWaterGate = false;
            _renderer.color = new Color(0.3170612f, 0.4462543f, 0.7075472f,1f);
            _renderer.sortingLayerName = "Default";
            IsUnderWater = true;
            

        }
        else if (IsUnderWater)
        {
            _animator.Play("Exiting Water");
            yield return new WaitForSeconds(0.3f);
            EnteringWaterGate = false;
            _renderer.color = new Color(1f, 1f, 1f,1f);
            _renderer.sortingLayerName = "Foreground";
            _renderer.sortingOrder = 1;
            gameObject.layer = LayerDefault;
            IsUnderWater = false;

        }

        yield return new WaitForSeconds(1f);
        WaterGateCD = false;

    }
}
 