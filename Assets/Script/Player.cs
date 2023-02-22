using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private Vector2 movedirection;
    private Rigidbody2D rb;
    private SpriteRenderer _renderer;
    [SerializeField] private GameObject bulletprefab;
    [SerializeField] private GameObject cannon1;
    [SerializeField] private GameObject cannon2;
    private Animator _animator;
    [SerializeField] private Image Redbar;
    [SerializeField] private Image Bluebar;
    private int LayerIgnoreRaycast;
    private int LayerDefault;
    public bool IsUnderWater = false;
    public bool EnteringWaterGate = false;
    public bool WaterGateCD = false;
    private float CurrentHeat;
    private float CurrentIce;
    private float MaxHeat = 100;
    private bool isCooling;
    private bool isIcing;
    public bool isDying;
    [SerializeField]private float Health = 100f;
    [SerializeField]private Image Healthbar;
    private bool isTakingDamage = false;
    [SerializeField] private AppDatas _appDatas;
    private AudioSource _audio;
    [SerializeField]private AudioClip lasersound;
    [SerializeField]private AudioClip splashsound;
    [SerializeField]private AudioClip destructsound;
    private bool BeingHealed;


    void Start()
    {
        _audio = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        LayerDefault = LayerMask.NameToLayer("Default");
        LayerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
        _renderer = GetComponent<SpriteRenderer>();
        Physics.IgnoreLayerCollision(6,2);
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (IsUnderWater && CurrentHeat > 0 && !isCooling && !isIcing)
        {
            StartCoroutine(DecreaseHeat());
            isCooling = true;
        }
        else if(IsUnderWater && CurrentHeat == 0 && !isIcing && !isCooling)
        {
            StartCoroutine(IncreaseIce());
            isIcing = true;
        }


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
        if (_appDatas.killCounter == 100)
        {
            SceneManager.LoadScene(sceneName: "YouWin");
        }
        
        if (Health == 0f && isDying == false)
        {
            GameOverDestroyed();
        }
        if (CurrentIce == 100f && isDying == false)
        {
            GameOverSunked();
        }
        Redbar.fillAmount = CurrentHeat / MaxHeat;
        Bluebar.fillAmount = CurrentIce / MaxHeat;
        rb.velocity = movedirection * speed;
        CurrentHeat = Mathf.Clamp(CurrentHeat, 0, 100);
        CurrentIce = Mathf.Clamp(CurrentIce, 0, 100);
        Healthbar.fillAmount = Health / MaxHeat;
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("EnemyBullet") && !isTakingDamage && !EnteringWaterGate)
        {
            TakeDamage();
        }

        if (col.CompareTag("Mine") && IsUnderWater && !isTakingDamage && !EnteringWaterGate )
        {
            TakeDamage();
        }
        if (col.CompareTag("Heart") && !IsUnderWater && !EnteringWaterGate && BeingHealed == false)
        {
            GetHealth();
        }
    }

    private void OpenFire()
    {
        if(Input.GetKeyDown(KeyCode.Space) && IsUnderWater == false && EnteringWaterGate == false && CurrentHeat != 100f)
        {
            Heatmeter();
            _audio.PlayOneShot(lasersound);
            Instantiate(bulletprefab, cannon1.transform.position, cannon1.transform.rotation);
            Instantiate(bulletprefab, cannon2.transform.position, cannon2.transform.rotation);
        }
    }

    private void UnderWater()
    {
        if (!WaterGateCD && !isDying)
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
            _audio.PlayOneShot(splashsound);
            yield return new WaitForSeconds(0.3f);
            EnteringWaterGate = false;
            _renderer.color = new Color(0.3170612f, 0.4462543f, 0.7075472f,1f);
            _renderer.sortingLayerName = "Default";
            IsUnderWater = true;
            

        }
        else if (IsUnderWater)
        {
            _animator.Play("Exiting Water");
            _audio.PlayOneShot(splashsound);
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

    private IEnumerator Gettingheal()
    {
        Health += 10f;
        Healthbar.fillAmount = Health / MaxHeat;
        yield return new WaitForSeconds(1f);
        BeingHealed = false;
    }

    private IEnumerator IncreaseIce()
    {
        CurrentIce += 10f;
        yield return new WaitForSeconds(0.5f);
        isIcing = false;
    }
    private IEnumerator DecreaseHeat()
    {
        CurrentHeat -= 10f;
        yield return new WaitForSeconds(0.5f);
        isCooling = false;

    }

    private IEnumerator DiedIce()
    {
        _animator.Play("Sunking");
        EnteringWaterGate = true;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(sceneName: "GameOverSunked");
    }

    private IEnumerator TakingDamage()
    {
        Health -= 10f;
        Healthbar.fillAmount = Health / MaxHeat;
        _renderer.enabled = false;
        yield return new WaitForSeconds(0.2f);
        _renderer.enabled = true;
        yield return new WaitForSeconds(0.2f);
        _renderer.enabled = false;
        yield return new WaitForSeconds(0.2f);
        _renderer.enabled = true;
        yield return new WaitForSeconds(0.2f);
        _renderer.enabled = false;
        yield return new WaitForSeconds(0.2f);
        _renderer.enabled = true;
        yield return new WaitForSeconds(0.2f);
        _renderer.enabled = false;
        yield return new WaitForSeconds(0.2f);
        _renderer.enabled = true;
        isTakingDamage = false;
    }
    
    private void GetHealth()
    {
        BeingHealed = true;
        StartCoroutine(Gettingheal());
    }

    private IEnumerator DiedDestroyed()
    {
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        _animator.Play("Destroyed");
        EnteringWaterGate = true;
        yield return new WaitForSeconds(1.6f);
        SceneManager.LoadScene(sceneName: "GameOverDestroyed");
    }
    

    private void Heatmeter()
    {
        if (CurrentHeat >= 0 && CurrentIce == 0f)
        {
            CurrentHeat += 5f;
        }
        else if(CurrentIce > 0)
        {
            CurrentIce -= 10f;
        }
    }

    private void GameOverSunked()
    {
        isDying = true;
        StartCoroutine(DiedIce());
    }

    private void GameOverDestroyed()
    {
        isDying = true;
        _audio.PlayOneShot(destructsound);
        StartCoroutine(DiedDestroyed());
    }

    private void TakeDamage()
    {
        isTakingDamage = true;
        StartCoroutine(TakingDamage());
    }
}
 