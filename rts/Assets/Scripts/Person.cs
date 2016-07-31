using UnityEngine;
using System.Collections;


 abstract class Person : MonoBehaviour
{
    protected string personName { get; set; }
    protected PersonStats Stats { get; set; }


    public Person(string personName = "Hero")
    {
        
    }

    protected virtual void OnDamage(int damage)
    {
        health =- damage;
    }
    protected virtual void OnRegen()
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
    protected virtual void OnRegen(int regen)
    {
        while (health != healthMax)
        {
            if (health + regen <= healthMax)
            {
                health =+ regen;
            }
            else
            {
                health = healthMax;
            }
        }
    }
    protected virtual void IncreaseHealthMax(int count)
    {
        healthMax =+ count;
    }



}

