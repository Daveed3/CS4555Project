using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

public class Inventory : MonoBehaviour
{
    private const int INVENTORY_SLOTS = 4;
    private List<IInventoryItem> Items = new List<IInventoryItem>();
    public event EventHandler<InventoryEventArgs> ItemAdded;

    public void AddItem(IInventoryItem item)
    {
        if(Items.Count < INVENTORY_SLOTS)
        {
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();

            if(collider.enabled)
            {
                collider.enabled = false;
                Items.Add(item);
                item.OnPickup();

                ItemAdded?.Invoke(this, new InventoryEventArgs(item));
            }
        }
    }

}