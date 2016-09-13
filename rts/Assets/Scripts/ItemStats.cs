using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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


    public override string GetStats(string strConcat = "")
    {
        string temp = "";

        if (health != 0)
        {
            if(health > 0)
            {
                temp += string.Format("\n{0}: <color=#62ed05>+{1}</color>","HP",health);
            }
            else
            {
                temp += string.Format("\n{0}: <color=#ff3300>{1}</color>", "HP", health);
            }
        }
        if (mana != 0)
        {
            if (mana > 0)
            {
                temp += string.Format("\n{0}: <color=#62ed05>+{1}</color>", "Mana", mana);
            }
            else
            {
                temp += string.Format("\n{0}: <color=#ff3300>{1}</color>", "Mana", mana);
            }
        }
        if (physicalResistance != 0)
        {
            if (physicalResistance > 0)
            {
                temp += string.Format("\n{0}: <color=#62ed05>+{1}</color>", "Physical Defence", physicalResistance);
            }
            else
            {
                temp += string.Format("\n{0}: <color=#ff3300>{1}</color>", "Physical Defence", physicalResistance);
            }
        }
        if (magicalResistance != 0)
        {
            if (magicalResistance > 0)
            {
                temp += string.Format("\n{0}: <color=#62ed05>+{1}</color>", "Magical Defence", magicalResistance);
            }
            else
            {
                temp += string.Format("\n{0}: <color=#ff3300>{1}</color>", "Magical Defence", magicalResistance);
            }
        }
        if (physicalDamage != 0)
        {
            if (physicalDamage > 0)
            {
                temp += string.Format("\n{0}: <color=#62ed05>+{1}</color>", "Physical Damage", physicalDamage);
            }
            else
            {
                temp += string.Format("\n{0}: <color=#ff3300>{1}</color>", "Physical Damage", physicalDamage);
            }
        }
        if (magicalDamage != 0)
        {
            if (magicalDamage > 0)
            {
                temp += string.Format("\n{0}: <color=#62ed05>+{1}</color>", "Magical Damage", magicalDamage);
            }
            else
            {
                temp += string.Format("\n{0}: <color=#ff3300>{1}</color>", "Magical Damage", magicalDamage);
            }
        }
        if (criticalDamage != 0)
        {
            if (criticalDamage > 0)
            {
                temp += string.Format("\n{0}: <color=#62ed05>+{1}</color>", "Critical Damage", criticalDamage);
            }
            else
            {
                temp += string.Format("\n{0}: <color=#ff3300>{1}</color>", "Critical Damage", criticalDamage);
            }
        }
        if (strength != 0)
        {
            if (strength > 0)
            {
                temp += string.Format("\n{0}: <color=#62ed05>+{1}</color>", "S", strength);
            }
            else
            {
                temp += string.Format("\n{0}: <color=#ff3300>{1}</color>", "S", strength);
            }
        }
        if (intellect != 0)
        {
            if (intellect > 0)
            {
                temp += string.Format("\n{0}: <color=#62ed05>+{1}</color>", "I", intellect);
            }
            else
            {
                temp += string.Format("\n{0}: <color=#ff3300>{1}</color>", "I", intellect);
            }
        }
        if (agility != 0)
        {
            if (agility > 0)
            {
                temp += string.Format("\n{0}: <color=#62ed05>+{1}</color>", "A", agility);
            }
            else
            {
                temp += string.Format("\n{0}: <color=#ff3300>{1}</color>", "A", agility);
            }
        }
        if (movement != 0)
        {
            if (movement > 0)
            {
                temp += string.Format("\n{0}: <color=#62ed05>+{1}</color>", "M", movement);
            }
            else
            {
                temp += string.Format("\n{0}: <color=#ff3300>{1}</color>", "M", movement);
            }
        }
        return temp + strConcat;
    }
    
    public static ItemStats operator +(ItemStats s1, ItemStats s2)
    {
        return new ItemStats(true, s1.health + s2.health, s1.mana + s2.mana, s1.physicalResistance + s2.physicalResistance,
                        s1.magicalResistance + s2.magicalResistance, s1.physicalDamage + s2.physicalDamage,
                        s1.magicalDamage + s2.magicalDamage, s1.criticalDamage + s2.criticalDamage,
                        s1.strength + s2.strength, s1.intellect + s2.intellect, s1.agility + s2.agility,
                        s1.movement + s2.movement);
    }
    public static ItemStats GetSum(List<ItemStats> stats)
    {
        if (stats != null)
        {
            ItemStats acum = new ItemStats(false);
            foreach (ItemStats s in stats)
            {
                acum += s;
            }
            return acum;
        }
        else
        {
            return null;
        }
    }
}
