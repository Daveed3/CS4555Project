using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class InventoryManager : MonoBehaviour
    {
        public Transform inventoryHUD;

        public static InventoryItem activeItem;

        // Use this for initialization
        void Start()
        {         
            
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKey(KeyCode.Alpha1))
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
            else if(Input.GetKey(KeyCode.Alpha2))
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
            else if (Input.GetKey(KeyCode.Alpha3))
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
            else if(Input.GetKey(KeyCode.Alpha4))
            {
                Debug.Log("pressed 4");

                if (Inventory.Items[3] != null)
                {
                    (Inventory.Items[3] as InventoryItem).OnUse();
                }
            }
            else if(Input.GetKey(KeyCode.Alpha5))
            {
                Debug.Log("pressed 5");
                if (Inventory.Items[4] != null)
                {

                }
            }
        }
    }
}