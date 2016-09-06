using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlaceholderInventory : MonoBehaviour
{
    GameObject inventoryPanel;
    GameObject slotPanel;
    public GameObject inventorySlot;
    public GameObject inventoryItem;

    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();
    public Stats stats = null;
    ItemDatabase database;

    void Start()
    {
        database = GetComponent<ItemDatabase>();
        inventoryPanel = GameObject.Find("PlaceholderPanel");
        slotPanel = inventoryPanel.transform.FindChild("SlotPanel").gameObject;        
        foreach (Transform child in slotPanel.transform)
        {
            child.GetComponent<PlaceholderSlot>().id = this.slots.Count;
            slots.Add(child.gameObject);
           
        }
        items.AddRange(new List<Item> { new WeaponItem(), new ShealdItem(), new HelmetItem(), new ChestItem(), new PantsItem(), new BootsItem(), new GlovesItem() });
        Debug.Log("tcyc");
    }
    void Update()
    {
        stats = ItemStats.GetSum(Item.Stats(items));
    }
}

