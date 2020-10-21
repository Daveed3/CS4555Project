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

        public override void OnUse()
        {
            SimpleShoot script = GetComponent<SimpleShoot>();
            script.enabled = true;

            placeHolderPlayerArms.SetActive(false);
            cameraPlayerArms.SetActive(true);

            armAnimator.SetInteger("HoldingHandgun", 1);
            placeholderArmAnimator.SetInteger("HoldingHandgun", 1);
            bodyAnimator.SetInteger("HoldingHandgun", 1);

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
            bodyAnimator.SetInteger("HoldingHandgun", 0);

            base.OnPutAway();
        }
    }
}