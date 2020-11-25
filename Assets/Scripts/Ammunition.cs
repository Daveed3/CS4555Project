using UnityEngine;
using UnityEditor;

namespace Assets.Scripts
{
    public class Ammunition : InventoryItem
    {
        public Player Player;
        public readonly int cost = 250;
        public string Message;
        public Ammunition()
        {
            Message = $"You need {cost} score to buy this";
        }
        public override void OnPickup()
        {
            if (Player.Score >= cost)
            {
                Player.DecreaseScore(cost);

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