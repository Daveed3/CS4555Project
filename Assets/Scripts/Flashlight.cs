using UnityEngine;
using UnityEditor;

namespace Assets.Scripts
{
    public class Flashlight : InventoryItem
    {
        private bool IsOn = false;
        public AudioSource playerOnPickupRemark;

        public override void OnUse()
        {
            Light light = GetComponent<Light>();
            Renderer renderer = GetComponent<Renderer>();
            Debug.Log("IsOn is " + IsOn);
            if(!IsOn)
            {
                light.enabled = true;
                renderer.material.EnableKeyword("_EMISSION");
                IsOn = true;
            }
            else
            {
                light.enabled = false;
                renderer.material.DisableKeyword("_EMISSION");
                IsOn = false;
            }
        }

        public override void OnPickup()
        {
            base.OnPickup();
            playerOnPickupRemark.Play();
            gameObject.SetActive(true);
            transform.parent = parent.transform;
            transform.localPosition = PickupPosition;
            transform.localEulerAngles = PickupRotation;
        }
    }
}