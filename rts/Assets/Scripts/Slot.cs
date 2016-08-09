using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class Slot : MonoBehaviour ,IDropHandler
{
    public int id;
    private Inventory inv;

    

    // Use this for initialization
    void Start ()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
	}

    public void OnDrop(PointerEventData eventData)
    {
        ItemData dropedItem = eventData.pointerDrag.GetComponent<ItemData>();
        if (inv.items[id].id == -1)
        {
            inv.items[dropedItem.slotId] = new Item();
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
