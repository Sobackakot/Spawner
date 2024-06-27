 
using System.Collections.Generic;
using UnityEngine; 

public class InventoryPanel : MonoBehaviour
{
    public static InventoryPanel Instance;

    private List<Item> dataItems = new List<Item>();
    private List<ItemInSlot> itemsInSlot = new List<ItemInSlot>();
    private List<InventorySlot> inventorySlots = new List<InventorySlot>();

    public void Awake()
    {   
        if(Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(Instance);
        inventorySlots.AddRange(GetComponentsInChildren<InventorySlot>(false));
        itemsInSlot.AddRange(GetComponentsInChildren<ItemInSlot>(false));
    }
    private void Start()
    {
        dataItems = new List<Item>(); 
        for (int i =0; i < inventorySlots.Count; i++)
        {
            dataItems.Add(null);
        }
    }
    public void AddItemToInventorySlot(int index)
    {   
        if(index < inventorySlots.Count && dataItems[index]!=null)
            inventorySlots[index].SetItemInSlot(itemsInSlot[index], dataItems[index]);
    }
    public void RemoveItemToInventorySlot(int index)
    {
        if (index < inventorySlots.Count)
            inventorySlots[index].ResetItemInSlot(itemsInSlot[index]);
    }
    public bool PickUpItem(Item item)
    {
        for (int i = 0; i < dataItems.Count; i++)
        {
            if (dataItems[i] == null)
            {
                dataItems[i] = item;
                AddItemToInventorySlot(i);
                return true;
            }
        } 
        return false;
    }
    public void DropItem(Item item)
    {
        for (int i = 0; i < dataItems.Count; i++)
        {
            if (dataItems[i] == item)
            {
                dataItems[i] = null;
                RemoveItemToInventorySlot(i);
                return;
            }
        } 
    }

}
