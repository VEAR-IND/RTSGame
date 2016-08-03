using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersonsData : MonoBehaviour
{
    public List<Person> persons = new List<Person>() { };

    void Start()
    {
        persons.Add(new Person(new PersonStats(true, 1, 1, 2, 200, 200, 5, 200, 200, 5, 5, 5, 5, 5, 5, 2, 2, 2, 110), "1")); //assasin
        persons.Add(new Person(new PersonStats(true, 1, 2, 1, 300, 300, 5, 200, 200, 5, 5, 5, 5, 5, 5, 2, 2, 2, 110), "2")); //mage
        persons.Add(new Person(new PersonStats(true, 2, 1, 1, 500, 200, 5, 200, 200, 5, 5, 5, 5, 5, 5, 5, 2, 2, 110), "3")); //worrior   
    }

    	
}
