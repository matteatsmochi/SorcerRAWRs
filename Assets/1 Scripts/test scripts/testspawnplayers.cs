using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testspawnplayers : MonoBehaviour
{
    
    WaypointManger wpm;
    SpawnManager spm;
    
    
    void Start()
    {
        wpm = GetComponent<WaypointManger>();
        spm = GetComponent<SpawnManager>();
        StartCoroutine("SpawnPlayers");
    }

    void Update()
    {
        
    }

    IEnumerator SpawnPlayers()
    {
        yield return new WaitForSeconds(5);
        Instantiate(spm.sorcerRAWRs[0], new Vector3(wpm.questWaypoints[0].x, wpm.questWaypoints[0].y + 10, wpm.questWaypoints[0].z + 10), Quaternion.identity);
        
    }
}
