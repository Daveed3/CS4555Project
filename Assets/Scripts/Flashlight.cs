using UnityEngine;
using UnityEditor;

namespace Assets.Scripts
{
    public class Flashlight : InventoryItem
    {
        public override void OnUse()
        {
            base.OnUse();
        }

        public override void OnPickup()
        {
            base.OnPickup();

            base.OnUse();
        }
    }
}