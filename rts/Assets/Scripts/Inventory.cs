using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject slotPanel;
    public GameObject inventorySlot;
    public GameObject inventoryItem;

    public delegate void InventoryEventHandler(object sender, EventArgs e);
    public event InventoryEventHandler onCreate;
    public virtual event  InventoryEventHandler onUpdate;
    public int slotAmount;

    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    public bool isPlayerOwner = false;
    public ItemDatabase database;
    private bool isFiled = false;
    void Start()
    {        
        if (inventoryPanel != null && slotPanel != null && inventorySlot != null && inventoryItem != null && !isFiled)
        {
            CreateGrid();
        }        
    }
    public void CreateGrid()
    {
        database = GameObject.Find("ItemDatabase").GetComponent<ItemDatabase>();
        for (int i = 0; i < slotAmount; i++)
        {
            items.Add(new Item());
            slots.Add(Instantiate(inventorySlot,slotPanel.transform) as GameObject);
            slots[i].GetComponent<Slot>().id = i;
            slots[i].GetComponent<Slot>().inv = this;
            slots[i].GetComponent<Slot>().onEmpty += (sender, e) =>
            {
                if (onUpdate != null)
                {
                    onUpdate(this.gameObject, EventArgs.Empty);
                }
            };
            slots[i].GetComponent<Slot>().onFill += (sender, e) =>
            {
                if (onUpdate != null)
                {
                    onUpdate(this.gameObject, EventArgs.Empty);
                }
            };
        }
        isFiled = true;
        if (isPlayerOwner)
        {
            AddAllItems();
        }
        if (onCreate != null)
        {
            onCreate(this.gameObject, EventArgs.Empty);
        }

    }
    
    public void AddAllItems()
    {
        foreach (Item item in database.database)
        {
            AddItem(item.id);
        }
    }
    public void AddAllItems(List<int> ids)
    {
        foreach (int id in ids)
        {
            AddItem(id);
        }
    }
    public void AddItem(int id)
    {
        if (database.GetById(id) != null)
        {
            Item itemToAdd = database.GetById(id);
            if (itemToAdd.isStackable && IsExist(itemToAdd.id) != -1)
            {
                for (int i = 0; i < slots.Count; i++)
                {
                    if (items[i].id == itemToAdd.id)
                    {
                        ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                        data.count++;
                        data.transform.GetChild(0).GetComponent<Text>().text = data.count.ToString();
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < slots.Count; i++)
                {
                    if (items[i].id == -1)
                    {
                        items[i] = itemToAdd;
                        GameObject itemObj = Instantiate(inventoryItem);
                        itemObj.transform.SetParent(slots[i].transform);
                        itemObj.GetComponent<ItemData>().item = itemToAdd;
                        itemObj.GetComponent<ItemData>().slotId = i;
                        itemObj.GetComponent<Image>().sprite = itemToAdd.itemSpite;
                        itemObj.transform.position = Vector2.zero;
                        itemObj.name = itemToAdd.itemName;
                        break;
                    }
                }
            }
        }
    }
    public void ResetLastMove(GameObject _fromSlot, ItemData _dropedItemData)
    {
        InventoryMove(_fromSlot, _dropedItemData);
    }
    public virtual void DeleteItem(Slot _clickedSlot)
    {
        ItemData clickedItemData = _clickedSlot.GetItemData;
        _clickedSlot.Erase();
        clickedItemData.MoveTo();
        Destroy(clickedItemData.gameObject);
    }
    internal virtual List<int> GetItemsIds()
    { 
        var test = items.FindAll(i => i.id != -1);
        var test2 = test.ConvertAll<int>(c => c.id);
        return test2 as List<int>;
    }
    protected int IsExist(int _id)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].id == _id)
            {
                return i;
            }
        }
        return -1;
    }
    
    public static void InventoryMove(GameObject _fromSlot, ItemData dropedItemData, GameObject _toSlot = null)
    {
        var fromSlot = _fromSlot != null ? (_fromSlot.gameObject.GetComponent<Slot>() != null ? _fromSlot.gameObject.GetComponent<Slot>() : _fromSlot.gameObject.GetComponent<PlaceholderSlot>()) : null;
        var toSlot = _toSlot != null ? (_toSlot.gameObject.GetComponent<Slot>() != null ? _toSlot.gameObject.GetComponent<Slot>() : _toSlot.gameObject.GetComponent<PlaceholderSlot>()): null;
        if(toSlot == null || !toSlot.isSuitable(dropedItemData.item.GetType()))
        {
            dropedItemData.MoveTo(fromSlot);
        }
        else if (toSlot.isSlotEmpty)
        {
            fromSlot.Erase();
            toSlot.Fill(dropedItemData.item);
            dropedItemData.MoveTo(toSlot);
        }
        else if (toSlot != fromSlot )
        {
            ItemData swapItemData = toSlot.GetItemData;
            swapItemData.MoveTo(fromSlot);
            fromSlot.Fill(swapItemData.item);
            toSlot.Fill(dropedItemData.item);
            dropedItemData.MoveTo(toSlot);
        }
        else
        {
            dropedItemData.MoveTo(fromSlot);
        }
    }
}
    
   