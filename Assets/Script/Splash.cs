using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    private Player _player;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _player = FindObjectOfType<Player>();
    }


    void Update()
    {
        if (_player.EnteringWaterGate == true && _player.isDying == false)
        {
            Debug.Log("Splash");
            _animator.Play("Splash");
        }
    }
}
