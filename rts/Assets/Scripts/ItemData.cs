using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler{
    public int count = 1;
    public int slotId;
    public Item item;      
    private Vector2 offset;
    public Transform lastParent;
    private GameObject inventoryPanel, placeholderPanel;
    private PlaceholderInventory placeholderInv;
    private Inventory inv;
    private Tooltip tooltip;
    public GameObject placeholderImage;
    private CanvasGroup canvasGroup;
    public bool isMoved = true;


    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        tooltip = inv.GetComponent<Tooltip>();
        placeholderInv = GameObject.Find("Inventory").GetComponent<PlaceholderInventory>();
        inventoryPanel = GameObject.Find("InventoryPanel");
        placeholderPanel = GameObject.Find("PlaceholderPanel");
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!item.isEmpty)
        {   
            offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
            lastParent = this.transform.parent;
            this.transform.SetParent(this.transform.parent.parent.parent.parent); 
            this.transform.position = eventData.position;
            canvasGroup.blocksRaycasts = false;            
            isMoved = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null && item.id != -1)
        {
            this.transform.position = eventData.position - offset;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isMoved)
        {
            ItemData dropedItem = eventData.pointerDrag.GetComponent<ItemData>();
            inv.ResetLastMove(lastParent.gameObject, dropedItem);//);
        }        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Deactivate();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.Activate(item);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            LeftClick(eventData);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            RightClick(eventData);
        }
    }
    private void RightClick(PointerEventData eventData)
    {
        Debug.Log("Use it right");
    }
    private void LeftClick(PointerEventData eventData)
    {
        Debug.Log("Use it left");
        if (item.isConsumable)
        {
            if (count >= 1)
            {
                count -= 1;
                transform.GetChild(0).GetComponent<Text>().text = count.ToString();
            }
            if (count == 0)
            {
                if (eventData.pointerCurrentRaycast.gameObject != null)
                {
                   inv.DeleteItem(eventData.pointerCurrentRaycast.gameObject.transform.parent.gameObject.GetComponent<Slot>());
                   tooltip.Deactivate();
                }
            }
        }
    }

    internal void MoveTo(Slot fromSlot)
    {
        this.isMoved = true;
        this.slotId = fromSlot.id;
        this.transform.SetParent(fromSlot.transform);
        this.transform.position = fromSlot.transform.position;
        this.canvasGroup.blocksRaycasts = true;
    }
    internal void MoveTo()
    {
        this.transform.SetParent(null);
    }
}
