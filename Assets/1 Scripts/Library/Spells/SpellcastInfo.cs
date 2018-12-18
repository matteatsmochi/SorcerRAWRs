using UnityEngine;

public class SpellcastInfo : MonoBehaviour
{
    public float damage = 0;
    public enum elements
    {
        Fire,
        Water,
        Ice,
        Electric,
        Earth,
        Light,
        Dark,
        None
    }
    public elements element = elements.None;
    public float knockback;
    public string target;
    public float duration;
    
}
