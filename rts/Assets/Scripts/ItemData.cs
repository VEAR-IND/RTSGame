using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class ItemData : MonoBehaviour , IBeginDragHandler, IDragHandler, IEndDragHandler {
    public Item item;
    public int count = 1;
    public int slotId;
    private Vector2 offset;
    private Inventory inv;

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
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
}
