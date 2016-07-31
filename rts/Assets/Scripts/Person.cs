using UnityEngine;
using System.Collections;


 abstract class Person : MonoBehaviour
{
    public string personName { get; set; }
    public PersonStats personStats { get; set; }


    public Person(string personName = "Hero")
    {
        personStats = new PersonStats();
    }
}

