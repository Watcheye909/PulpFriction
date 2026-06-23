using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;




    public PlayerMovement PM;
    
    
    //public bool canRev;
    public float revTime;
    public float currentTime;
    // Start is called before the first frame update
    void Awake()
    {
        PM = GameObject.Find("Player").GetComponent<PlayerMovement>();
        //canRev = true;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //PLAYER CHARGE SYSTEM
        if(PM.canRev && !PM.charging)
        {
            currentTime = revTime;
        }

        else if(PM.charging && currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }

        if(currentTime <= 0)
        {
            PM.canRev = false;
        }


        if(PM.launched && !PM.canLaunch && PM.RPMSpeed <= 0 && PM.rb.velocity.magnitude <= 0.01)
        {
            LevelReset();
        }
    }



    void PlayerSearch()
    {
        PM = GameObject.Find("Player").GetComponent<PlayerMovement>();
        Debug.Log("Search Complete");
    }

    void LevelReset()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }


    void OnEnable()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // This method is called every time a new scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayerSearch();
        Debug.Log($"Scene Loaded: {scene.name} (Build Index: {scene.buildIndex})");
        Debug.Log($"Load Mode: {mode}");
        
        // Example: Check if it's the first scene
        if (scene.buildIndex == 0)
        {
            Debug.Log("This is the first scene in the build order.");
        }
    }
}
