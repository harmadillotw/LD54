using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    public Transform parentAfterDrag;
    public Transform parentBeforeDrag;
    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject dragged = eventData.pointerDrag;
        cargoDetails cd = dragged.GetComponent<cargoDetails>();
        parentAfterDrag = transform.parent;
        if (transform.parent.tag == "Train")
        {
            foreach (TrainComponent comp in GlobalValues.train.trainComponents.Values)
            {
                if (comp.inventory.ContainsKey(cd.cargoId))
                {
                    comp.inventory.Remove(cd.cargoId);
                }
            }
        }
        if (transform.parent.tag == "Station")
        {

            if (GlobalValues.stationInventory.ContainsKey(cd.cargoId))
            {
                GlobalValues.stationInventory.Remove(cd.cargoId);
            }            
        }

        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
        GameObject dragged = eventData.pointerDrag;
        cargoDetails cd = dragged.GetComponent<cargoDetails>();
        if (transform.parent.tag == "Train")
        {
            int id = transform.parent.parent.gameObject.GetComponent<carriageID>().carriageId;
            if (GlobalValues.train.trainComponents.ContainsKey(id))
            {
                if (!GlobalValues.train.trainComponents[id].inventory.ContainsKey(cd.cargoId))
                {
                    GlobalValues.train.trainComponents[id].inventory.Add(cd.item.id, cd.item);
                }
            }
        }
        if (transform.parent.tag == "Station")
        {
            if (!GlobalValues.stationInventory.ContainsKey(cd.cargoId))
            {
                GlobalValues.stationInventory.Add(cd.item.id, cd.item);
            }
        }
    }
}
