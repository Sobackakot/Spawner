 
using UnityEngine;

public class PickUpItemToInventory : MonoBehaviour //this PickUpItem script
{
    public Item item;
    public Transform person;

    private void OnMouseDown()
    {
        float distance = Vector3.Distance(transform.position, person.position);
        if(distance <= 2)
        {
            bool isHas = InventoryPanel.Instance.PickUpItem(item);
            if(isHas)
            {
                Destroy(gameObject);
            }
                
        } 
    }
}
