using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject PauseMenu;
    public KeyCode key = KeyCode.P;

    void Update()
    {
        if (Input.GetKeyDown(key) && Time.timeScale == 1f)
        {
            PauseMenu.SetActive(true);
            Debug.Log("AAAAAAAAAAA");

            Time.timeScale = 0f;
        }
        else if (Input.GetKeyDown(key) && Time.timeScale == 0f)
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void UnPause()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
