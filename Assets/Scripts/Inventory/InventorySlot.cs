
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    private RectTransform transformDropSlot;
    private void Awake()
    {
        transformDropSlot = GetComponent<RectTransform>();
    }
     
    public void OnDrop(PointerEventData eventData)
    {   
        ItemInSlot pickItemInSlot = eventData.pointerDrag.GetComponent<ItemInSlot>();
        Item pickDataItem = pickItemInSlot.currentItem;  

        if (pickDataItem != null && transformDropSlot.childCount > 0)
            DropItemInSlot(pickItemInSlot, pickDataItem);
    }
     
    private void DropItemInSlot(ItemInSlot pickItemInSlot, Item pickDataItem)
    {
        ItemInSlot dropItemInSlot = transformDropSlot.GetChild(0).GetComponent<ItemInSlot>();
        Item dropDataItem = dropItemInSlot.currentItem;

        if (dropDataItem != null)
        {
            SetItemInSlot(dropItemInSlot, pickDataItem);
            SwapItemInSlot(pickItemInSlot, dropDataItem);
        }
        else
        {
            SetItemInSlot(dropItemInSlot, pickDataItem);
            ResetItemInSlot(pickItemInSlot);
        }
    } 
    private void SwapItemInSlot(ItemInSlot pickItemInSlot, Item dropDataItem)
    {
        SetItemInSlot(pickItemInSlot, dropDataItem);
    }
      
    public void SetItemInSlot(ItemInSlot item, Item data)
    {
        item.SetDataItem(data);
    } 
    public void ResetItemInSlot(ItemInSlot item)
    {
        item.ResetDataItem();
    } 
}
