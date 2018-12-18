using UnityEngine;

public class dinoStats : MonoBehaviour
{
    
    public string _name;
    public enum dinos
    {
        Ankylosaurus,
        Brachiosarus,
        Compsognathus,
        Dilophosaurus,
        Pachycephalosaurus,
        Parasaurolophus,
        Pterodactyl,
        Spinosaurus,
        Stegosaurus,
        Triceratops,
        Tyrannosaurus,
        Velociraptor
    }
    public dinos _dinosaur;

    public int _atk0; //default attack, does not requre mana
    public int _atk1; //custom attack 1
    public int _atk2; //custom attack 2
    public int _atk3; //custom attack 3
    public bool _atkrdy; //is the attack in queue ready
    
    // info from https://en.wikipedia.org/wiki/Attribute_(role-playing_games)
    public float _might; //attack
    public float _agility; //speed, dodge
    public float _stamnia; //hp
    public float _currentStamnia;
    public float _resilience; //defense
    
    public float _intellect; //skill points on level up, xp boost
    public float _spirit; //mana regen
    public float _charm; //npc favorible outcome lines
    public float _perception; //sight range, detect traps, hidden objects, etc

    public float _luck; //impact on drops, dodges, crits, etc
    
}
