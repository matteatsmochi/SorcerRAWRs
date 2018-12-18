using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testai : MonoBehaviour
{
    
    public Vector3 pos1;
    public Vector3 pos2;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            gameObject.transform.position = pos1;
        } else if (Input.GetKeyDown(KeyCode.S))
        {
            gameObject.transform.position = pos2;
        } else if (Input.GetKeyDown(KeyCode.W))
        {
            Destroy(gameObject);
        }
    }
}
