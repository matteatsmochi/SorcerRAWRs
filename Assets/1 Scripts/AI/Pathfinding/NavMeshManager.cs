using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class NavMeshManager : MonoBehaviour
{
    NavMeshSurface surface;
    
    void Start()
    {
        surface = gameObject.GetComponent<NavMeshSurface>();
        //StartCoroutine("InitNavMesh");
    }

    public void UpdateNavMesh()
    {
        surface.BuildNavMesh();
    }

    IEnumerator InitNavMesh()
    {
        yield return new WaitForSeconds(1);
        UpdateNavMesh();
    }

}
