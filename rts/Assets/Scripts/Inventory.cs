using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    GameObject inventoryPanel;
    GameObject slotPanel;
    public GameObject inventorySlot;
    public GameObject inventoryItem;
    public int slotAmount;

    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    ItemDatabase database;

    void Start()
    {
        database = GetComponent<ItemDatabase>();

        inventoryPanel = GameObject.Find("InventoryPanel");
        slotPanel = inventoryPanel.transform.FindChild("SlotPanel").gameObject;
        for(int i = 0; i < slotAmount; i++)
        {
            items.Add(new Item());
            slots.Add(Instantiate(inventorySlot));
            slots[i].GetComponent<Slot>().id = i;
            slots[i].transform.SetParent(slotPanel.transform);
        }
        AddItem(0);
        AddItem(1);
        AddItem(2);
        AddItem(3);
        AddItem(3);
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
    public void DeleteItem(int slotid)
    {
        items[slotid] = new Item();
        GameObject deleteObj = slots[slotid].transform.GetChild(0).gameObject;
        deleteObj.transform.parent = null;
        Destroy(deleteObj);
    }

    int IsExist(int id)
    {
        for (int i = 0; i < items.Count; i++)
        {
           if(items[i].id == id)
           {
                return i;                
           }           
        }
        return -1;
    }
}

