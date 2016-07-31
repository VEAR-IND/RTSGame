using UnityEngine;
using System.Collections;

public abstract class Stats : MonoBehaviour {

    protected int health { get; set; }

    protected int mana { get; set; }   

    protected int physicalResistance { get; set; }
    protected int magicalResistance { get; set; }

    protected int physicalDamage { get; set; }
    protected int magicalDamage { get; set; }
    protected int criticalDamage { get; set; }

    protected int strength { get; set; }
    protected int intellect { get; set; }
    protected int agility { get; set; }
    protected int movement { get; set; }

    protected Stats(int health = 100, int mana = 100, int physicalResistance = 1, int magicalResistance = 1, int physicalDamage = 1,
                     int magicalDamage = 1, int criticalDamage = 1, int strength = 1, int intellect = 1, int agility = 1, int movement = 100)
    {
        this.health = health;
        this.mana = mana;       
        this.physicalResistance = physicalResistance;
        this.magicalResistance = magicalResistance;
        this.physicalDamage = physicalDamage;
        this.magicalDamage = magicalDamage;
        this.criticalDamage = criticalDamage;
        this.strength = strength;
        this.intellect = intellect;
        this.agility = agility;
        this.movement = movement;
    }
}

