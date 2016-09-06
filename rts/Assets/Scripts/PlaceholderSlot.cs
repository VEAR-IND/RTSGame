using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class PlaceholderSlot : MonoBehaviour, IDropHandler
{
    public int id;
    private PlaceholderInventory pInv;
    private Inventory inv;
    private GameObject inventoryPanel, placeholderPanel;
    // Use this for initialization
    void Start()
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
            bool isInInventory = eventData.pointerCurrentRaycast.gameObject.transform.IsChildOf(inventoryPanel.transform) && dropedItem.lastParent.IsChildOf(inventoryPanel.transform);
            bool isSlotEmpty = pInv.items[id].id == -1;
            if (pInv.items[id].GetType() == dropedItem.item.GetType())
            {
                GameObject slotChild1 = null;
                dropedItem.placeholderImage = this.transform.GetChild(0).gameObject;
                if (this.transform.childCount > 1)
                {
                    slotChild1 = this.transform.GetChild(1).gameObject;
                }               
                if (isSlotEmpty)
                {
                    dropedItem.placeholderImage.gameObject.SetActive(false);
                    inv.items[dropedItem.slotId] = new Item();
                    pInv.items[id] = dropedItem.item;
                    dropedItem.slotId = id;
                }
                else if (slotChild1 != null)
                {
                    if (dropedItem.lastParent != slotChild1.GetComponent<ItemData>().lastParent)
                    {
                        Transform item = this.transform.GetChild(1);
                        item.GetComponent<ItemData>().slotId = dropedItem.slotId;
                        item.transform.SetParent(inv.slots[dropedItem.slotId].transform);
                        item.transform.position = inv.slots[dropedItem.slotId].transform.position;

                        inv.items[dropedItem.slotId] = item.GetComponent<ItemData>().item;
                        pInv.items[id] = dropedItem.item;

                        dropedItem.isSwaping = true;
                        dropedItem.slotId = id;
                        dropedItem.transform.SetParent(this.transform);
                        dropedItem.transform.position = this.transform.position;
                    }
                }
            }
        }       
    }
}
