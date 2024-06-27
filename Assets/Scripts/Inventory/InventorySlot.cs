
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    private RectTransform transformDropSlot;
    private void Start()
    {
        transformDropSlot = GetComponent<RectTransform>();
    }
     
    public void OnDrop(PointerEventData eventData)
    { 
        ItemInSlot pickItemInSlot = eventData.pointerDrag.GetComponent<ItemInSlot>();  
        Item pickDataItem = pickItemInSlot.itemData;

        if(pickDataItem != null && transformDropSlot.childCount > 0)
            DropItemInSlot(pickItemInSlot, pickDataItem);
    }
    private void DropItemInSlot(ItemInSlot pickItemInSloto, Item pickDataItem)
    {
        ItemInSlot dropItemInSlot = transformDropSlot.GetChild(0).GetComponent<ItemInSlot>();   
        Item dropDataItem = dropItemInSlot.itemData;
        dropItemInSlot.SetDataItem(pickDataItem);

        SwapItemInSlot(pickItemInSloto, dropDataItem); 
    }
    private void SwapItemInSlot(ItemInSlot pickItemInSloto, Item dropDataItem)
    {
        if (dropDataItem != null)
            pickItemInSloto.SetDataItem(dropDataItem);
        else pickItemInSloto.ResetDataItem();
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
