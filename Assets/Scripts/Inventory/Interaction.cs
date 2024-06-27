 
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public Item item;
    public Transform person;

    private void OnMouseDown()
    {
        float distance = Vector3.Distance(transform.position, person.position);
        if(distance <= 2)
        {
            if(InventoryPanel.Instance.PickUpItem(item))
                Destroy(gameObject);
        } 
    }
}
