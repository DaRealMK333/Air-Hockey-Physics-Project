using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawnerScript : MonoBehaviour
{
    public GameObject PlayerPortal , AiPortal;
    public bool isPortalOn=false;

    private int LeftOrRight;
    private float PortalXCord = 2.0f , PortalYCord ;
     
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PortalSpawner()
    {
        LeftOrRight = Random.Range(0, 2);
        PortalYCord = Random.Range(0, 3.5f);
        isPortalOn = false;
        if ((LeftOrRight == 0) && (isPortalOn == false))
        {
            Instantiate(PlayerPortal, new Vector3(PortalXCord, -PortalYCord, -2), Quaternion.identity); 
            Instantiate(AiPortal, new Vector3(-PortalXCord, PortalYCord, -2), Quaternion.identity);
            isPortalOn = true;
            // Debug.Log(isPortalOn);
        }
        else
        if ((LeftOrRight == 1) && (isPortalOn == false))
        {
            Instantiate(PlayerPortal, new Vector3(-PortalXCord, -PortalYCord, -2), Quaternion.identity);
            Instantiate(AiPortal, new Vector3(PortalXCord, PortalYCord, -2), Quaternion.identity);
            isPortalOn = true;
            // Debug.Log(isPortalOn);
        }
    }
}
