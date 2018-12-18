using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManger : MonoBehaviour
{
      
    public List<Vector3> questWaypoints = new List<Vector3>();

    public GameObject dest; //debug for test

    
    void Start()
    {
        // for (int i = questWaypoints.Count; i > 0; i--)
        // {
        //     completedQuest.Add(false);
        // }

        AddWaypoint(dest.transform.position);
        
    }

    public void AddWaypoint(Vector3 loc)
    {
        //adds to top of the list
        //for adding lists, add bottom to top
        questWaypoints.Insert(0, loc);
    }

    
}
