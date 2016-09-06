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
    //flags
    bool isFromPlaceholder = false;
    bool isFromInventory = false;
    bool isInPlaceholder = false;
    public bool isSwaping = false;

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        tooltip = inv.GetComponent<Tooltip>();
        placeholderInv = GameObject.Find("Inventory").GetComponent<PlaceholderInventory>();
        inventoryPanel = GameObject.Find("InventoryPanel");
        placeholderPanel = GameObject.Find("PlaceholderPanel");        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null && item.id !=-1)
        {   
            offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
            lastParent = this.transform.parent;
            this.transform.SetParent(this.transform.parent.parent.parent.parent); 
            this.transform.position = eventData.position;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            //set flags
            isSwaping = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            this.transform.position = eventData.position - offset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            isFromPlaceholder = this.lastParent.IsChildOf(placeholderPanel.transform);
            isFromInventory = this.lastParent.IsChildOf(inventoryPanel.transform);
            isInPlaceholder = eventData.pointerCurrentRaycast.gameObject.transform.IsChildOf(placeholderPanel.transform) && this.lastParent.IsChildOf(placeholderPanel.transform);
            bool isInInventory = eventData.pointerCurrentRaycast.gameObject.transform.IsChildOf(inventoryPanel.transform) && this.lastParent.IsChildOf(inventoryPanel.transform);            
            if ((isInInventory && !isSwaping) || isFromPlaceholder && !isInPlaceholder)
            {
                this.transform.SetParent(inv.slots[slotId].transform);
                this.transform.position = inv.slots[slotId].transform.position;
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                if (placeholderImage != null)
                {
                    placeholderImage.gameObject.SetActive(true);
                    placeholderImage = null;
                }
            }
            else if (isInPlaceholder || isFromInventory)
            {
                this.transform.SetParent(placeholderInv.slots[slotId].transform);
                this.transform.position = placeholderInv.slots[slotId].transform.position;
                GetComponent<CanvasGroup>().blocksRaycasts = true;
            }            
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
            if (this.count >= 1)
            {
                count -= 1;
                transform.GetChild(0).GetComponent<Text>().text = count.ToString();
            }
            if (this.count == 0)
            {
                inv.DeleteItem(slotId);
                tooltip.Deactivate();
            }
        }
    }
}
