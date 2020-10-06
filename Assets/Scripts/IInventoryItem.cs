using UnityEngine;
using UnityEditor;
using System;

namespace Assets.Scripts
{
    public interface IInventoryItem
    {
        string Name { get; }
        void OnPickup();
    }

    public class InventoryEventArgs : EventArgs
    {
        public IInventoryItem Item;

        public InventoryEventArgs(IInventoryItem item)
        {
            Item = item;
        }
    }
}