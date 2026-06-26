using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;
    public Transform orientation;
    public GameObject level;
    public CameraSettings cam;



    public GameObject speedlines;
    //public ParticleSystem Speedlines;
    public ParticleSystem chargeClouds;

    
    [Header("Movement Settings")]
    public bool UserControl;
    public bool RotationOn;

    public float horizontalInput;
    public float verticalInput;
    public float playerDrag;
    UnityEngine.Vector3 moveDirection;

    public float groundMulti;
    public float airMulti;
    public float slowDownMulti;

    [Header("Camera Settings")]
    public int speedlineThreshold;

    public float shakeAmt;
    public float shakeLength;

    public double camTilt;

    [Header("Level Settings")]
    public float tiltSpeed;
    public float maxTiltAngle;


    [Header("KeyBinds")]
    public KeyCode chargeKey;
    public KeyCode launchKey;
    
    [Header("Ball Values")]
    public int chargeAmount;
    public float RPMSpeed;

    
    public float extraSpeed;
    public float turnSpeed;


    [Header("Checks")]
    public bool launched;
    public bool charging;
    public bool canRev;
    public bool canLaunch;
    public bool grounded;

    public float playerHeight;
    public LayerMask whatIsGround;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GameObject.Find("Main Camera").GetComponent<CameraSettings>();
        level = GameObject.Find("LEVEL");
        
        
        launched = false;
        charging = false;
        
        canRev = true;
        canLaunch = true;

        chargeClouds.Stop();
        speedlines.SetActive(false);
    }

    
    
    void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    void Launch()
    { 
        // calculate movement direction to orientation
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(canLaunch)
            rb.AddForce(orientation.forward * RPMSpeed, ForceMode.Impulse);

        //if(!canLaunch)
        rb.AddForce(orientation.forward * RPMSpeed, ForceMode.Force);

        /* OLD VERSION
        // on ground
        if (grounded)
        {
            rb.AddForce(orientation.forward * RPMSpeed, ForceMode.Force);
        }
        */
            

        canLaunch = false;
    }

    void Steering()
    {

        if(RotationOn)
        {
            /*
            float targetY = horizontalInput * maxTiltAngle;
            Quaternion targetRotation = Quaternion.Euler(0, targetY, 0);
            orientation.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
            */

            //turnSpeed = Mathf.Clamp(turnSpeed, -60, 60);
            if(orientation.rotation.y <= 60 && orientation.rotation.y >= -60)
            {    
                Vector3 rotationSpeed = new Vector3(0 ,horizontalInput * turnSpeed, 0);
                orientation.Rotate(rotationSpeed * Time.deltaTime);
            }
        }
        /*
        if(UserControl)
                rb.AddForce(moveDirection.normalized * groundMulti, ForceMode.Force);
        
        // in the air
        else if (!grounded && UserControl)
            rb.AddForce(moveDirection.normalized * airMulti, ForceMode.Force);
        */
    }

    
    
    
    // Update is called once per frame
    void Update()
    {
        //Ground Check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        if(grounded)
            rb.drag = playerDrag;
            else
                rb.drag = 0;
        
       //Charge up
        if(Input.GetKeyDown(chargeKey) && canRev)
        {
            
            chargeClouds.Play();
            
            chargeAmount++;
            charging = true;
        }

        //Launch the ball
        if(Input.GetKeyDown(launchKey) && canLaunch)
        {
            RPMSpeed += chargeAmount/2;
            RPMSpeed += extraSpeed;
            launched = true;
            //rb.AddForce(transform.forward * RPMSpeed, ForceMode.Impulse);
        }

        






        SpeedEffects();
        MyInput();

        if(launched)
        {
            Steering();
        }
        
        
        if(RPMSpeed > 0)
        {
            if(grounded)
                RPMSpeed -= Time.deltaTime*slowDownMulti;
            
            if(!grounded)
                RPMSpeed -= Time.deltaTime/2;
        }


    }
    void FixedUpdate()
    {
        if(launched)
        {
            Launch();
            Invoke("SpeedCheck", 3f);
        }

    }

    void SpeedCheck()
    {
        if(rb.velocity.z <= RPMSpeed/2)
        {
            //RPMSpeed =  rb.velocity.z; (OLD VERSION)
            RPMSpeed /= 1.25f;
        }
    }

    void SpeedEffects()
    {
        //ParticleSystem CHECK
        if(!canRev)
        {
            chargeClouds.Stop();
        }



        
        if(rb.velocity.magnitude > speedlineThreshold)
        {
            speedlines.SetActive(true);
        }
        else
            speedlines.SetActive(false);

        
    }

    void OnCollisionEnter(Collision collision)
    {
        
    }

}
