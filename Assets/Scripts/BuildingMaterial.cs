using UnityEngine;
using UnityEditor;

namespace Assets.Scripts
{
    public class BuildingMaterial : InventoryItem
    {
        public Player Player;
        public readonly int cost = 100;
        public string Message;

        public const int MAX_COUNT = 25;
        public int MaterialCount = 25;

        public BuildingMaterial()
        {
            Message = $"You need {cost} score to buy this";
        }
        public override void OnPickup()
        {
            Debug.Log("in material pickup function");
            if (Player.Score >= cost)
            {
                Player.DecreaseScore(cost);
                MaterialCount = MAX_COUNT;
                Debug.Log("pixked up mats");
            }
        }
        public override void OnUse()
        {
            MaterialCount -= 1;
            Debug.Log("used one");
        }
    }
}