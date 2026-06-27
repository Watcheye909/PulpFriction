using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;



    public int points;
    public int newPoints;

    [Header("Scene Related")]
    public bool getPoints;
    public UpgradeManager UM;


    [Header("Distance Tracker")]
    // Stores the original starting position
    private Vector3 startPosition;
    // Stores the live calculated distance
    public float distanceFromStart;
    public int roundedDistance;
    public TextMeshProUGUI distanceText;


    [Header("Player Related")]
    public GameObject player;
    public PlayerMovement PM;
    public TextMeshProUGUI pointText;
    
    
    [Header("Charge Time")]
    //public bool canRev;
    public float revTime;
    public float currentTime;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player");
        PM = player.GetComponent<PlayerMovement>();
        UM = GetComponent<UpgradeManager>();
        //canRev = true;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
            Destroy(gameObject);


        getPoints = false;
    }

    void Start()
    {
        // Record the starting point when the game begins
        startPosition = player.transform.position;
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





        //----------STAT STUFFFF----------
        
        PM.extraSpeed = UM.extraSpeedT;
        PM.turnSpeed = UM.turnSpeedT;










        roundedDistance = Mathf.RoundToInt(distanceFromStart);
        newPoints = roundedDistance;





        // Calculate the straight-line distance every frame
        distanceFromStart = Vector3.Distance(startPosition, player.transform.position);
        
        if(distanceText != null)
        distanceText.SetText(roundedDistance + "");

        if(pointText != null)
        pointText.SetText("Points: " + points);



        if(PM.launched && !PM.canLaunch && PM.RPMSpeed <= 0 && PM.rb.velocity.magnitude <= 0.01)
        {
            LevelReset();
        }

        /*
        if(getPoints)
            convertToPoints();
        */
    }








    public void LaunchUpgrade()
    {
        PM.extraSpeed += 5;
    }
    public void TurningUpgrade()
    {
        
    }










    void PlayerSearch()
    {
        player = GameObject.Find("Player");
        PM = GameObject.Find("Player").GetComponent<PlayerMovement>();

        if(getPoints)
            convertToPoints();

        Debug.Log("Search Complete");
    }

    void LevelReset()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        getPoints = true;
    }


    void convertToPoints()
    {
        points += newPoints;
        getPoints = false;
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
        UM.UpgradeSearch();
        Debug.Log($"Scene Loaded: {scene.name} (Build Index: {scene.buildIndex})");
        Debug.Log($"Load Mode: {mode}");
        
        // Example: Check if it's the first scene
        if (scene.buildIndex == 0)
        {
            Debug.Log("This is the first scene in the build order.");
        }
    }



}
