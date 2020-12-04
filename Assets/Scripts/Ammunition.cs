using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class Ammunition : InventoryItem
    {
        public Player Player;
        public readonly int cost = 250;
        public string Message;

        public List<AudioSource> pickupAmmoRemarks;
        public AudioSource pickupRemark;

        public Ammunition()
        {
            Message = $"You need {cost} score to buy this";
        }
        public override void OnPickup()
        {
            Debug.Log("in ammo pickup function");
            if (Player.Score >= cost)
            {
                Player.DecreaseScore(cost);

                if (!pickupRemark.isPlaying)
                {
                    pickupRemark = pickupAmmoRemarks[Random.Range(0, pickupAmmoRemarks.Count)];
                    AudioManager.CheckAndPlayAudio(pickupRemark);
                }

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