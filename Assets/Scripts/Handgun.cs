using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class Handgun : InventoryItem
    {
        public Animator armAnimator;
        public Animator placeholderArmAnimator;
        public Animator bodyAnimator;
        public GameObject placeHolderPlayerArms;
        public GameObject cameraPlayerArms;
        public List<AudioSource> playerOnPickupRemarks;
        public AudioSource pickupRemark;

        public int Damage = 25;
        public const int MAX_COUNT = 150;
        public int AmmunitionCount = 150;
        private bool _hasAmmunition = true;

        public bool HasAmmunition {
            get
            {
                return AmmunitionCount > 0 ? true : false;
            }
            set
            {
                _hasAmmunition = value;
            }
        }

        public override void OnUse()
        {
            HandgunShoot script = GetComponent<HandgunShoot>();
            script.enabled = true;

            placeHolderPlayerArms.SetActive(false);
            cameraPlayerArms.SetActive(true);

            armAnimator.SetInteger("HoldingHandgun", 1);
            placeholderArmAnimator.SetInteger("HoldingHandgun", 1);

            base.OnUse();
        }

        public override void OnPutAway()
        {
            HandgunShoot script = GetComponent<HandgunShoot>();
            script.enabled = false;

            placeHolderPlayerArms.SetActive(true);
            cameraPlayerArms.SetActive(false);

            armAnimator.SetInteger("HoldingHandgun", 0);
            placeholderArmAnimator.SetInteger("HoldingHandgun", 0);

            base.OnPutAway();
        }
        public override void OnPickup()
        {
            if (!pickupRemark.isPlaying)
            {
                pickupRemark = GetRandomPickupRemark();
                pickupRemark.Play();
            }
            base.OnPickup();
        }

        public void Shoot()
        {
            if (HasAmmunition)
            {
                AmmunitionCount -= 1;
                Debug.Log($"ammunition is now {AmmunitionCount}");
            }
        }
        public void PickupAmmunition()
        {
            Debug.Log($"Picked up {Name} ammo!");
            AmmunitionCount = 150;
            HasAmmunition = true;
            Debug.Log($"{Name} ammo is now {AmmunitionCount}");
        }
        private AudioSource GetRandomPickupRemark()
        {
            return playerOnPickupRemarks[Random.Range(0, playerOnPickupRemarks.Count)];
        }
    }
}