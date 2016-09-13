using UnityEngine;
using System.Collections;

public class Assasin : Person
{
    public Assasin(string name = "Assasin")
        :base(new PersonStats(), name)
    {
       
    }
    void Start()
    {
        base.personStats = new PersonStats(true, 1, 1, 2, 200, 200, 5, 200, 200, 5, 5, 5, 5, 5, 5, 2, 2, 2, 110);
    }

}
