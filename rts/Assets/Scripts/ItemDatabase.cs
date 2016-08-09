using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System;
using Newtonsoft.Json;

public class ItemDatabase : MonoBehaviour {

   public List<Item> database = new List<Item>() { };

    void Awake()
    {
        
    }
    void Start ()
    {
        database.Add(new Item(
            "Bloody King",
            new ItemStats(true, physicalDamage: 12, strength: 4, criticalDamage: 10),
            "Sprites/",
            "Sword_1",
            "Sword of a king, who kill all his famyly. It's cursed, use it carefuly!",
            100,
            2.34,
            false,
            false,
            Item.ItemRarity.Epic
        ));
        database.Add(new Item(
            "The Big Ocean Sword",
            new ItemStats(true, physicalDamage: 16, strength: 3, criticalDamage: 2, agility: 2, mana: 50),
            "Sprites/",
            "Sword_5",
            "Sword from the bottom of the ocean, you can hear the sound of it.",
            120,
            3.74,
            false,
            false,
            Item.ItemRarity.Legendary    
        ));

        database.Add(new Item(
            "The Evil Sword",
            new ItemStats(true, health: -10, physicalDamage: 13, strength: 5, criticalDamage: 2),
            "Sprites/",
            "Sword_12",
            "Sword who kill so many creatures, that soak up with evil",
            84,
            5.14,
            false,
            false,
            Item.ItemRarity.Rare
        ));
        database.Add(new Item(
            "Health pousion",
            new ItemStats(true, health:100),
            "Sprites/",
            "Botles_0",
            "Drink this bottle and you will feel better. No, this is not vodka",
            10,
            0.1,
            true,
            false,
            Item.ItemRarity.Normal
        ));
        Debug.Log(database.Count);
    }
	
	void Update ()
    {
        
	}

    void OnApplicationQuit()
    {
        string allData = "";

        if (File.Exists(Application.dataPath + "/Resources/Data/Items.json"))
        {
            File.Copy(Application.dataPath + "/Resources/Data/Items.json",
                Application.dataPath + string.Format("/Resources/Data/Items{0}.json", GenerateUniqueNumber()));
        }
        foreach (Item item in database)
        {
            allData += JsonConvert.SerializeObject(item)+" \n ";
        }
        File.WriteAllText(Application.dataPath + "/Resources/Data/Items.json", allData);
    }

   public Item GetById(int id)
   {
        Item toReturn = database.Find(x => x.id == id);
        if (toReturn != null)
        {
            return toReturn;
        }
        else return null;
   }

   public string GenerateUniqueNumber()
   {
        long ticks = DateTime.Now.Ticks;
        byte[] bytes = BitConverter.GetBytes(ticks);
        return Convert.ToBase64String(bytes).Replace('+', '_').Replace('/', '-').TrimEnd('=');        
    }
}
