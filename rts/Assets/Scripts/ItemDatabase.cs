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
        database.Add(new WeaponItem(
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
        database.Add(new WeaponItem(
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

        database.Add(new WeaponItem(
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
            true,
            Item.ItemRarity.Normal
        ));

        database.Add(new HelmetItem(
            "Leather helmet",
            new ItemStats(true, physicalResistance:20, movement: 1),
            "Sprites/",
            "Sets_0",
            "One of the cheapest and lightest helmet, not match resistanse, but it do'nt hold down movements",18,
            0.3,
            false,
            false,
            Item.ItemRarity.Normal
        ));
        database.Add(new ChestItem(
            "Leather chest",
            new ItemStats(true, physicalResistance: 40, movement: 1),
            "Sprites/",
            "Sets_5",
            "One of the cheapest and lightest chest, not match resistanse, but it do'nt hold down movements", 57,
            0.7,
            false,
            false,
            Item.ItemRarity.Normal
        ));
        database.Add(new PantsItem(
            "Leather pants",
            new ItemStats(true, physicalResistance: 32, movement: 1),
            "Sprites/",
            "Sets_10",
            "One's of the cheapest and lightest pants, not match resistanse, but they do'nt hold down movements", 34,
            0.65,
            false,
            false,
            Item.ItemRarity.Normal
        ));
        database.Add(new BootsItem(
            "Leather boots",
            new ItemStats(true, physicalResistance: 18, movement: 1),
            "Sprites/",
            "Sets_15",
            "One's of the cheapest and lightest boots, not match resistanse, but they do'nt hold down movements", 26,
            0.42,
            false,
            false,
            Item.ItemRarity.Normal
        ));
        database.Add(new GlovesItem(
           "Leather gloves",
           new ItemStats(true, physicalResistance: 9, movement: 1),
           "Sprites/",
           "Sets_20",
           "One's of the cheapest and lightest gloves, not match resistanse, but they do'nt hold down movements", 12,
           0.15,
           false,
           false,
           Item.ItemRarity.Normal
       ));
        database.Add(new ShealdItem(
           "Leather sheald",
           new ItemStats(true, physicalResistance: 43, movement: 1),
           "Sprites/",
           "Sets_25",
           "One's of the cheapest and lightest sheald, not match resistanse, but it do'nt hold down movements", 52,
           0.95,
           false,
           false,
           Item.ItemRarity.Normal
       ));
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
