using UnityEngine;

public class hiveTrigger : MonoBehaviour
{
    enemyHive ehive;

    public enum triggerTypes
    {
        Spawn,
        Trap
        
    }
    public triggerTypes Ttype;
    public int trips;
    int enter;

    void Awake()
    {
        ehive = GetComponentInParent<enemyHive>();
        enter = 0;
    }
    
    private void OnTriggerEnter(Collider col)
    {
        
        if (col.gameObject.CompareTag("Player"))
        {
            enter ++;
            if (enter == trips)
            {
                //action to call when trigger is activated
                ehive.Triggered((int)Ttype, 1);
            }
            
        }
        
    }
}
