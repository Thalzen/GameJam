using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reactor : MonoBehaviour
{

    private Player _player;
   
    void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( _player.GetComponent<Renderer>().enabled == true)
        {
            if (_player.EnteringWaterGate && _player.isDying == false || _player.IsUnderWater)
            {
                gameObject.GetComponent<Renderer>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<Renderer>().enabled = true;
            }
            
        }
        else
        {
            gameObject.GetComponent<Renderer>().enabled = false;
        }
    }
}
