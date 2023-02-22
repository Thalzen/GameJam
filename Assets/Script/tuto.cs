using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tuto : MonoBehaviour
{
    [SerializeField] private GameObject tutotext;
    [SerializeField] private GameObject[] image;
    [SerializeField] private GameObject[] text;
    [SerializeField] private GameObject[] fleche;
    private int i = 0;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (i < 5 )
            {
                incrementimage();
                incrementtext();
                incrementfleche();
                i++;
            }
            
        }

        if (Input.GetMouseButtonUp(0) && i == 5)
        {
            SceneManager.LoadScene(sceneName:"Level1");
        }
    }

    private void incrementimage()
    {
        image[i].SetActive(false);
        image[i + 1].SetActive(true);
    }

    private void incrementtext()
    {
        text[i].SetActive(false);
        text[i + 1].SetActive(true);
    }
    
    private void incrementfleche()
    {
        if (i < 4)
        {
            fleche[i].SetActive(false);
            fleche[i + 1].SetActive(true);
        }
        if (i == 4)
        {
            fleche[i].SetActive(false);
            fleche[i + 1].SetActive(true);
            tutotext.SetActive(false);
            
        }
        
        
    }
}