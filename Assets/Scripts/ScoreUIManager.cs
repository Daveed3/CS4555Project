using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ScoreUIManager : MonoBehaviour
    {
        public Text RoundText;
        public Text ScoreText;

        void Update()
        {
            RoundText.text = $"ROUND {RoundManager.Round}";
            ScoreText.text = $"score {Player.Score}";
        }
    }
}