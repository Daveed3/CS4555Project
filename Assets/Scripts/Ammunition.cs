using UnityEngine;
using UnityEditor;

namespace Assets.Scripts
{
    public class Ammunition : InventoryItem
    {
        public override void OnPickup()
        {
            if(Inventory.Items[0] != null)
            {
                (Inventory.Items[0] as AssaultRifle).PickupAmmunition();
            }
            if(Inventory.Items[1] != null)
            {
                (Inventory.Items[1] as Handgun).PickupAmmunition();
            }
        }
    }
}