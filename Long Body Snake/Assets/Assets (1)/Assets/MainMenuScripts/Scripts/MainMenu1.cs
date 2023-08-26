using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu1 : MonoBehaviour
{
    public GameObject credits;
    public GameObject creditsText;

    public GameObject settings;

    public AudioSource click;

    public GameObject[] buttons;

    [SerializeField] Animator cutscene;
    [SerializeField] Animator player;

    private void Start()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(true); 
        }
        credits.SetActive(false);
    }

    public void StartGame()
    {
        click.Play();
        cutscene.SetTrigger("Start");
        player.SetTrigger("Swim");
    }

    public void OpenCredits()
    {
        click.Play();

        credits.SetActive(true);
        creditsText.transform.position = new Vector3(creditsText.transform.position.x, 143f, 0);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }
    }

    public void CloseCredits()
    {
        click.Play();

        credits.SetActive(false);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(true);
        }
    }

    public void OpenSettings()
    {
        click.Play();

        settings.SetActive(true);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }
    }

    public void CloseSettings()
    {
        click.Play();

        settings.SetActive(false);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(true);
        }
    }

    public void QuitGame()
    {
        click.Play();

        Debug.Log("Quit");
        Application.Quit();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
