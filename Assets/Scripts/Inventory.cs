using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

namespace Assets.Scripts
{
    public class Inventory : MonoBehaviour
    {
        private const int INVENTORY_SLOTS = 4;
        private int itemCount;
        public IInventoryItem[] Items = new IInventoryItem[] { null, null, null, null };
        public event EventHandler<InventoryEventArgs> ItemAdded;

        public void AddItem(IInventoryItem item, int index)
        {
            if (itemCount < INVENTORY_SLOTS)
            {
                Collider collider = (item as MonoBehaviour).GetComponent<Collider>();

                if (collider.enabled)
                {
                    collider.enabled = false;
                    Items[index] = item;
                    item.OnPickup();
                    itemCount++;

                    ItemAdded?.Invoke(this, new InventoryEventArgs(item));
                }
            }
        }
    }
}