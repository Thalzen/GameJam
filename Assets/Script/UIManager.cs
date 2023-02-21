using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]private AppDatas _appDatas;
    [SerializeField]private TMP_Text killCountertext;
    private void OnEnable()
    {
        Enemy.EnemyKilled += RefreshKillCounter;
    }
    private void OnDisable()
    {
        Enemy.EnemyKilled -= RefreshKillCounter;
    }




    private void RefreshKillCounter()
    {
        killCountertext.text = "Kill : " + _appDatas.killCounter.ToString() + "/100";
    }
}

