using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject credits;

    void Start()
    {
        MainMenu();
    }

    public void Play()
    {
        SceneManager.LoadScene("Lvl_1");
    }

    void DisablePanels()
    {
        mainMenu.SetActive(false);
        credits.SetActive(false);
    }

    public void MainMenu()
    {
        DisablePanels();
        mainMenu.SetActive(true);
    }
    public void Credits()
    {
        DisablePanels();
        credits.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
