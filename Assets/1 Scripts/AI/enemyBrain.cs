using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBrain : MonoBehaviour
{
    //Managers
    enemyHive hive;
    enemyDestinationManager dm;
    enemyDamageManager dmgm;
    enemySensorManager sensm;
    enemyStats es;
    
    //States
    int state; //public for debug on screen text
    //null = 0
    //isOnPatrol = 1
    //isPursuing = 2
    //isAttacking = 3
    //isRetreating = 4
    bool insideloop;

    //patrol
    public List<Vector3> patrolPoints;
    int currentPatrolDestination;
    bool changeDirection;

    //attacking
    public float timeBetweenSpells;
    float currentTime;
    public int bigSpellEvery;
    int currentSpell;

    void Awake()
    {
        //assign components
        dm = GetComponent<enemyDestinationManager>();
        dmgm = GetComponent<enemyDamageManager>();
        sensm = GetComponent<enemySensorManager>();
        es = GetComponent<enemyStats>();
        StartCoroutine(LateStart());
    }

    void Start()
    {
        patrolPoints.Add(new Vector3(transform.position.x + Random.Range(-10, 10), 0, transform.position.z + Random.Range(-10, 10)));
        patrolPoints.Add(new Vector3(transform.position.x + Random.Range(-10, 10), 0, transform.position.z + Random.Range(-10, 10)));
        patrolPoints.Add(new Vector3(transform.position.x + Random.Range(-10, 10), 0, transform.position.z + Random.Range(-10, 10)));
        patrolPoints.Add(new Vector3(transform.position.x + Random.Range(-10, 10), 0, transform.position.z + Random.Range(-10, 10)));

        currentTime = 0f;
        currentSpell = 0;

        //set stats to variables
        sensm.SetSightRange(es._perception);
        sensm.SetAttackRange(es._perception * 0.5f);
        dm.SetSpeed(es._agility);
        
        ChangeState(1);
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.2f);
        hive = GetComponentInParent<enemyHive>();
    }

    
    void Update()
    {
        switch (state)
        {
            //isOnPatrol
            case 1:
                if (!insideloop)
                {
                    insideloop = true;
                    ChangePatrolPoint();
                }

                if (hive && hive.OnAlert == true)
                {
                    dm.NewDestination(hive.ClosestPlayer(gameObject).transform.position);
                    ChangeState(5);
                }
                
                if (sensm.currentTarget != null)
                {
                    dm.isOnPatrol = false;
                    hive.OnAlert = true;
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
                    if (currentTime < timeBetweenSpells)
                    {
                        currentTime += Time.deltaTime;
                    } else 
                    {
                        //cast spell
                        //incriment spell counts
                        if (currentSpell == bigSpellEvery)
                        {
                            dmgm.enemyCastSpell(sensm.currentTarget, es._atk1);
                            currentSpell = 0;
                        } else{
                            dmgm.enemyCastSpell(sensm.currentTarget, es._atk0);
                            currentSpell += 1;
                        }
                        
                        //reset current time
                        currentTime = 0f;
                    }
                    
                    dm.NewAttackTarget(sensm.currentTarget);
                } else 
                {
                    ChangeState(1);
                }
                
            break;

            //isRetreating
            case 4: 
                //nothing right now
            break;
            
            //isHivePursuing
            case 5:
                if (sensm.currentTarget != null)
                {
                    ChangeState(2);
                } else
                {
                    dm.NewDestination(hive.ClosestPlayer(gameObject).transform.position);
                }
                
            break;
        }
    }

    public void ChangeState(int newstate)
    {
        state = newstate;
        insideloop = false;
    }

    public void ChangePatrolPoint()
    {
        
        if (Random.Range(0f, 1f) <= 0.1f)
        {
            changeDirection = !changeDirection;
        }

        if (changeDirection)
        {
            currentPatrolDestination = (currentPatrolDestination + 1) % patrolPoints.Count;
        } else
        {
            if (--currentPatrolDestination < 0)
            {
                currentPatrolDestination = patrolPoints.Count - 1;
            }
        }

        dm.NewDestination(patrolPoints[currentPatrolDestination]);
        dm.isOnPatrol = true;
    }

    public void Despawn()
    {
        hive.RemoveGameobject(gameObject);
        Destroy(gameObject, 0.5f);
    }


}
