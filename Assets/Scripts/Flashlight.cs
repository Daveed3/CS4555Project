using UnityEngine;
using UnityEditor;

namespace Assets.Scripts
{
    public class Flashlight : InventoryItem
    {
        private bool isOn = false;

        public override void OnUse()
        {
            Light light = GetComponent<Light>();
            Renderer renderer = GetComponent<Renderer>();
            Debug.Log("isOn is " + isOn);
            if(!isOn)
            {
                light.enabled = true;
                renderer.material.EnableKeyword("_EMISSION");
            }
            else
            {
                light.enabled = false;
                renderer.material.DisableKeyword("_EMISSION");
            }

            isOn = !isOn;
        }

        public override void OnPickup()
        {
            base.OnPickup();

            gameObject.SetActive(true);
            transform.parent = parent.transform;
            transform.localPosition = PickupPosition;
            transform.localEulerAngles = PickupRotation;
        }
    }
}