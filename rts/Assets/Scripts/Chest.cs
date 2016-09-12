using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System;
using System.Threading;
using System.Collections.Generic;

public class Chest : MonoBehaviour
{
    public delegate void ChestEventHandler(object sender, EventArgs e);
    public event ChestEventHandler onClick;
    public event ChestEventHandler onCreate;
    public event ChestEventHandler onOpenBeginFirstTime;
    public event ChestEventHandler onOpenBegin;
    public event ChestEventHandler onOpen;
    public event ChestEventHandler onCloseBegin;
    public event ChestEventHandler onClose;
    private bool isOpen = false;
    private bool isOpenedFirstTime = true;
    private Inventory inventory;
    private GameObject canvas;
    private GameObject inventoryPanel, slotPanel;
    public List<int> itemsIds = new List<int>() { };
    private bool hover = false;
    private MeshRenderer renderer;
    Camera camera;
    void Start()
    {
        camera = GameObject.Find("Camera").GetComponent<Camera>();
        canvas = GameObject.Find("Canvas");
        renderer = GetComponent<MeshRenderer>();
        if (onCreate != null)
        {
            onCreate(this.gameObject, EventArgs.Empty);
        }
        if (isOpenedFirstTime)
        {
            onOpenBeginFirstTime += GenerateItems;
            onOpenBeginFirstTime += (sender, e) => { this.isOpenedFirstTime = false; };
        }
        onOpenBegin += CreateInventoryPanel;
        onOpenBegin += CreateInventory;

        onOpen += ShowInventory;
        onOpen += (sender, e) => { this.isOpen = true; };
        onCloseBegin += CloseBegin;
        onClose += Close;
        onClose += (sender, e) => { this.isOpen = false; };
    }

    private void GenerateItems(object sender, EventArgs e)//1
    {
        System.Random rnd = new System.Random();

        int maxCount = 16;
        int minCount = 1;
        int spawnChance = rnd.Next(100);
        int randCount = rnd.Next(minCount, maxCount);
        int maxId = GameObject.Find("ItemDatabase").GetComponent<ItemDatabase>().database.Count;

        for (int i = 0; i < randCount; i++)
        {
            if (rnd.Next(100) < spawnChance)
            {
                itemsIds.Add(rnd.Next(maxId));
            }
        }
    }

    private void CreateInventoryPanel(object sender, EventArgs e)//2
    {
        inventoryPanel = Instantiate(Resources.Load<GameObject>("Prefabs/InventoryPanel"), canvas.transform, false) as GameObject;
        slotPanel = Instantiate(Resources.Load<GameObject>("Prefabs/SlotPanel"), inventoryPanel.transform, false) as GameObject;
    }

    private void CreateInventory(object sender, EventArgs e)
    {
        inventory = this.gameObject.AddComponent<Inventory>();
        inventory.inventoryPanel = inventoryPanel;
        inventory.slotPanel = slotPanel;
        inventory.inventorySlot = Resources.Load<GameObject>("Prefabs/Slot");
        inventory.inventoryItem = Resources.Load<GameObject>("Prefabs/Item");
        inventory.slotAmount = 16;
        inventory.onCreate += MoveItemsToInventory;
    }

    private void MoveItemsToInventory(object sender, EventArgs e)//3
    {
        inventory.AddAllItems(itemsIds);
    }

    private void ShowInventory(object sender, EventArgs e)
    {
        inventoryPanel.SetActive(true);
    }

    private void CloseBegin(object sender, EventArgs e)
    {
        inventoryPanel.SetActive(false);
        itemsIds = inventory.GetItemsIds();
        Destroy(inventory);
    }

    private void Close(object sender, EventArgs e)
    {
        Debug.Log("close");
        inventoryPanel.transform.SetParent(null);
        foreach (Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }
        Destroy(inventoryPanel);
    }
    
    void Update()
    {
        if (hover && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {            
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    if (hit.collider.gameObject == this.gameObject)
                    {
                        if (onClick != null)
                        {
                            onClick(this.gameObject, EventArgs.Empty);
                        }
                        if (!isOpen)
                        {
                            if (onOpenBeginFirstTime != null && !isOpen)
                            {
                                onOpenBeginFirstTime(this.gameObject, EventArgs.Empty);
                            }
                            if (onOpenBegin != null && onOpen != null && !isOpen)
                            {
                                onOpenBegin(this.gameObject, EventArgs.Empty);
                                onOpen(this.gameObject, EventArgs.Empty);
                            }
                        }
                        else
                        {
                            if (onCloseBegin != null && onClose != null && isOpen)
                            {
                                onCloseBegin(this.gameObject, EventArgs.Empty);
                                onClose(this.gameObject, EventArgs.Empty);
                            }
                        }
                        if (!isOpenedFirstTime)
                        {
                            onOpenBeginFirstTime = null;
                        }
                    }
                }
                
            }
            renderer.material.color = Color.yellow;
            hover = false;
        }
        else
        {
            renderer.material.color = Color.white;
        }
    }

    void OnMouseOver()
    {
        hover = true;
    }
}
