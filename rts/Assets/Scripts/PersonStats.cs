using UnityEngine;
using System.Collections;

public class PersonStats : Stats
{
    protected int healthMax { get; set; }
    protected int healthRegen { get; set; }

    protected int manaMax { get; set; }
    protected int manaRegen { get; set; }

    public PersonStats(int health = 100, int healthMax = 100, int healthRegen = 1, int mana = 100, int manaMax = 100,
                     int manaRegen = 1, int physicalResistance = 1, int magicalResistance = 1, int physicalDamage = 1,
                     int magicalDamage = 1, int criticalDamage = 1, int strength = 1, int intellect = 1, int agility = 1, int movement = 100)
        :base(health, mana, physicalResistance, magicalResistance, physicalDamage, magicalDamage, criticalDamage, strength, intellect, agility, movement)
    {        
        this.healthMax = healthMax;
        this.healthRegen = healthRegen;

        this.mana = mana;
        this.manaMax = manaMax;       
    }
}
