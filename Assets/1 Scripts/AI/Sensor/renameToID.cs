using UnityEngine;
public class renameToID : MonoBehaviour
{
    void Awake()
    {
        gameObject.name = gameObject.GetInstanceID().ToString();
    }
}
