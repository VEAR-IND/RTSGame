using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;

[System.Serializable]

public class Stats {

    public int health;
    public int mana;
    public int physicalResistance;
    public int magicalResistance;
    public int physicalDamage;
    public int magicalDamage;
    public int criticalDamage;
    public int strength;
    public int intellect;
    public int agility;
    public int movement;

    public Stats()
    {

    }
    
    public Stats(bool isDefault, int health = 100, int mana = 100, int physicalResistance = 1, int magicalResistance = 1, int physicalDamage = 1,
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
        return new Stats(true, s1.health + s2.health, s1.mana + s2.mana, s1.physicalResistance + s2.physicalResistance,
                        s1.magicalResistance + s2.magicalResistance, s1.physicalDamage + s2.physicalDamage,
                        s1.magicalDamage + s2.magicalDamage, s1.criticalDamage + s2.criticalDamage,
                        s1.strength + s2.strength, s1.intellect + s2.intellect, s1.agility + s2.agility, 
                        s1.movement + s2.movement);
    }

    public static Stats GetSum(List<Stats> stats)
    {
        Stats acum = new Stats(false);
        foreach(Stats s in stats)
        {
            acum += s;
        }
        return acum;
     }

    public virtual void ShowStats(string strConcat = "")
    {
        Debug.LogFormat("hp:{0}, mana:{1}, pRes:{2}, mRes:{3}, pDmg:{4}, mDmg:{5}, cDmg:{6}, st:{7}, i:{8}, ag:{9}, m:{10}" 
            + strConcat, health, mana, physicalResistance, magicalResistance, physicalDamage,
            magicalDamage, criticalDamage, strength, intellect, agility, movement );
    }

    public virtual string GetStats(string strConcat = "")
    {
        return string.Format("hp:{0}\n mana:{1}\n pRes:{2}\n mRes:{3}\n pDmg:{4}\n mDmg:{5}\n cDmg:{6}\n st:{7}\n i:{8}\n ag:{9}\n m:{10}\n"
                + strConcat, health, mana, physicalResistance, magicalResistance, physicalDamage,
                magicalDamage, criticalDamage, strength, intellect, agility, movement);
    }
    public virtual string GetStats(Stats inventoryStats, string strConcat = "" )
    {
        return string.Format("hp:{0}{11}\nmana:{1}{12}\npRes:{2}{13}\nmRes:{3}{14}\npDmg:{4}{15}\nmDmg:{5}{16}\ncDmg:{6}{17}\nst:{7}{18}\ni:{8}{19}\nag:{9}{20}\nm:{10}{21}\n"
                + strConcat, health, mana, physicalResistance, magicalResistance, physicalDamage,
                magicalDamage, criticalDamage, strength, intellect, agility, movement, Helper.MakeColor(inventoryStats.health), Helper.MakeColor(inventoryStats.mana), Helper.MakeColor(inventoryStats.physicalResistance), Helper.MakeColor(inventoryStats.magicalResistance), Helper.MakeColor(inventoryStats.physicalDamage),
                Helper.MakeColor(inventoryStats.magicalDamage), Helper.MakeColor(inventoryStats.criticalDamage), Helper.MakeColor(inventoryStats.strength), Helper.MakeColor(inventoryStats.intellect), Helper.MakeColor(inventoryStats.agility), Helper.MakeColor(inventoryStats.movement));
    }

    
}

