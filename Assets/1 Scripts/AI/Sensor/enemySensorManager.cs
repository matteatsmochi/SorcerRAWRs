using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SensorToolkit;

public class enemySensorManager : MonoBehaviour
{

    //Sensors
    RangeSensor SightSensor; //what can the dino see
    float AttackRange; //what can the dino attack

    public GameObject currentTarget;
    public bool inAttackRange;

    void Awake()
    {
        //assign components
        SightSensor = GetComponent<RangeSensor>();
    }
    
    void Start()
    {

    }

    void Update()
    {
        if (currentTarget != null)
        {
            if (DistanceBetweenObjects(gameObject, currentTarget) < AttackRange) {inAttackRange = true;} else {inAttackRange = false;}
        }
        
    }

    
    public void SightEnter()
    {
        if (currentTarget == null)
        {
            ResetTargets();
        }
    }

    public void SightExit()
    {
        if (!SightSensor.GetDetected().Contains(currentTarget))
        {
            ResetTargets();
        }  
    }

    public void ResetTargets()
    {
        //do this when changing priority, after retreating, after formation
        currentTarget = null;
        if (SightSensor.GetDetected().Count > 0)
        {
            currentTarget = SightSensor.GetNearest();
        }

    }

    float DistanceBetweenObjects(GameObject start, GameObject end)
    {
        return Vector3.Distance(start.transform.position, end.transform.position);
    }

    public void SetAttackRange(float range)
    {
        AttackRange = range;
    }

    public void SetSightRange(float range)
    {
        SightSensor.SensorRange = range;
    }

    public List<GameObject> DetectedList()
    {
        return SightSensor.GetDetected();
    }

    


    // void SortAttackTargets()
    // {
    //     //run after target enters attack sensor, after attack target dies
    //     for (int i = 0; i < SightSensor.GetDetected().Count; i++)
    //     {
    //         enemyStats ces = target.GetComponent<enemyStats>(); //current enemy stats
    //         enemyStats pes = SightSensor.GetDetected()[i].GetComponent<enemyStats>(); //proposed enemy stats

    //         if (DistanceBetweenObjects(gameObject, SightSensor.GetDetected()[i]) < AttackRange)
    //         {
    //             if (priority == 0) //prioritize small
    //             {
    //                 if (pes._stamnia < ces._stamnia) {target = SightSensor.GetDetected()[i];}
    //             } else if (priority == 1) //prioritize big
    //             {
    //                 if (pes._stamnia > ces._stamnia) {target = SightSensor.GetDetected()[i];}
    //             }
    //         }
    //     }
    // }
    
    
    
    
    

    
    
    

 

}
