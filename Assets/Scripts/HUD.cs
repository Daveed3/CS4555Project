using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class HUD : MonoBehaviour
    {
        public Inventory inventory;
        public GameObject ItemMessagePanel;

        private void Start()
        {
            inventory.ItemAdded += InventoryScriptItemAdded;
        }

        private void InventoryScriptItemAdded(object sender, InventoryEventArgs e)
        {
            Transform inventoryHUD = transform.Find("InventoryUI").Find("InventoryHUD").Find("Inventory");
            Transform slot;
            Image image;

            if (e.Item.Name.Equals("assault rifle"))
            {
                slot = inventoryHUD.GetChild(0);
            }
            else if (e.Item.Name.Equals("handgun"))
            {
                slot = inventoryHUD.GetChild(1);
            }
            else if (e.Item.Name.Equals("hammer"))
            {
                slot = inventoryHUD.GetChild(2);
            }
            else if (e.Item.Name.Equals("flashlight"))
            {
                slot = inventoryHUD.GetChild(3);
            }
            else
            {
                slot = inventoryHUD.GetChild(4);
            }

            image = slot.GetChild(0).GetChild(0).GetComponent<Image>();
            image.enabled = true;
            image.sprite = e.Item.Image;
        }

        public void OpenMessagePanel(string text)
        {
            ItemMessagePanel.SetActive(true);
        }

        public void CloseMessagePanel()
        {
            ItemMessagePanel.SetActive(false);
        }
    }
}