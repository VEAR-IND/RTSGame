using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class PlaceholderSlot : Slot
{
    public override event SlotEventHandler onEmpty;
    public override event SlotEventHandler onFill;
    protected PlaceholderInventory pInv;
    protected GameObject placeholderPanel;
    protected GameObject slotImageGO
    {
        get
        {
            return transform.childCount > 0 ? transform.GetChild(0).gameObject : null;
        }        
    }
    protected override GameObject slotItemGO
    {
        get
        {
            return transform.childCount > 1 ? transform.GetChild(1).gameObject : null;
        }
    }
    protected override Item slotItem
    {
        get
        {
            return pInv.items[id];
        }
        set
        {
            pInv.items[id] = value;
        }
    }
    public override bool isSlotEmpty
    {
        get
        {
            return slotItem.isEmpty && slotItemGO == null;
        }
    }

    void Start()
    {
        pInv = GameObject.Find("Inventory").GetComponent<PlaceholderInventory>();
        placeholderPanel = GameObject.Find("PlaceholderPanel");
    }

    public override bool isSuitable(Type t)
    {
        return slotItem.Type == t;
    }

    internal override void Erase()
    {
        slotItem = slotItem.Erase();
        if (onEmpty != null)
        {
            onEmpty(this.gameObject, EventArgs.Empty);
        }
    }
    internal override ItemData GetItemData
    {
        get
        {
            return slotItemGO.GetComponent<ItemData>();
        }
    }
    internal override void Fill(Item item)
    {
        slotItem = item;
        if (onFill != null)
        {
            onFill(this.gameObject, EventArgs.Empty);
        }
    }
}
