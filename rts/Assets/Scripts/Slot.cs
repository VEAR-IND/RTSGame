using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class Slot : MonoBehaviour ,IDropHandler
{
    public int id;
    public Inventory inv;
    protected GameObject inventoryPanel;
    public delegate void SlotEventHandler(object sender, EventArgs e);
    public virtual event SlotEventHandler onEmpty;
    public virtual event SlotEventHandler onFill;
    protected virtual GameObject slotItemGO
    {
        get
        { 
            return transform.childCount > 0 ? transform.GetChild(0).gameObject : null;
        }
    }
    protected virtual Item slotItem
    {
        get
        {
            return inv.items[id];
        }
        set
        {
            inv.items[id] = value;
        }
    }
    public virtual bool isSlotEmpty
    {
        get
        {
            return slotItem.isEmpty && slotItemGO == null;
        }
    }
    public virtual bool isSuitable(Type t)
    {       
        return true;        
    }
    
    void Start ()
    {
        inv = inv ?? GameObject.Find("Inventory").GetComponent<Inventory>();
        inventoryPanel = GameObject.Find("InventoryPanel");       
	}

    public void OnDrop(PointerEventData eventData)
    {
        ItemData dropedItem = eventData.pointerDrag.GetComponent<ItemData>();
        if (dropedItem != null && eventData.pointerCurrentRaycast.gameObject != null)
        {
            GameObject from = dropedItem.lastParent.gameObject;
            GameObject to = this.gameObject;
            Inventory.InventoryMove(from, dropedItem, to );
        }
    }
    internal virtual void Erase()
    {
        slotItem = new Item();
        if (onEmpty != null)
        {
            onEmpty(this.gameObject, EventArgs.Empty);
        }
    }

    internal virtual ItemData GetItemData
    {
        get
        {
            return slotItemGO.GetComponent<ItemData>();
        }
    }

    internal virtual void Fill(Item item)
    {
        slotItem = item;
        if (onFill != null)
        {
            onFill(this.gameObject, EventArgs.Empty);
        }
    }
}
