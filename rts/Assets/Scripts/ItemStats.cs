using UnityEngine;
using System.Collections;

[System.Serializable]
public class ItemStats : Stats
{
    
    public ItemStats():base()
    {

    }
    public ItemStats(bool isDefault, int health = 0, int mana = 0, int physicalResistance = 0, int magicalResistance = 0, int physicalDamage = 0,
                     int magicalDamage = 0, int criticalDamage = 0, int strength = 0, int intellect = 0, int agility = 0, int movement = 0) 
        : base(isDefault, health, mana, physicalResistance, magicalResistance, physicalDamage, magicalDamage, criticalDamage, strength,
            intellect, agility, movement)
    {

    }
}
