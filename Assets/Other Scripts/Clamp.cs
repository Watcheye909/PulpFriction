using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clamp : MonoBehaviour
{

    public float minYrot = -90f;
    public float maxYrot = 90f;

    private Transform localTrans;
    
    
    // Start is called before the first frame update
    void Start()
    {
        localTrans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerEurlerAngles = localTrans.rotation.eulerAngles;

        playerEurlerAngles.y = (playerEurlerAngles.y > 180) ? playerEurlerAngles.y - 360 : playerEurlerAngles.y;
        playerEurlerAngles.y = Mathf.Clamp(playerEurlerAngles.y, minYrot, maxYrot);

        localTrans.rotation = Quaternion.Euler(playerEurlerAngles);
    }
}
