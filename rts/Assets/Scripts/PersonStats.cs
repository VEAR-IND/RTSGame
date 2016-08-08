using UnityEngine;
using System.Collections;

[System.Serializable]
public class PersonStats : Stats
{
    //vars
    public int healthMax;
    public int healthRegen;
    public int manaMax;
    public int manaRegen;
    public int strengthToHealth;
    public int intellectToMana;
    public int agilityToMovement;

    //flags
    public bool isAlive = true;

    //ctors
    public PersonStats() { }
    public PersonStats(bool isDefault, int strengthToHealth = 0, int intellectToMana = 0, int agilityToMovement = 0, int health = 100,
                       int healthMax = 100, int healthRegen = 1, int mana = 100, int manaMax = 100, int manaRegen = 1,
                       int physicalResistance = 1, int magicalResistance = 1, int physicalDamage = 1, int magicalDamage = 1,
                       int criticalDamage = 1, int strength = 1, int intellect = 1, int agility = 1, int movement = 100)
        :base(isDefault, health, mana, physicalResistance, magicalResistance, physicalDamage, magicalDamage, criticalDamage, strength,
            intellect, agility, movement)
    {        
        this.healthMax = healthMax;
        this.healthRegen = healthRegen;
        this.manaMax = manaMax;
        this.manaRegen = manaRegen;
        this.strengthToHealth = strengthToHealth;
        this.intellectToMana = intellectToMana;
        this.agilityToMovement = agilityToMovement;
    }

    //metods
    public override void ShowStats(string stringConcat = "")
    {                   
        base.ShowStats(string.Format(", hpMax:{0}, hpReg:{1}, manaMax:{2}, manaReg:{3}" +stringConcat, healthMax, healthRegen, manaMax, manaRegen));
    }

    public override string GetStats(string stringConcat = "")
    {
       return base.GetStats(string.Format(", hpMax:{0}\n, hpReg:{1}\n, manaMax:{2}\n, manaReg:{3}\n" + stringConcat, healthMax, healthRegen, manaMax, manaRegen));
    }

    public virtual void OnDamage(int damageCount)
    {
        if (health - damageCount >= 0)
        {
            health = -damageCount;
        }
        else
        {
            health = 0;
            isAlive = false;
        }
    }

    public virtual void OnHealthRegen()
    {
        if (isAlive)
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
    }

    public virtual void OnHealthRegen(int healthCount)
    {
        if (isAlive)
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
    }

    public virtual void IncreaseHealthMax(int healthCount)
    {
        healthMax =+ healthCount;
    }

    public virtual void OnManaRegen()
    {
        while (mana != manaMax)
        {
            if (mana + manaRegen <= manaMax)
            {
                mana = +manaRegen;
            }
            else
            {
                mana = manaMax;
            }
        }
    }

    public virtual void OnManaRegen(int manaCount)
    {
        while (mana != healthMax)
        {
            if (mana + manaCount <= healthMax)
            {
                mana = +manaCount;
            }
            else
            {
                mana = manaMax;
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

    public virtual void IncreasePhysicalDamage(int damageCount)
    {
        physicalDamage = +damageCount;
    }

    public virtual void IncreaseMagicalDamage(int damageCount)
    {
        magicalDamage  = +damageCount;
    }

    public virtual void IncreaseCriticalDamage(int damageCount)
    {
        criticalDamage = +damageCount;
    }

    public virtual void IncreaseMovement(int movementCount)
    {
        movement = +movementCount;
    }

    public virtual void IncreaseStrenght(int strengthCount)
    {
        strength = +strengthCount;
        IncreaseHealthMax(strengthCount * strengthToHealth);
    }

    public virtual void IncreaseAgility(int agilityCount)
    {
        agility = +agilityCount;
        IncreaseMovement(agilityCount * agilityToMovement);
    }

    public virtual void IncreaseIntellect(int intellectCount)
    {
        intellect = +intellectCount;
        IncreaseManaMax(intellectCount * intellectToMana);
    }
}
