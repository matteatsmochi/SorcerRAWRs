using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SensorToolkit;

public class SensorManager : MonoBehaviour
{
    //Managers
    DestinationManager dm; //to tell the dino where to go
    WaypointManger wpm;
    dinoStats ds;
    DamageManager dmgm;

    //Sensors
    public RangeSensor SightSensor; //what can the dino see
    public RangeSensor AttackSensor; //what can the dino attack
    
    //Lists
    List<GameObject> detectedList = new List<GameObject>();
    List<int> Enemies = new List<int>();

    
    //States
    int priority = 0;
    int state = 0;
    //null = 0
    //isOnRoute = 1
    //isPursuing = 2
    //isAttacking = 3
    //isRetreating = 4

    
    GameObject currentTarget = null;
    GameObject proposedTarget = null;

    
    
    
    void Start()
    {
        //assign components
        dm = GetComponent<DestinationManager>();
        GameObject wpmgo = GameObject.Find("Waypoint Manager"); 
        wpm = wpmgo.GetComponent<WaypointManger>();
        ds = GetComponent<dinoStats>();
        dmgm = GetComponent<DamageManager>();
        
        // Set Ranges
        SightSensor.SensorRange = 10f; //debug numbers. will set variable based on character stats
        AttackSensor.SensorRange = 3f;
    }

    
    void Update()
    {
        //debug mouse click
        if (Input.GetMouseButtonDown(0))
        {
            state = 4;
        }
        
        // if (SightSensor.GetNearest() != null) //nothing is in sight.
        // {
        //     Debug.Log(ObjectSensor.GetNearest().GetInstanceID());
        // }

        
        switch (state)
        {
            case 1: //isOnRoute
                //continue to move towards current waypoint
                dm.NewDestination(wpm.questWaypoints[0]); 
            break;

            case 2: //isPursuing
                //continue to move towards target
                dm.NewDestination(currentTarget.transform.position);
            break;

            case 3: //isAttacking
                
                if (ds._atkrdy)
                {

                }
                dm.NewAttackTarget(currentTarget);
            break;

            case 4: //isRetreating
                if (!dm.GetIsRetreating())
                {
                    //sets new retreat point, locks to null state until retreat is finished
                    dm.NewRetreat();
                    state = 0;
                }
            break;


        }

        
        
        
        
    }

    public void ChangeState(int newstate)
    {
        state = newstate;
    }

    public void ChangePriority(int p)
    {
        priority = p;
        ResetTargets();
    }

    public void ResetTargets()
    {
        //do this when changing priority, after retreating, after formation
        Enemies.Clear();
        currentTarget = null;
        if (AttackSensor.GetDetected().Count > 0)
        {
            state = 3; //isAttacking
            currentTarget = AttackSensor.GetNearest();
            SortAttackTargets();

        } else if (SightSensor.GetDetected().Count > 0)
        {
            state = 2; //isPursuing
            currentTarget = SightSensor.GetNearest();
        } else
        {
            state = 1; //inOnRoute
        }

        detectedList = SightSensor.GetDetected();

        //clear enemies list
        //populate enemies list
        //check how many inside attack
        //set target to closest
        //check closest vs all others with selected priority, switch if correct
        //set proper state (2 or 3)
    }

    void AddEnemiesToList(int sensor)
    {

    }

    void SortAttackTargets()
    {
        List<GameObject> attackList = new List<GameObject>();
        attackList = AttackSensor.GetDetected();
        for (int i = 0; i < attackList.Count; i++)
        {
            enemyStats ces = currentTarget.GetComponent<enemyStats>(); //current enemy stats
            enemyStats pes = attackList[i].GetComponent<enemyStats>(); //proposed enemy stats

            if (priority == 0) //prioritize small
            {
                if (pes._stamnia < ces._stamnia) {currentTarget = attackList[i];}
            } else if (priority == 1) //prioritize big
            {
                if (pes._stamnia > ces._stamnia) {currentTarget = attackList[i];}
            }

        }
    }
    
    
    
    
    
    #region "Sensors"
    
    public void SightEnter()
    {
        detectedList = SightSensor.GetDetected();
        
        if (detectedList.Count > 0)
        {
            for (int i = 0; i < detectedList.Count; i++)
            {
                if (!Enemies.Contains(detectedList[i].GetInstanceID()))
                {
                    Debug.Log(detectedList[i].GetInstanceID() + " was added to the list of seen enemies");
                    
                    
                    Enemies.Add(detectedList[i].GetInstanceID());
                    //do we have a target?
                    //no, new target

                    //yes, add to list & ignore

                    
                }
                
            }
        }
    }

    public void SightExit()
    {
        detectedList = SightSensor.GetDetected();
        
        if (detectedList.Count > 0)
        {
            List<int> temp = new List<int>();
            for (int i = 0; i < detectedList.Count; i++ )
            {
                temp.Add(detectedList[i].GetInstanceID());
                Debug.Log(i + ": " + detectedList[i].GetInstanceID());
            }

            Debug.Log("Enemies: " + Enemies.Count);
            for (int i = Enemies.Count - 1; i >= 0 ; i-- )
            {
                Debug.Log(i);
                if (!temp.Contains(Enemies[i]))
                {
                    Debug.Log(Enemies[i] + " was removed from the list of seen enemies");
                    //code to chase or assign new waypoint
                    LostSight(Enemies[i].ToString());
                    Enemies.Remove(Enemies[i]);
                }
            }
        } else 
        {
            if (Enemies.Count > 0)
            {
                for (int i = Enemies.Count - 1; i >= 0 ; i-- )
                {
                    Debug.Log(Enemies[i] + " was removed");
                    
                    //code to chase or assign new waypoint
                    LostSight(Enemies[i].ToString());
                    Enemies.Remove(Enemies[i]);
                }
            }
        }
    }

    void LostSight(string id)
    {
        //Gameobject that left sight. compare with target
        //if target still exists and no other possible targets, pursue
        //if other possible targets, new target
        //if no other targets and does not exist, waypoint
        
        
        
        if (GameObject.Find(id) != null) //still esists, but out of sight
        {
            GameObject lost = GameObject.Find(id);
            
            
        } else //does not exist
        {

        }
    }

    
    public void AttackEnter()
    {
        //is this target?
        //if yes, Stop moving and Attack sequence
        //if no, what do i do? change targets?
    }

    public void AttackExit()
    {
       //pursue target
       //if it doesnt exist, assign new target
       //if no attack targets, sight targets pursue
       //if no sight targets, waypoint
    }

    #endregion

}
