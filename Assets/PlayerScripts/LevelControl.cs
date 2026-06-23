using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    public PlayerMovement PM;
    // Start is called before the first frame update
    void Start()
    {
        PM = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!PM.UserControl)
        {
            float targetX = -PM.verticalInput * PM.maxTiltAngle; // Invert as necessary
            float targetZ = PM.horizontalInput * PM.maxTiltAngle;

            // Smoothly interpolate towards the target rotation
            Quaternion targetRotation = Quaternion.Euler(targetX, 0, targetZ);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * PM.tiltSpeed);
        }
    }
}
