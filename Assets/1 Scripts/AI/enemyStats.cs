using UnityEngine;

public class enemyStats : MonoBehaviour
{
    
    public string _name;

    public int _atk0; //standard attack
    public int _atk1; //special attack
    
    // info from https://en.wikipedia.org/wiki/Attribute_(role-playing_games)
    public float _might; //attack
    public float _agility; //speed, dodge
    public float _stamnia; //hp
    public float _currentStamnia;
    public float _resilience; //defense
    public float _perception; //sight range, detect traps, hidden objects, etc

}
