using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHive : MonoBehaviour
{
    public List<hiveTrigger> HiveTriggers = new List<hiveTrigger>(); //list of all hive triggers
    public List<GameObject> HiveEnemies = new List<GameObject>(); //list of all enemies in the hive
    public List<GameObject> SpawnPoints = new List<GameObject>(); //list of points to spawn prefab enemies
    public List<GameObject> HivePrefabs = new List<GameObject>(); //list of all enemies prefabs
    List<GameObject> VisiblePlayers = new List<GameObject>(); //list of all visible players
    public bool OnAlert;

    void Start()
    {
        OnAlert = false;
    }

    public GameObject ClosestPlayer(GameObject enemy)
    {
        //declare gameobject to return
        GameObject closest = null;
        
        //clear VisiblePlayers list
        VisiblePlayers.Clear();
        
        //add all visible players to VisiblePlayers
        for (int i = 0; i < HiveEnemies.Count; i++)
        {
            VisiblePlayers.AddRange(HiveEnemies[i].GetComponent<enemySensorManager>().DetectedList());
        }
        //cycle thru all visible players and compare distance
        //if distance is shorter, replace
        
        if (VisiblePlayers.Count > 0)
        {
            
            //there is 1 or more visible players to the hive
            for (int i = 0; i < VisiblePlayers.Count; i++)
            {
                if (closest == null)
                {
                    closest = VisiblePlayers[i];
                    continue;
                }
                if (DistanceBetweenObjects(enemy, VisiblePlayers[i]) < DistanceBetweenObjects(enemy, closest))
                {
                    closest = VisiblePlayers[i];
                }
            }
            return closest;
        } else
        {
            //no visible players to the hive
            OnAlert = false;
            
            return null;
        }
        
        
    }

    float DistanceBetweenObjects(GameObject start, GameObject end)
    {
        return Vector3.Distance(start.transform.position, end.transform.position);
    }

    public void RemoveGameobject(GameObject go)
    {
        HiveEnemies.Remove(go);
    }

    public void Triggered(int type, int big)
    {
        int bigEnemies = big;
        float d = 0.2f;
        float da = 0;
        switch (type)
        {
            case 0: //spawn
            for (int i = SpawnPoints.Count; i > 0; i--)
            {
                int spawnpt = Random.Range(0, SpawnPoints.Count);
                
                if (bigEnemies > 0)
                {
                    StartCoroutine(SpawnOnDelay(HivePrefabs[0], SpawnPoints[spawnpt].transform, da));
                    bigEnemies --;
                } else{
                    StartCoroutine(SpawnOnDelay(HivePrefabs[1], SpawnPoints[spawnpt].transform, da));
                }
                SpawnPoints.RemoveAt(spawnpt);
                da += d;
            }

            break;

            case 1: //trap
            

            break;
        }
    }

    IEnumerator SpawnOnDelay(GameObject spawn, Transform loc, float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject spenemy = Instantiate(spawn, loc.position, Quaternion.identity);
        spenemy.transform.SetParent(gameObject.transform);
        HiveEnemies.Add(spenemy);
    }
}
