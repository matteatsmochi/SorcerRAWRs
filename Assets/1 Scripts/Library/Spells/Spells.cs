using UnityEngine;
[System.Serializable]
public class Spells
{
	public string spellName; //name of the spell
	public GameObject spellPrefab; //spell GO to instantiate
    public enum elements
    {
        Fire,
        Water,
        Ice,
        Electric,
        Earth,
        Light,
        Dark,
        Other
    }
    public elements element = elements.Other; //spell category. default to other

    public enum types
    {
        Projectile,
        Other
    }
    public types type = types.Projectile;

    public float baseDamage; //base damage or healing for the spell
    public float range; //distance between attacker and target in order to cast
    public float knockBack; //force to add on impact
    public float castTime; //time required to cast a spell
    public float duration; //time before prefab is destroyed
    public Sprite icon; //icon for UI
    
}