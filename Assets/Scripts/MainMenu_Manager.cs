using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu_Manager : MonoBehaviour {


    public GameObject MainMenu;
    public GameObject StageSelect;

    public void Play()
    {
        MainMenu.SetActive(false);
        StageSelect.SetActive(true);
    }

    public void Return()
    {
        MainMenu.SetActive(true);
        StageSelect.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SelectLVL1()
    {
        SceneManager.LoadScene("Killian");
    }

    public void SelectLVL2()
    {
        SceneManager.LoadScene("Killian2");
    }
}
