using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointDestroy : MonoBehaviour
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
    public static void PointDestroyer()
    {
        Destroy(GameObject.Find("Plus_One(Clone)"));
    }
}
