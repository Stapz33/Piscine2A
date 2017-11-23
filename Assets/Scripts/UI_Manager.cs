using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {

    public static UI_Manager Instance { get; private set; }

    public GameObject _winMenu;
    public GameObject _loseMenu;
    public GameObject _IGMenu;
    public Image BoostBar;
    private float MaxBoost = 100f;
    private float BoostActive;

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
        BoostActive = Player_Manager.Instance.NumberBoost();
        ReturnBoost();
    }

    public void DispWin()
    {
        _winMenu.SetActive(true);
        _IGMenu.SetActive(false);
    }

    public void DispLose()
    {
        _loseMenu.SetActive(true);
        _IGMenu.SetActive(false);
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
        SceneManager.LoadScene("MenuScene");
    } 

    public void ReturnBoost()
    {
        BoostBar.fillAmount = BoostActive / MaxBoost;
        Debug.Log(BoostBar.fillAmount);
    }
}
