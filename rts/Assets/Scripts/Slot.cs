using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class Slot : MonoBehaviour ,IDropHandler
{
    public int id;
    private Inventory inv;
    private PlaceholderInventory pInv;
    private GameObject inventoryPanel, placeholderPanel;
    
    // Use this for initialization
    void Start ()
    {
        pInv = GameObject.Find("Inventory").GetComponent<PlaceholderInventory>();
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        inventoryPanel = GameObject.Find("InventoryPanel");
        placeholderPanel = GameObject.Find("PlaceholderPanel");        
	}

    public void OnDrop(PointerEventData eventData)
    {
        ItemData dropedItem = eventData.pointerDrag.GetComponent<ItemData>();
        if (dropedItem != null && eventData.pointerCurrentRaycast.gameObject != null)
        {
            bool isFromPlaceholder = dropedItem.lastParent.IsChildOf(placeholderPanel.transform);
            bool isFromInventory = dropedItem.lastParent.IsChildOf(inventoryPanel.transform);
            bool isInInventory = eventData.pointerCurrentRaycast.gameObject.transform.IsChildOf(inventoryPanel.transform) 
                && dropedItem.lastParent.IsChildOf(inventoryPanel.transform);
            bool isSlotEmpty = inv.items[id].id == -1;
            if (isSlotEmpty)
            {
                if (isInInventory)
                {
                    inv.items[dropedItem.slotId] = new Item();
                }
                else if (isFromPlaceholder)
                {
                    pInv.items[dropedItem.slotId] = pInv.items[dropedItem.slotId].Erase();
                }
                inv.items[id] = dropedItem.item;
                dropedItem.slotId = id;
            }
            else if (dropedItem.slotId != id)
            {
                Transform item = this.transform.GetChild(0);
                item.GetComponent<ItemData>().slotId = dropedItem.slotId;
                item.transform.SetParent(inv.slots[dropedItem.slotId].transform);
                item.transform.position = inv.slots[dropedItem.slotId].transform.position;

                dropedItem.slotId = id;
                dropedItem.transform.SetParent(this.transform);
                dropedItem.transform.position = this.transform.position;
            }            
        }
    }
}
