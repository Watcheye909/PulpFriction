using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public GameObject Player;
    public PlayerMovement PM;
    public Rigidbody rb;
    public float strength;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PM = Player.GetComponent<PlayerMovement>();
        rb = Player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 3)
        {
            //RESIZE THE PLAYER TO NEUTRAL
            //RESET THE VERTICAL VELOCITY TO 0
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            
            //APPLY THE JUMP FORCE
            rb.drag = 0;

            rb.AddForce(transform.up * strength, ForceMode.Impulse);
            //Player.GetComponent<PlayerMovement>().Jump();
            
            Debug.Log("Bouncepad Triggered");
        }

    }

}
