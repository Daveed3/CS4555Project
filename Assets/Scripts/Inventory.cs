using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

namespace Assets.Scripts
{
    public class Inventory : MonoBehaviour
    {
        private const int INVENTORY_SLOTS = 5;
        private int itemCount;
        public static IInventoryItem[] Items = new IInventoryItem[INVENTORY_SLOTS];
        public event EventHandler<InventoryEventArgs> ItemAdded;

        public void AddItem(IInventoryItem item, int index)
        {
            if (itemCount < INVENTORY_SLOTS)
            {
                Collider collider = (item as MonoBehaviour).GetComponent<Collider>();

                if (collider.enabled)
                {
                    // don't hide the collider for building materials since the player should be able to pick more up in the future
                    if (index != 4)
                    {
                        collider.enabled = false;
                    }
                    Items[index] = item;
                    item.OnPickup();
                    itemCount++;

                    ItemAdded?.Invoke(this, new InventoryEventArgs(item));
                }
            }
        }
    }
}