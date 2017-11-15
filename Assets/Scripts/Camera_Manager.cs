using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Manager : MonoBehaviour
{

    public static Camera_Manager Instance { get; private set; }

    private GameObject Camera;
    public GameObject cameraFPS;
    public GameObject cameraTPS;
    public GameObject cameraTOP;

    private int activeCamera = 0;

    private void Awake()
    {
        Camera = cameraFPS;
        Instance = this;
    }
    // Use this for initialization
    void ChangeCamera()
    {
        if (Input.GetKeyDown("c"))
        {
            if (activeCamera == 0)
            {
                cameraFPS.SetActive(false);
                cameraTPS.SetActive(true);
                Camera = cameraTPS;
                activeCamera += 1;
                return;
            }
            if (activeCamera == 1)
            {
                cameraTPS.SetActive(false);
                cameraTOP.SetActive(true);
                Camera = cameraTOP;
                activeCamera += 1;
                return;
            }
            if (activeCamera == 2)
            {
                cameraTOP.SetActive(false);
                cameraFPS.SetActive(true);
                Camera = cameraFPS;
                activeCamera = 0;
                return;
            }
        }
    }
    private void Update()
    {
        ChangeCamera();
    }

    public void DeathScreen()
    {
        Camera.transform.parent = null;
    }
}
