using UnityEngine;
using UnityEditor;

namespace Assets.Scripts
{
    public class Handgun : InventoryItem
    {
        public Animator armAnimator;
        public Animator placeholderArmAnimator;
        public Animator bodyAnimator;
        public GameObject placeHolderPlayerArms;
        public GameObject cameraPlayerArms;
       
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
            SimpleShoot script = GetComponent<SimpleShoot>();
            script.enabled = true;

            placeHolderPlayerArms.SetActive(false);
            cameraPlayerArms.SetActive(true);

            armAnimator.SetInteger("HoldingHandgun", 1);
            placeholderArmAnimator.SetInteger("HoldingHandgun", 1);

            base.OnUse();
        }

        public override void OnPutAway()
        {
            SimpleShoot script = GetComponent<SimpleShoot>();
            script.enabled = false;

            placeHolderPlayerArms.SetActive(true);
            cameraPlayerArms.SetActive(false);

            armAnimator.SetInteger("HoldingHandgun", 0);
            placeholderArmAnimator.SetInteger("HoldingHandgun", 0);

            base.OnPutAway();
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
    }
}