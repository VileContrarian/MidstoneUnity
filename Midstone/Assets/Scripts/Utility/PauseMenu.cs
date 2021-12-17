using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool IsPaused = false;
    public bool CanPause = true;
    public GameObject PauseMeu;
    public Text PauseText, Lives;

    public static bool over;
    public static int lives;

    void Start()
    {
        over = false;
        lives = 3;
        PauseMeu.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
        CanPause = false;

        PauseText.text = "WELCOME TO THE GAME!";
    }
    void Update()
    {
        UpdateLife();
        if (over)
        {
            GameOver();
        }
        else
        {
            if (CanPause)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (IsPaused)
                    {
                        Pause();
                        Resume();
                    }
                    else if (!IsPaused)
                    {
                        Pause();
                    }
                }
            }
        }
    }

    public void Resume()
    {
        PauseMeu.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void Pause()
    {
        PauseMeu.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;

        PauseText.text = "GAME PAUSED";
    }

    public void GameOver()
    {
        PauseMeu.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
        CanPause = false;

        PauseText.text = "GAMEOVER! NOW WHAT?";
    }

    public void Play()
    {
        if (over)
        {
            SceneManager.LoadScene("Level1");
        }
        else
        {
            CanPause = true;
            Resume();
        }
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void UpdateLife()
    {
        Lives.text = "Lives Left: " + lives;
    }
}
