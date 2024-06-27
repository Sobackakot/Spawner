 
using UnityEngine; 


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/newItem")]
public class Item : ScriptableObject 
{ 
    public string nameItem;
    public Sprite imageItem; 
}
