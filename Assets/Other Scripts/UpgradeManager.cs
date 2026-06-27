using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public GameObject player;
    public PlayerMovement PM;
    public GameMaster GM;

    public TextMeshProUGUI descriptionText;
    
    [Header("Upgrade Cost")]
    public int launchCost;
    public int turnCost;
    public int costMulti;

    [Header("Stat Boost Values")]
    public float launchBonus;
    public float turnSpeedBonus;

    [Header("Main Stats")]
    public float extraSpeedT;
    public float turnSpeedT;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        PM = GameObject.Find("Player").GetComponent<PlayerMovement>();
        GM = GetComponent<GameMaster>();

        extraSpeedT = PM.extraSpeed;
        turnSpeedT = PM.turnSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpgradeSearch()
    {
        player = GameObject.Find("Player");
        PM = GameObject.Find("Player").GetComponent<PlayerMovement>();
        GM = GetComponent<GameMaster>();


        Debug.Log("Search Complete");
    }

    public void LaunchUpgrade()
    {
        if(GM.points >= launchCost)
        {
            extraSpeedT += launchBonus;
            GM.points -= launchCost;
            launchCost *= costMulti;
        }
        else
            Debug.Log("You A Brokey");
    }

    public void LaunchText()
    {
        descriptionText.SetText("Cost: " + launchCost + "\n Increases the initial boost you get after charging your Dash\n" + "Boost Stat: " + extraSpeedT);
    }

    public void TurningText()
    {
        descriptionText.SetText("Cost: " + turnCost + "\n Improves how quickly the ball turns, helping with overall handling\n" + "Turn Stat: " + turnSpeedT);
    }

    public void ClearText()
    {
        descriptionText.SetText("Select An Upgrade!");
    }

    public void TurningUpgrade()
    {
        if(GM.points >= turnCost)
        {
            turnSpeedT += turnSpeedBonus;
            GM.points -= turnCost;
            turnCost *= costMulti;
        }
        else
            Debug.Log("You A Brokey");
    }
}
