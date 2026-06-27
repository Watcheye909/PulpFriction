using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public bool isOpen;

    [Header("References")]
    public GameObject upgradePanel;
    public GameMaster gm;
    public CameraSettings cam;

    public KeyCode menuKey;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        cam = GameObject.Find("Main Camera").GetComponent<CameraSettings>();
        isOpen = false;
        upgradePanel.SetActive(isOpen);
    }

    // Update is called once a couple frame
    void FixedUpdate()
    {
        //upgradePanel.SetActive(isOpen);
    }

    void Update()
    {
        upgradePanel.SetActive(isOpen);
        if(Input.GetKeyDown(menuKey) && !isOpen)
        {
            isOpen = true;
            MenuOpen();
        }

        if(Input.GetKeyUp(menuKey) && isOpen)
        {
            isOpen = false;
            MenuClose();
        }
    }

    public void MenuOpen()
    {
        //isOpen = true;
        //upgradePanel.SetActive(isOpen);
        if (isOpen)
        {
            Time.timeScale = 0f; // Pause the game
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
            Cursor.visible = true; // Show the cursor
            //cam.enabled = false; // Disable the camera movement
        }
    }

    public void MenuClose()
    {
        //isOpen = false;
        //upgradePanel.SetActive(isOpen);
        Time.timeScale = 1f; // Resume the game
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
        Cursor.visible = false; // Hide the cursor
        //cam.enabled = true; // Enable the camera movement
    } 


}
