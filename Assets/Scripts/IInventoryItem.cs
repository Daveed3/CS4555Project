using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public interface IInventoryItem
    {
        string Name { get; set; }
        Sprite Image { get; set; }
        List<AudioSource> PlayerOnPickUpRemarks { get; set; }

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