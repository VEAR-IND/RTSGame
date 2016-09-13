using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class PlaceholderInventory : Inventory
{
    public Stats stats
    {
        get
        {
            return ItemStats.GetSum(Item.Stats(items));
        }
    }
    public override event InventoryEventHandler onUpdate;

    void Start()
    {
        database = GameObject.Find("ItemDatabase").GetComponent<ItemDatabase>();
        inventoryPanel = GameObject.Find("PlaceholderPanel");
        slotPanel = inventoryPanel.transform.FindChild("SlotPanel").gameObject;
        items.AddRange(new List<Item> { new WeaponItem(), new ShealdItem(), new HelmetItem(), new ChestItem(), new PantsItem(), new BootsItem(), new GlovesItem() });
        foreach (Transform child in slotPanel.transform)
        {
            child.GetComponent<PlaceholderSlot>().id = this.slots.Count;
            slots.Add(child.gameObject);
            slots[slots.Count - 1].GetComponent<PlaceholderSlot>().onEmpty += (sender, e) =>
            {
                if (this.onUpdate != null)
                {
                    this.onUpdate(this.gameObject, EventArgs.Empty);
                }
            };
            slots[slots.Count - 1].GetComponent<PlaceholderSlot>().onFill += (sender, e) =>
            {
                if (this.onUpdate != null)
                {
                    this.onUpdate(this.gameObject, EventArgs.Empty);
                }
            };
        }

        //items.AddRange(new List<Item> { new WeaponItem(), new ShealdItem(), new HelmetItem(), new ChestItem(), new PantsItem(), new BootsItem(), new GlovesItem() });
    }

}

