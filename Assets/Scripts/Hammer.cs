using UnityEngine;
using UnityEditor;

namespace Assets.Scripts
{
    public class Hammer : InventoryItem
    {
        public Animator armAnimator;
        public Animator placeholderArmAnimator;
        public Animator bodyAnimator;
        public GameObject placeHolderPlayerArms;
        public GameObject cameraPlayerArms;
        public GameObject playerBody;

        public override void OnUse()
        {
            HammerHit script = playerBody.GetComponent<HammerHit>();

            script.enabled = true;

            placeHolderPlayerArms.SetActive(false);
            cameraPlayerArms.SetActive(true);
            armAnimator.SetInteger("HoldingHammer", 1);
            placeholderArmAnimator.SetInteger("HoldingHammer", 1);
            bodyAnimator.SetInteger("HoldingHammer", 1);

            Debug.Log("holding hammer");

            base.OnUse();
        }

        public override void OnPutAway()
        {
            HammerHit script = playerBody.GetComponent<HammerHit>();

            script.enabled = false;

            placeHolderPlayerArms.SetActive(true);
            cameraPlayerArms.SetActive(false);
            armAnimator.SetInteger("HoldingHammer", 0);
            placeholderArmAnimator.SetInteger("HoldingHammer", 0);
            bodyAnimator.SetInteger("HoldingHammer", 0);

            Debug.Log("holding hammer");

            base.OnPutAway();
        }
    }
}