using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIStartMenu : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject QuitMenu;


    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void LoadGame(string scenename)
    {
        SceneManager.LoadScene(sceneName: scenename);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 && Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1f)
        {
            QuitMenu.SetActive(false);
            PauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1 && Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0f)
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
