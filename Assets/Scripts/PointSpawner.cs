using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PointSpawner : MonoBehaviour
{
    public GameObject PlusOnePoint;
    private float Xcord, Ycord;
    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PointsSpawner()
    {
        Xcord = Random.Range(-1.75f, 1.76f);
        Ycord = Random.Range(-3.5f, 3.51f);
        Instantiate(PlusOnePoint, new Vector3(Xcord, Ycord, -1), quaternion.identity);
    }
}
