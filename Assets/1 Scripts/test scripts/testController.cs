using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testController : MonoBehaviour
{
    
    dinoDamageManager dmgm;
    dinoBrain dbrain;
    UIManager uim;

    void Start()
    {
        dmgm = GetComponent<dinoDamageManager>();
        dbrain = GetComponent<dinoBrain>();
        GameObject uimgo = GameObject.Find("UI Manager");
        uim = uimgo.GetComponent<UIManager>();

    }

    
    void Update()
    {
        //debug key presses
        if (Input.GetKeyDown(KeyCode.A))
        {
            dmgm.AddSpellToQueue(0);
        } else if (Input.GetKeyDown(KeyCode.S))
        {
            dmgm.AddSpellToQueue(1);
        } else if (Input.GetKeyDown(KeyCode.D))
        {
            dmgm.AddSpellToQueue(2);
        } else if (Input.GetKeyDown(KeyCode.F))
        {
            dmgm.AddSpellToQueue(3);
        } else if (Input.GetKeyDown(KeyCode.W))
        {
            dmgm.ClearSpellQueue();
        } else if (Input.GetKeyDown(KeyCode.R))
        {
            dbrain.ChangeState(4);
        } 
        uim.UpdatePlayerSpellQueue();
    }
}
