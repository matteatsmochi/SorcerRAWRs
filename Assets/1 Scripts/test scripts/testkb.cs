using UnityEngine;
using UnityEngine.AI;
 
public class testkb : MonoBehaviour 
{
    public NavMeshAgent agent; //object to move
    public GameObject from; //origin point
    public GameObject to; //point to move towards
    public float kbmagnitude; //how long to move in that direction
 
    Vector3 kbto;
    bool moving;
 
    Vector3 kbDir;
 
    void Update() {
        if(Input.GetKeyDown("space")) {
            SetKB();
        }
 
        if(kbmagnitude > 0) {
 
            agent.Move(kbDir * Time.deltaTime * 10); //this wants an agent relative offset vector for movement. It wants to know which direction and how far to move, you're passing in an absolute position you want it to arrive at.
            kbmagnitude -= Time.deltaTime;
        }
    }
 
    void SetKB() {
        //get the direction of the knockback
        kbDir = (transform.position - from.transform.position).normalized;
        //add a bit of up offset so they move up
 
        //. .... = myPos + (direction between two points) gives you a world space position.  Passing this into agent.Move causes some weird results because it's not expecting a world space position.
        //kbto = transform.position + (transform.position - from.transform.position).normalized;
        //kbto.y = 0.75f;
 
        kbmagnitude = 0.5f;
    }
}