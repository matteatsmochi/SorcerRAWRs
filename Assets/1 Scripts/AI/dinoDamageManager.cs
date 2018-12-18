using System;
using UnityEngine;
using System.Collections.Generic;

public class dinoDamageManager : MonoBehaviour
{
    SpellManager sm;
    UIManager uim;
    dinoBrain db;
    dinoStats ds;

    
    bool countdown;
    float castReady;
    float currentCastTime;
    float prct;
    List<int> SpellQueue = new List<int>(); //spell queue
    

    void Awake()
    {
        //assign components
        GameObject smgo = GameObject.Find("Spell Manager");
        sm = smgo.GetComponent<SpellManager>();
        GameObject uimgo = GameObject.Find("UI Manager");
        uim = uimgo.GetComponent<UIManager>();
        db = GetComponent<dinoBrain>();
        ds = GetComponent<dinoStats>();
    }
    
    void Start()
    {
        AddSpellToQueue(0); //add generic spell to queue at start
        

    }

    void Update()
    {
        
        
        if (countdown)
        {

            //countdown to next spell ready.
            prct = (castReady/currentCastTime);
            if (prct < 1)
            {
                castReady += Time.deltaTime;
            } else {
                //spell is ready to cast
                ds._atkrdy = true;
                countdown = false;
            }
            
            //send percent to UIM
            
        }
    }

    public void TakeDamage(float damage)
    {
        //popup text ui
        GameObject putParent = Instantiate(uim.popupTextPrefab, transform.position, Quaternion.identity);
        popupText put = putParent.GetComponentInChildren<popupText>();
        put.SetText(true, Mathf.RoundToInt(damage), gameObject);

        
        ds._currentStamnia -= damage;
        if (ds._currentStamnia <= 0)
        {
            //KO
            Destroy(gameObject, 2f);
        }

        //update healthbar
            

    }

    public void HealDamage(float damage)
    {
        //popup text ui
        GameObject putParent = Instantiate(uim.popupTextPrefab, transform.position, Quaternion.identity);
        popupText put = putParent.GetComponentInChildren<popupText>();
        put.SetText(false, Mathf.RoundToInt(damage), gameObject);
            
        Mathf.Clamp(ds._currentStamnia += damage, 0, ds._stamnia);

        //update healthbar
            
    }
    
    public void dinoCastSpell(GameObject spellTarget)
    {
        if (ds._atkrdy)
        {
            Vector3 loc = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
            RFX1_Target target;
            RFX1_EffectSettingProjectile projectile;
            RFX1_EffectSettingColor clr;
            SpellcastInfo si;

            //debug: make this work for projectile spells, do other spells later
            //check if spell is available

            //cast spell, attach damage/heal info
            GameObject _spell = Instantiate(sm.spellLibrary[SpellQueue[0]].spellPrefab, loc, Quaternion.identity);
            target = _spell.GetComponent<RFX1_Target>();
            projectile = _spell.GetComponent<RFX1_EffectSettingProjectile>();
            clr = _spell.GetComponent<RFX1_EffectSettingColor>();
            si = _spell.GetComponent<SpellcastInfo>();

            target.Target = spellTarget;
            
            projectile.CollidesWith = 1<<10 | 1<<12;
            
            
            si.damage = sm.spellLibrary[SpellQueue[0]].baseDamage + ds._might;
            si.knockback = sm.spellLibrary[SpellQueue[0]].knockBack; 
            si.target = "Enemy";
            si.duration = sm.spellLibrary[SpellQueue[0]].duration;
            
            si.element = (SpellcastInfo.elements)(int)sm.spellLibrary[SpellQueue[0]].element;

            Destroy(_spell, si.duration);
            RemoveSpellAtIndex(0);
            LoadSpellFromQueue();
            
        }
    }

    public void AddSpellToQueue(int index)
    {
        if (SpellQueue.Count < 4)
        {
            switch (index)
            {
                case 0:
                    SpellQueue.Add(ds._atk0);
                break;

                case 1:
                    SpellQueue.Add(ds._atk1);
                break;

                case 2:
                    SpellQueue.Add(ds._atk2);
                break;

                case 3:
                    SpellQueue.Add(ds._atk3);
                break;
            }
        }

        if (SpellQueue.Count == 1) {LoadSpellFromQueue();}

    }

    public void RemoveSpellAtIndex(int index)
    {
        SpellQueue.RemoveAt(index);
    }

    public void ClearSpellQueue()
    {
        SpellQueue.Clear();
        ds._atkrdy = false;
        countdown = false;
        castReady = 0;
    }

    void LoadSpellFromQueue()
    {
        ds._atkrdy = false;
        
        if (SpellQueue.Count == 0) {AddSpellToQueue(0);}

        castReady = 0;
        currentCastTime = sm.spellLibrary[SpellQueue[0]].castTime;
        db.PassAttackRange(sm.spellLibrary[SpellQueue[0]].range);
        countdown = true;
    }

    public List<int> GetSpellQueue()
    {
        return SpellQueue;
    }

    public float GetPercentReady()
    {
        return prct * 100;
    }

    

    
}
