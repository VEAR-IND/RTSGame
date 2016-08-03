using UnityEngine;
using System.Collections;

public class Warrior : Person    
{
    
   public Warrior(string name = "Warrior")
        :base(new PersonStats(), name)
   {
      
   }
   void Start()
   {
        base.personStats = new PersonStats(true, 2, 1, 1, 500, 200, 5, 200, 200, 5, 5, 5, 5, 5, 5, 5, 2, 2, 110);
   }
}