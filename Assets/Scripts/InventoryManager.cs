using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class InventoryManager : MonoBehaviour
    {
        public Transform inventoryHUD;

        public static InventoryItem activeItem;
        public AudioSource lowAmmoRemark;
        public AudioSource outOfAmmoRemark;
        public bool handgunLowAmmoRemarkHasPlayed = false;
        public bool assaultRifleLowAmmoRemarkHasPlayed = false;
        public bool handgunOutOfAmmoRemarkHasPlayed = false;
        public bool assaultRifleOutOfAmmoRemarkHasPlayed = false;        
        public Text hangunAmmunition;
        public Text assaultRifleAmmunition;
        public Text buildingMaterialCount;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            MonitorAmmunition();
            UpdateInventoryUI();

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("pressed 1");
                if (Inventory.Items[0] != null)
                {
                    if (activeItem?.Equals(Inventory.Items[0] as InventoryItem) == true)
                    {
                        activeItem?.OnPutAway();
                        activeItem = null;
                    }
                    else
                    {
                        activeItem?.OnPutAway();
                        activeItem = Inventory.Items[0] as InventoryItem;
                        (Inventory.Items[0] as InventoryItem).OnUse();
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("pressed 2");
                if (Inventory.Items[1] != null)
                {
                    if (activeItem?.Equals(Inventory.Items[1] as InventoryItem) == true)
                    {
                        activeItem?.OnPutAway();
                        activeItem = null;
                    }
                    else
                    {
                        activeItem?.OnPutAway();
                        activeItem = Inventory.Items[1] as InventoryItem;
                        (Inventory.Items[1] as InventoryItem).OnUse();
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Debug.Log("pressed 3");
                if (Inventory.Items[2] != null)
                {
                    if (activeItem?.Equals(Inventory.Items[2] as InventoryItem) == true)
                    {
                        activeItem?.OnPutAway();
                        activeItem = null;
                    }
                    else
                    {
                        activeItem?.OnPutAway();
                        activeItem = Inventory.Items[2] as InventoryItem;
                        (Inventory.Items[2] as InventoryItem).OnUse();
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Debug.Log("pressed 4");

                if (Inventory.Items[3] != null)
                {
                    (Inventory.Items[3] as InventoryItem).OnUse();
                }
            }
        }
        private void UpdateInventoryUI()
        {
            if (Inventory.Items[0] != null)
            {
                AssaultRifle assaultRifle = Inventory.Items[0] as AssaultRifle;
                assaultRifleAmmunition.text = $"{assaultRifle.AmmunitionCount}/{AssaultRifle.MAX_COUNT}";
            }

            if (Inventory.Items[1] != null)
            {
                Handgun handgun = Inventory.Items[1] as Handgun;
                hangunAmmunition.text = $"{handgun.AmmunitionCount}/{Handgun.MAX_COUNT}";
            }

            if (Inventory.Items[4] != null)
            {
                BuildingMaterial buildingMaterial = Inventory.Items[4] as BuildingMaterial;
                buildingMaterialCount.text = $"{buildingMaterial.MaterialCount}/{BuildingMaterial.MAX_COUNT}";
            }
        }
        
        private void MonitorAmmunition()
        {
            if (activeItem != null)
            {
                if (activeItem.ItemName.Equals("handgun"))
                {
                    Handgun weapon = activeItem as Handgun;

                    if (weapon.AmmunitionCount < Handgun.MAX_COUNT * 0.30)
                    {
                        if (!handgunLowAmmoRemarkHasPlayed)
                        {
                            lowAmmoRemark.Play();
                            handgunLowAmmoRemarkHasPlayed = true;
                        }
                    }
                    else
                    {
                        handgunLowAmmoRemarkHasPlayed = false;
                    }

                    if (weapon.AmmunitionCount == 0)
                    {
                        if (!handgunOutOfAmmoRemarkHasPlayed)
                        {
                            lowAmmoRemark.Stop();
                            outOfAmmoRemark.Play();
                            handgunOutOfAmmoRemarkHasPlayed = true;
                        }
                    }
                    else
                    {
                        handgunOutOfAmmoRemarkHasPlayed = false;
                    }
                }
                else if (activeItem.ItemName.Equals("assault rifle"))
                {
                    AssaultRifle weapon = activeItem as AssaultRifle;

                    if (weapon.AmmunitionCount < AssaultRifle.MAX_COUNT * 0.30)
                    {
                        if (!assaultRifleLowAmmoRemarkHasPlayed)
                        {
                            lowAmmoRemark.Play();
                            assaultRifleLowAmmoRemarkHasPlayed = true;
                        }
                    }
                    else
                    {
                        assaultRifleLowAmmoRemarkHasPlayed = false;
                    }

                    if (weapon.AmmunitionCount == 0)
                    {
                        if (!assaultRifleOutOfAmmoRemarkHasPlayed)
                        {
                            lowAmmoRemark.Stop();
                            outOfAmmoRemark.Play();
                            assaultRifleOutOfAmmoRemarkHasPlayed = true;
                        }
                    }
                    else
                    {
                        assaultRifleOutOfAmmoRemarkHasPlayed = false;
                    }
                }
            }
        }
    }
}