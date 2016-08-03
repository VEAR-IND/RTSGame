using UnityEngine;
using System.Collections;

public class Mage : Person
{ 

   public Mage(string newName = "Mage")
        :base(new PersonStats(), newName)
   {
        
   }
   void Start()
   {
       base.personStats = new PersonStats(true, 1, 2, 1, 300, 300, 5, 200, 200, 5, 5, 5, 5, 5, 5, 2, 2, 2, 110);
   }
}
