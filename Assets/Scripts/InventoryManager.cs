using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class InventoryManager : MonoBehaviour
    {
        public Inventory inventory;
        public Transform inventoryHUD;

        private static InventoryItem activeItem;

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
                if (inventory.Items[0] != null)
                {
                    activeItem?.OnPutAway();
                    activeItem = inventory.Items[0] as InventoryItem;
                    (inventory.Items[0] as InventoryItem).OnUse();
                }
            }
            else if(Input.GetKey(KeyCode.Alpha2))
            {
                Debug.Log("pressed 2");
                if (inventory.Items[1] != null)
                {
                    Debug.Log("taking out hammer");
                    activeItem?.OnPutAway();
                    activeItem = inventory.Items[1] as InventoryItem;
                    Debug.Log("using hammer");
                    (inventory.Items[1] as InventoryItem).OnUse();
                }
            }
            else if(Input.GetKey(KeyCode.Alpha3))
            {
                if (inventory.Items[2] != null)
                {
                    activeItem.OnPutAway();
                    Debug.Log("Flashlight on");
                }
            }
            else if(Input.GetKey(KeyCode.Alpha4))
            {
                Debug.Log("pressed 4");
                if (inventory.Items[3] != null)
                {

                }
            }
        }
    }
}
