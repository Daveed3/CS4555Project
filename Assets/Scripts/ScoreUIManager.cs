using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ScoreUIManager : MonoBehaviour
    {
        public Text RoundText;
        public Text ScoreText;
        public Text GameOverText;
        public GameObject GameOverUI;
        public Canvas CrossHairCanvas;
        public Canvas InventoryCanvas;

        void Update()
        {
            RoundText.text = RoundManager.Round == 0 ? "The game will start in 30 seconds..." : $"ROUND {RoundManager.Round}";
            ScoreText.text = $"score {Player.Score}";

            if(Player.IsDead)
            {
                CrossHairCanvas.enabled = false;
                InventoryCanvas.enabled = false;
                RoundText.enabled = false;
                GameOverUI.active = true;
                GameOverText.text = $"Game Over! \n You survived {RoundManager.Round} rounds!";
            }
        }
    }
}