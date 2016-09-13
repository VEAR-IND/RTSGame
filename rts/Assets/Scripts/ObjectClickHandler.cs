using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;


class ObjectClickHandler : MonoBehaviour
{
    Chest chest;
    void Start()
    {
        chest = this.GetComponent<Chest>(); 

        chest.onClick += ChangeColor;

    }
    void ChangeColor(object sender, EventArgs e)
    {
        GameObject g = sender as GameObject;
        g.GetComponent<MeshRenderer>().material.color = GetRandomColor();
    }





    Color GetRandomColor()
    {
        System.Random rnd = new System.Random();
        return new Color((float)rnd.NextDouble(), (float)rnd.NextDouble(), (float)rnd.NextDouble());
    }

    
        
}

