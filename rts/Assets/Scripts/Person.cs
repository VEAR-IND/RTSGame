using UnityEngine;
using System.Collections;

[System.Serializable]
public class Person : MonoBehaviour
{

    public string personName;
    public bool isStatsShown = false;
    public PersonStats personStats = new PersonStats();


    public Person(PersonStats personStats, string personName = "Hero")
    {
        this.personStats = personStats;
        this.personName = personName;
    }
}

