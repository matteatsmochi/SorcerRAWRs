﻿using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class dinoDestinationManager : MonoBehaviour
{    
    dinoBrain dbrain;
    NavMeshAgent agent;
    WaypointManger wpm;
    
    
    //used to face target when in battle
    GameObject faceObject;
    bool isAttacking = false;
    
    //used when retreating
    public bool isRetreating = false;

    //used when knock back
    public bool isKnockBack = false;
    Vector3 kbDir;
    float kbWait;
    float kbMagnitude;

    void Start()
    {
        //assign components
        dbrain = GetComponent<dinoBrain>();
        agent = GetComponent<NavMeshAgent>();
        GameObject wpmgo = GameObject.Find("Waypoint Manager"); 
        wpm = wpmgo.GetComponent<WaypointManger>();
        
        //agent.Warp(new Vector3(-20,0,-20)); //warp test
    }

    

    
    void Update()
    {
        
        if (agent.remainingDistance <= 1f) //we've reached our destination
        {
            //if we are at waypoint[0] clear from list and get new one
            

            if (isRetreating)
            {
                isRetreating = false;
                dbrain.ChangeState(1);
                //when reached retreat, heads for waypoint, reset targets.
            }
        }

        if (isAttacking)
        {
            FaceDestination();
        }

        if (isKnockBack)
        {
            if(kbMagnitude > 0 & kbWait < 0.2f)
            {
                kbWait += Time.deltaTime;
            }
            else if (kbMagnitude > 0 & kbWait >= 0.2f)
            {
                agent.Move(kbDir * Time.deltaTime * 10);
                kbMagnitude -= Time.deltaTime;
            }
            else
            {
                isKnockBack = false;
                dbrain.ChangeState(1);
            }
        }

    }

    public void NewDestination(Vector3 dest)
    {
        agent.SetDestination(dest);
        agent.isStopped = false;
    }

    public void NewDestinationWaypoint()
    {
        NewDestination(wpm.questWaypoints[0]);
    }

    public void ClearDestination()
    {
        agent.isStopped = true;
    }

    
    public void NewAttackTarget(GameObject go)
    {
        //deliver GO for the attack target so that we can face it
        faceObject = go;
        isAttacking = true;
    }

    public void ClearAttackTarget()
    {
        //not attacking, nav mesh controls face direction
        faceObject = null;
        isAttacking = false;
    }

    public void NewRetreat()
    {
        Vector3 retreatpoint = gameObject.transform.position - ((gameObject.transform.forward) * 10);
        //10 is a debug distance
        //may need to increase dino speed & rotation while retreating
        NewDestination(retreatpoint);
        isRetreating = true;
    }


    Vector3 GetBehindPosition(Transform target, float distanceBehind) 
    {
     return target.position - (target.forward * distanceBehind);
    }

    
    void FaceDestination()
    {
        if (faceObject != null)
        {
        Vector3 faceDirection = (faceObject.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(faceDirection.x, 0, faceDirection.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); //float is speed of rotation.
        }
    }


    public void SetSpeed(float speed)
    {
        agent.speed = speed;
    }

    public void Knockback(float kb, Vector3 dir)
    {
        agent.isStopped = true;
        kbWait = 0;
        kbDir = dir;
        kbMagnitude = kb / 10;
        isKnockBack = true;
        dbrain.ChangeState(5);
    }

    
}
