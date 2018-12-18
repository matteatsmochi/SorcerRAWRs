using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellCollision : MonoBehaviour
{
    SpellcastInfo si;

    void Awake()
    {
        si = GetComponentInParent<SpellcastInfo>();
    }
    
    void OnTriggerEnter(Collider col)
    {
        Vector3 kbdir = (col.transform.position - transform.position).normalized;
        if (col.gameObject.CompareTag("Player") && si.target == "Player")
        {
            col.gameObject.GetComponentInParent<dinoDamageManager>().TakeDamage(si.damage);
            col.gameObject.GetComponentInParent<dinoDestinationManager>().Knockback(si.knockback, kbdir);
            
        } else if (col.gameObject.CompareTag("Enemy") && si.target == "Enemy")
        {
            col.gameObject.GetComponentInParent<enemyDamageManager>().TakeDamage(si.damage);
            col.gameObject.GetComponentInParent<enemyDestinationManager>().Knockback(si.knockback, kbdir);
        }
        
        
    }
}
