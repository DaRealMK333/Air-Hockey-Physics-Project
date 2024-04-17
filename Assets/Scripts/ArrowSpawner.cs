using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject Arrow;
    private float Xcord , Ycord;

    private int RotationNumber;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ArrowSpawn()
    {
        Xcord = Random.Range(-1.90f, 1.91f);
        Ycord = Random.Range(-3.80f, 3.81f);
        RotationNumber = Random.Range(0, 4) * 90;
        Instantiate(Arrow, new Vector3(Xcord, Ycord, -1), Quaternion.Euler(0f, 0f, RotationNumber));
        var angles = Arrow.transform.eulerAngles;
        angles.z = RotationNumber;
        Arrow.transform.eulerAngles = angles;

    }
}
