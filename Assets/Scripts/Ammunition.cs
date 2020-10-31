using UnityEngine;
using UnityEditor;

namespace Assets.Scripts
{
    public class Ammunition : InventoryItem
    {
        public Player Player;
        private readonly int _cost = 250;

        public override void OnPickup()
        {
            if (Player.Score >= _cost)
            {
                Player.DecreaseScore(_cost);

                if (Inventory.Items[0] != null)
                {
                    (Inventory.Items[0] as AssaultRifle).PickupAmmunition();
                }
                if (Inventory.Items[1] != null)
                {
                    (Inventory.Items[1] as Handgun).PickupAmmunition();
                }
            }
        }
    }
}