using UnityEngine;

public class spin : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.up, -10 * Time.deltaTime);
    }
}
