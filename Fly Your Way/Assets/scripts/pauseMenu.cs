using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class pauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    

    public GameObject pauseMenuUi;
    public GameObject skinMenuUi;

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
            
        }
    }

    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        skinMenuUi.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        //CloseBannerAd();
    }

    public void Pause()
    {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        

    }

    public void Skins()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        skinMenuUi.SetActive(true);

    }

    public void QuitGame()
    {
       
        Application.Quit();
    }
}
