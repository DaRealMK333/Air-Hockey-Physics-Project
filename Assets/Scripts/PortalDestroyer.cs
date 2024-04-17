using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalDestroyer : MonoBehaviour
{
    private PortalSpawnerScript portalSpawnerscript;

    private float PortalTimer = 0; 
    // Start is called before the first frame update
    void Start()
    {
        portalSpawnerscript = FindObjectOfType<PortalSpawnerScript>();
    }

    // Update is called once per frame
    void Update()
    {
       // TimePortalDestroy();1 
    }


    public static void PortalDestroy()
    {
        Destroy(GameObject.Find("PlayerPortal(Clone)"));
        Destroy(GameObject.Find("AiPortal(Clone)"));
    }
    
}
