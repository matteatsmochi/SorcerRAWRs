using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dinoBrain : MonoBehaviour
{
    //Managers
    dinoDestinationManager dm;
    dinoDamageManager dmgm;
    dinoSensorManager sensm;
    dinoStats ds;
    
    //States
    int state; //public for debug on screen text
    //null = 0
    //isOnRoute = 1
    //isPursuing = 2
    //isAttacking = 3
    //isRetreating = 4
    bool insideloop;

    void Awake()
    {
        //assign components
        dm = GetComponent<dinoDestinationManager>();
        dmgm = GetComponent<dinoDamageManager>();
        sensm = GetComponent<dinoSensorManager>();
        ds = GetComponent<dinoStats>();
    }
    
    void Start()
    {
        //set stats to variables
        sensm.SetSightRange(ds._perception);
        dm.SetSpeed(ds._agility);
        
        ChangeState(1);
        
        
    }

    
    void Update()
    {
        switch (state)
        {
            //isOnRoute
            case 1:
                if (!insideloop)
                {
                    insideloop = true;
                    dm.NewDestinationWaypoint();
                }
                if (sensm.currentTarget != null)
                {
                    ChangeState(2);
                }
            break;

            //isPursuing
            case 2:
                if (sensm.currentTarget != null)
                {
                    dm.NewDestination(sensm.currentTarget.transform.position);
                    if (sensm.inAttackRange)
                    {
                        ChangeState(3);
                        dm.ClearDestination();
                    }
                } else 
                {
                    ChangeState(1);
                }
            break;

            //isAttacking
            case 3:
                if (sensm.currentTarget != null)
                {
                    dmgm.dinoCastSpell(sensm.currentTarget);
                    dm.NewAttackTarget(sensm.currentTarget);
                } else 
                {
                    ChangeState(1);
                }
                
            break;

            //isRetreating
            case 4: 
                if (!dm.isRetreating)
                {
                    //sets new retreat point, locks to null state until retreat is finished
                    dm.NewRetreat();
                }
            break;

            //isKnockBack
            case 5:

            break;
        }
    }

    public void ChangeState(int newstate)
    {
        state = newstate;
        insideloop = false;
    }

    public void PassAttackRange(float range)
    {
        sensm.SetAttackRange(range);
    }

}
