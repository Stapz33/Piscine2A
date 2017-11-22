using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {

    public static UI_Manager Instance { get; private set; }

    public GameObject _winMenu;
    public GameObject _loseMenu;
    public Text timerText;

    public int buildIndex;
    // Use this for initialization

    void Awake()
    {
        Instance = this;
        buildIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DispWin()
    {
        _winMenu.SetActive(true);
    }

    public void DispLose()
    {
        _loseMenu.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene(buildIndex, LoadSceneMode.Single);
    }

    public void Continue()
    {
        SceneManager.LoadScene(buildIndex + 1, LoadSceneMode.Single);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu Scene");
    } 
}
