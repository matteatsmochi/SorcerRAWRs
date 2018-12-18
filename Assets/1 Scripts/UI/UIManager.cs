using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    SpellManager sm;

    public GameObject player1;
    GameObject player2;
    GameObject player3;
    GameObject player4;

    dinoDamageManager p1dm;

    
    
    public Image atk1charge;
    public List<Image> atkList;
    public Sprite none;

    public GameObject popupTextPrefab;

    void Start()
    {
        //assign components
        GameObject smgo = GameObject.Find("Spell Manager");
        sm = smgo.GetComponent<SpellManager>();

        p1dm = player1.GetComponent<dinoDamageManager>();
        

    }

    
    void Update()
    {
        
        atk1charge.rectTransform.offsetMax = new Vector2(0, 0 - p1dm.GetPercentReady()); //timer goes down
        //atk1charge.rectTransform.offsetMax = new Vector2(0, -100 + p1dm.GetPercentReady()); //timer goes up
        
    }

    public void UpdatePlayerSpellQueue()
    {
        
        for (int i = 0; i < 4; i++)
        {
            atkList[i].sprite = none;
        }

        if (p1dm.GetSpellQueue().Count > 0)
        {
            for (int i = 0; i < p1dm.GetSpellQueue().Count; i++)
            {
                atkList[i].sprite = sm.spellLibrary[p1dm.GetSpellQueue()[i]].icon;
            }
        }
        

        
    }

    
}
