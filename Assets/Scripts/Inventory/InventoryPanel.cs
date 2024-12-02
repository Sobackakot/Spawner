 
using System.Collections.Generic;
using UnityEngine;  
public class InventoryPanel : MonoBehaviour
{
    public static InventoryPanel Instance;
    public Transform personTransform;
     
    private List<Item> dataItems;  
    private List<ItemInSlot> itemsInSlot = new List<ItemInSlot>();
    private List<InventorySlot> inventorySlots = new List<InventorySlot>();  


    private Rigidbody rb;

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

        for (int i = 0; i < inventorySlots.Count; i ++)
        {
            dataItems.Add(null); 
        } 
    }
    public void AddItem(int index)
    {   
        if(index < inventorySlots.Count && dataItems[index]!=null)
            inventorySlots[index].SetItemInSlot(itemsInSlot[index], dataItems[index]);
    }
       
    public bool PickUpItem(Item newItem)
    {
        for (int Index = 0; Index < dataItems.Count; Index++)
        {
            if (dataItems[Index] == null)
            {
                dataItems[Index] = newItem;
                AddItem(Index); 
                return true;
            }
        } 
        return false;
    }
     
    public void RemoveItemToInventorySlot(int index)
    {
        if (index < inventorySlots.Count)
            inventorySlots[index].ResetItemInSlot(itemsInSlot[index]);
    }
    public void DropItem(Item item)
    {
        for (int i = 0; i < dataItems.Count; i++)
        {
            if (dataItems[i] == item)
            {
                dataItems[i] = null;
                RemoveItemToInventorySlot(i);
                DropItemfromInventpory(item.nameItem);
                return;
            }
        } 
    }
    private void DropItemfromInventpory(string nameItem)
    { 
        string loadPath = nameItem; 
        GameObject itemPrefab =  (GameObject)Resources.Load(loadPath);


       float pointX = Random.Range(personTransform.position.x -4, personTransform.position.x + 4);
        float pointZ = Random.Range(personTransform.position.z - 4, personTransform.position.z + 4);
        Vector3 pointSpawn = new Vector3(pointX, personTransform.position.y, pointZ);  

        Instantiate(itemPrefab, pointSpawn, Quaternion.identity);
    }

}
