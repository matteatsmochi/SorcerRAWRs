using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class enemyDestinationManager : MonoBehaviour
{    
    enemyBrain ebrain;
    NavMeshAgent agent;
    
    //used to face target when in battle
    GameObject faceObject;
    bool isAttacking = false;
    
    //used when on patrol
    public bool isOnPatrol = false;

    //used when knock back
    public bool isKnockBack = false;
    Vector3 kbDir;
    float kbWait;
    float kbMagnitude;

    
    void Awake()
    {
        //assign components
        ebrain = GetComponent<enemyBrain>();
        agent = GetComponent<NavMeshAgent>();
    }
    
    void Start()
    {
        
        
        //agent.Warp(new Vector3(-20,0,-20)); //warp test
    }

    

    
    void Update()
    {
        
        if (agent.remainingDistance <= 1f) //we've reached our destination
        {
            //if we are at waypoint[0] clear from list and get new one
            

            if (isOnPatrol)
            {
                //when reached patrol point, set next patrol point.
                ebrain.ChangePatrolPoint();
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
                ebrain.ChangeState(1);
            }
        }

    }

    public void NewDestination(Vector3 dest)
    {
        agent.SetDestination(dest);
        agent.isStopped = false;
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
        kbDir = dir;
        kbWait = 0;
        kbMagnitude = kb / 10;
        isKnockBack = true;
        ebrain.ChangeState(6);
    }

    
            
    

}
