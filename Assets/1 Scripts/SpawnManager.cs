using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] sorcerRAWRs;

    public GameObject[] enemies;

    public void SpawnPlayer(Vector3 loc, int index)
    {
        Instantiate(sorcerRAWRs[index], loc, Quaternion.identity);
    }

    public void SpawnEnemy(Vector3 loc, int index)
    {
        Instantiate(enemies[index], loc, Quaternion.identity);
    }
}
