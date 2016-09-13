using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {

    public Item item;
    public string data;
    public GameObject tooltip;
    public GameObject inventoryPanel;
    
    void Start()
    {
        //tooltip = GameObject.Find("Tooltip");
        //inventoryPanel = GameObject.Find("InventoryPanel");
        tooltip.SetActive(false);
    }
    void Update()
    {
        if (tooltip.activeSelf)
        {
            tooltip.transform.position = Input.mousePosition;
        }
        if (!inventoryPanel.activeInHierarchy)
        {
            Deactivate();
        }
    }
    // Use this for initialization
    public void Activate(Item item)
    {
        this.item = item;       
        tooltip.SetActive(true);
        ConstructDataString();
        tooltip.GetComponentInChildren<Text>().text = data;
    }

    public void Deactivate()
    {
        data = "";
        tooltip.SetActive(false);
    }
    public void ConstructDataString()
    {
        string temp = "";
        switch (item.itemRariry)
        {
            case Item.ItemRarity.Junk: temp += AddColor("A9A9A9", item.itemName + '\n' + item.itemDiscription);break;
            case Item.ItemRarity.Normal: temp += AddColor("FFFFFF", item.itemName + '\n' + item.itemDiscription); break;
            case Item.ItemRarity.Fine: temp += AddColor("16D900", item.itemName + '\n' + item.itemDiscription); break;
            case Item.ItemRarity.Rare: temp += AddColor("00FFFF", item.itemName + '\n' + item.itemDiscription); break;
            case Item.ItemRarity.Epic: temp += AddColor("640C99", item.itemName + '\n' + item.itemDiscription); break;
            case Item.ItemRarity.Legendary: temp += AddColor("FFEF00", item.itemName + '\n' + item.itemDiscription); break;
        }

        data += temp;
        data += item.itemStats.GetStats();
    }

    public string AddColor(string color, string text)
    {
        return string.Format("<color=#{0}>{1}</color>", color, text);

    }
}
