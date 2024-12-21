using System.Collections.Generic;
using UnityEngine;

public class InventorySystem
{
    public void AddItem(InventoryItem item)
    {
        Debug.Log("Adding item to the cloud");
    }

    public void RemoveItem(InventoryItem item)
    {
        Debug.Log("Removing item from the cloud");
    }

    public List<InventoryItem> GetInventory()
    {
        Debug.Log("Returning an inventory list stored in the clound");
        return new List<InventoryItem>();
    }
}
