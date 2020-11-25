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
            RoundText.text = RoundManager.Round == 0 ? $"The game will start in {RoundManager.SecondsToStart} seconds..." : $"ROUND {RoundManager.Round}";
            ScoreText.text = $"score {Player.Score}";

            if(Player.IsDead)
            {
                GameObject.Find("Main Camera").GetComponent<MouseLook>().enabled = false;
                GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
                GameObject.Find("Spot Light").GetComponent<MouseLook>().enabled = false;
                foreach (Transform child in GameObject.Find("Enemy").transform) {
                    if (child.gameObject.tag == "Enemy") {
                        child.transform.position = new Vector3(0,0,0);
                    }
                }

                CrossHairCanvas.enabled = false;
                InventoryCanvas.enabled = false;
                RoundText.enabled = false;
                GameOverUI.SetActive(true);
                GameOverText.text = $"Game Over!\nYou survived {RoundManager.Round} rounds!\nFinal Score: {Player.Score}";
            }
        }
    }
}