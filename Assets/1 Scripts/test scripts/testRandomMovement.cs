using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testRandomMovement : MonoBehaviour
{
    float cooldown = 0f;
    bool moving = false;
    
    public Transform startMarker;
    public Transform endMarker;
    
    public float speed = 1f;

    private float startTime;
    private float journeyLength;

    void Start()
    {
        startMarker = gameObject.transform;
        endMarker = gameObject.transform;
    }

    void Update()
    {
        if (cooldown >= 0 && !moving)
        {
            //stopped, on cooldown waiting to make new move
            //Debug.Log("hello2");
            cooldown -= Time.deltaTime;
            //Debug.Log(cooldown);
        } else if (cooldown >= 0 && moving) {
            //in the process of moving
            
            if (transform.position == endMarker.position)
            {
                //we are at the destination, not moving
                //Debug.Log("hello3");
                moving = false;
            } else {
                //Debug.Log("hello4");
                float distCovered = (Time.time - startTime) * speed;
                float fracJourney = distCovered / journeyLength;
                transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
            }
            
        } else {
            //cooldown is over, start new move
            //Debug.Log("hello1");
            StartMoving();
            moving = true;
            cooldown = 2f;
        }
    }

    void StartMoving()
    {
        int randX = Random.Range(-2, 2);
        //Debug.Log(randX);
        int randZ = Random.Range(-2, 2);
        //Debug.Log(randZ);

        startTime = Time.time;
        startMarker.position = gameObject.transform.position;
        endMarker.position = new Vector3(startMarker.position.x + randX, startMarker.position.y, startMarker.position.z + randZ);

        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
        
    }
}
