using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testflip : MonoBehaviour
{
    
    private List<string> flips = new List<string>();
    
    void Start()
    {
        TestFlip();
    }

    void TestFlip()
    {
        do
        {
            if (!FlipCoin())
            {
                flips.Clear();
            }

        } while (flips.Count < 100);

        for (int i = 0; i < flips.Count - 1; i++)
        {
            Debug.Log(i + flips[i]);
        }
    }

    bool FlipCoin()
    {
        int coin = Random.Range(0,2);
        flips.Add(coin.ToString());
        if (coin == 0)
        {
            return false;
        } else{
            return true;
        }
    }

}
