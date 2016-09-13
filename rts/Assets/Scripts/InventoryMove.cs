using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class InventoryMove : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    private Vector2 offset;
    public void OnBeginDrag(PointerEventData eventData)
    {        
        offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
        this.transform.position = eventData.position;
       // GetComponent<CanvasGroup>().blocksRaycasts = false;        
    }
    public void OnDrag(PointerEventData eventData)
    {        
        this.transform.position = eventData.position - offset;       
    }
}
