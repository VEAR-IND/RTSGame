using UnityEngine;
using System.Collections;


public class Person : MonoBehaviour
{
    public string personName { get; set; }
    [SerializeField]
    private PersonStats personStats;


    public Person(string personName = "Hero")
    {
        personStats = new PersonStats();
        this.personName = personName;
    }
}

