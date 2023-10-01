using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DragableItem draggableItem = dropped.GetComponent<DragableItem>();
            draggableItem.parentAfterDrag = transform;
        }
    }

    // Start is called before the first frame update

}
