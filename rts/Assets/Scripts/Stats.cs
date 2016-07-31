using UnityEngine;
using System.Collections;

public  class Stats : MonoBehaviour {

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

    protected Stats(bool clear = false)
    {

    }
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
    public static Stats operator + (Stats s1, Stats s2)
    {
        return new Stats(s1.health + s2.health, s1.mana + s2.mana, s1.physicalResistance + s2.physicalResistance,
                        s1.magicalResistance + s2.magicalResistance, s1.physicalDamage + s2.physicalDamage,
                        s1.magicalDamage + s2.magicalDamage, s1.criticalDamage + s2.criticalDamage,
                        s1.strength + s2.strength, s1.intellect + s2.intellect, s1.agility + s2.agility, 
                        s1.movement + s2.movement);
    }
    public static Stats GetSum(Stats[] stats)
    {
        Stats acum = new Stats(false);
        foreach(Stats s in stats)
        {
            acum += s;
        }
        return acum;
     }

}

