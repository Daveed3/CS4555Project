using UnityEngine;
using UnityEditor;

public class HUD : MonoBehaviour
{
    public Inventory inventory;
    public GameObject ItemMessagePanel;

    public void OpenMessagePanel(string text)
    {
        ItemMessagePanel.SetActive(true);
    }

    public void CloseMessagePanel()
    {
        ItemMessagePanel.SetActive(false);
    }
}