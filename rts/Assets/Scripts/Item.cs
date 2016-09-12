using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

[System.Serializable]
public class Item
{
    private static int lastId = 0;
    internal bool isEmpty
    {
        get
        {
            return id == -1 && this != null;
        }
    }
    internal Type Type
    {
        get
        {
            return this.GetType();
        }
    }

    public int id;
    public string itemName;
    public string itemDiscription;
    public int itemCost;
    public double itemWeight;
    public bool isStackable;
    public bool isConsumable;
    public ItemStats itemStats;
    public ItemRarity itemRariry;

    [Newtonsoft.Json.JsonIgnore]
    public Sprite itemSpite;
    private string itemSpritePath;
    private string itemSpriteName;

    [Newtonsoft.Json.JsonIgnore]
    public GameObject itemObject;
    private string itemObjectPath;

    public enum ItemRarity
    {
        Junk = 0,
        Normal = 1,
        Fine = 2,
        Rare = 3,
        Epic = 4,
        Legendary = 5
    };

    public Item()
    {
        this.id = -1;
    }
    public Item(bool isEmpty)
    {
        this.id = lastId++;
    }

    public Item(string itemName, string itemDiscription = "This is...", int itemCost = 10, double itemWeight = 1, bool isConsumable = false, bool isStackable = false, ItemRarity itemRariry = ItemRarity.Junk) : this(true)
    {
        this.itemName = itemName;
        this.itemDiscription = itemDiscription;
        this.itemCost = itemCost;
        this.itemWeight = itemWeight;
        this.isStackable = isStackable;
        this.itemRariry = itemRariry;
        this.isConsumable = isConsumable;
    }

    public virtual Item Erase()
    {
        return new Item();
    }

    public Item(string itemName, ItemStats itemStats, string itemDiscription = "This is...", int itemCost = 10, double itemWeight = 1, bool isConsumable = false, bool isStackable = false, ItemRarity itemRariry = ItemRarity.Junk) : this(itemName, itemDiscription, itemCost, itemWeight, isConsumable, isStackable, itemRariry)
    {
        this.itemStats = itemStats;
    }
    public Item(string itemName, ItemStats itemStats, string itemSpritePath, string itemSpriteName, string itemDiscription = "This is...", int itemCost = 10, double itemWeight = 1, bool isConsumable = false, bool isStackable = false, ItemRarity itemRariry = ItemRarity.Junk) : this(itemName, itemStats, itemDiscription, itemCost, itemWeight, isConsumable, isStackable, itemRariry)
    {
        
        Sprite[] sprites = Resources.LoadAll<Sprite>(itemSpritePath);
        foreach (Sprite s in sprites)
        {
            if (s.name == itemSpriteName)
            {
                this.itemSpite = s;
                break;
            }
        }
    }
    public Item(string itemName, ItemStats itemStats, string itemSpritePath, string itemSpriteName, string itemObjectPath, string itemDiscription = "This is...", int itemCost = 10, double itemWeight = 1, bool isConsumable = false, bool isStackable = false, ItemRarity itemRariry = ItemRarity.Junk) : this(itemName, itemStats, itemSpritePath, itemSpriteName, itemDiscription, itemCost, itemWeight, isConsumable, isStackable, itemRariry)
    {
        string objectPath = Application.dataPath + itemObjectPath;
        if (File.Exists(objectPath))
        {
            this.itemObject = Resources.Load<GameObject>(itemObjectPath);
        }
        else
        {
            Debug.Log(string.Format("No object!!! at item {}", this.id));
        }
    }
    public virtual void UseItem()
    {
        Debug.Log("Use it");
    }

   
    public static List<ItemStats> Stats(List<Item> items)
    {
        List<ItemStats> returnList = new List<ItemStats>();
        foreach (Item item in items.FindAll(i => i.id != -1))
        {
            returnList.Add(item.itemStats);
        }

        return returnList.Count != 0 ? returnList: null;
    }
}