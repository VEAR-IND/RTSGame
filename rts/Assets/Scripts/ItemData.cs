using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class ItemData : MonoBehaviour , IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler{
    public Item item;
    public int count = 1;
    public int slotId;
    private Tooltip tooltip;
    private Vector2 offset;
    private Inventory inv;

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        tooltip = inv.GetComponent<Tooltip>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
           
            this.transform.SetParent(this.transform.parent.parent); 
            this.transform.position = eventData.position;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
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
        this.transform.SetParent(inv.slots[slotId].transform);
        this.transform.position = inv.slots[slotId].transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
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
