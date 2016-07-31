using UnityEngine;
using System.Collections;

public class PersonStats : Stats
{
    public int healthMax { get; set; }
    public int healthRegen { get; set; }

    public int manaMax { get; set; }
    public int manaRegen { get; set; }

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

    //substract demageCount from person health 
    public virtual void OnDamage(int damageCount)
    {
        if (health - damageCount >= 0)
        {
            health = -damageCount;
        }
        else
        {
            health = 0;
        }
    }

    public virtual void OnHealthRegen()
    {
        while (health != healthMax)
        {
            if (health + healthRegen <= healthMax)
            {
                health = +healthRegen;
            }
            else
            {
                health = healthMax;
            }
        }
    }
    public virtual void OnHealthRegen(int healthCount)
    {
        while (health != healthMax)
        {
            if (health + healthCount <= healthMax)
            {
                health = +healthCount;
            }
            else
            {
                health = healthMax;
            }
        }
    }
    public virtual void IncreaseHealthMax(int healthCount)
    {
        healthMax =+ healthCount;
    }

    public virtual void OnManaRegen()
    {
        while (health != healthMax)
        {
            if (health + healthRegen <= healthMax)
            {
                health = +healthRegen;
            }
            else
            {
                health = healthMax;
            }
        }
    }
    public virtual void OnManaRegen(int manaCount)
    {
        while (health != healthMax)
        {
            if (health + manaCount <= healthMax)
            {
                health = +manaCount;
            }
            else
            {
                health = healthMax;
            }
        }
    }
    public virtual void IncreaseManaMax(int manaCount)
    {
        manaMax = +manaCount;
    }

    public virtual void IncreasePhysicalResistance(int resistanceCount)
    {
        physicalResistance =+ resistanceCount;
    }

    public virtual void IncreaseMagicalResistance(int resistanceCount)
    {
        magicalResistance =+ resistanceCount;
    }
}
