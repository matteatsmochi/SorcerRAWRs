using System;
using UnityEngine;
using System.Collections.Generic;

public class enemyDamageManager : MonoBehaviour
{
    SpellManager sm;
    enemyBrain ebrain;
    enemyStats es;
    UIManager uim;
    

    void Start()
    {
        //assign components
        GameObject smgo = GameObject.Find("Spell Manager");
        sm = smgo.GetComponent<SpellManager>();
        ebrain = GetComponent<enemyBrain>();
        GameObject uimgo = GameObject.Find("UI Manager");
        uim = uimgo.GetComponent<UIManager>();
        es = GetComponent<enemyStats>();
        
    }

    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        //popup text ui
        GameObject putParent = Instantiate(uim.popupTextPrefab, transform.position, Quaternion.identity);
        popupText put = putParent.GetComponentInChildren<popupText>();
        put.SetText(true, Mathf.RoundToInt(damage), gameObject);

        es._currentStamnia -= damage;
        if (es._currentStamnia <= 0)
        {
            //KO
            Despawn();
        }

    }

    public void HealDamage(float damage)
    {
        //popup text ui
        GameObject putParent = Instantiate(uim.popupTextPrefab, transform.position, Quaternion.identity);
        popupText put = putParent.GetComponentInChildren<popupText>();
        put.SetText(false, Mathf.RoundToInt(damage), gameObject);

        Mathf.Clamp(es._currentStamnia += damage, 0, es._stamnia);
    }
    
    public void enemyCastSpell(GameObject spellTarget, int index)
    {
    
        Vector3 loc = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
        RFX1_Target target;
        RFX1_EffectSettingProjectile projectile;
        RFX1_EffectSettingColor clr;
        SpellcastInfo si;

        //debug: make this work for projectile spells, do other spells later

        //cast spell, attach damage/heal info
        GameObject _spell = Instantiate(sm.spellLibrary[index].spellPrefab, loc, Quaternion.identity);
        target = _spell.GetComponent<RFX1_Target>();
        projectile = _spell.GetComponent<RFX1_EffectSettingProjectile>();
        clr = _spell.GetComponent<RFX1_EffectSettingColor>();
        si = _spell.GetComponent<SpellcastInfo>();

        target.Target = spellTarget;

        projectile.CollidesWith = 1<<10 | 1<<11;
        
        si.damage = sm.spellLibrary[index].baseDamage + es._might;
        si.knockback = sm.spellLibrary[index].knockBack;
        si.target = "Player";
        si.duration = sm.spellLibrary[index].duration;
        
        si.element = (SpellcastInfo.elements)(int)sm.spellLibrary[index].element;

        Destroy(_spell, si.duration);

    }

    void Despawn()
    {
        ebrain.Despawn();
    }
    
}
