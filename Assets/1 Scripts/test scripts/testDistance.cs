using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testDistance : MonoBehaviour
{
    
    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject four;
    
    void Start()
    {
        Debug.Log(DistanceBetweenObjects(one, three));
        Debug.Log(DistanceBetweenObjects(one, four));
        Debug.Log(DistanceBetweenObjects(two, three));
        Debug.Log(DistanceBetweenObjects(two, four));
    }


    float DistanceBetweenObjects(GameObject start, GameObject end)
    {
        return Vector3.Distance(start.transform.position, end.transform.position);
    }
}
