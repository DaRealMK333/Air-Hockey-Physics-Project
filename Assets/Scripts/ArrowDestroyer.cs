using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDestroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public static void ArrowDestroy()
    {
        Destroy(GameObject.Find("arrow(Clone)"));
    }
}
