using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class GameLoader : MonoBehaviour
{
    // OnClick method (in hierarchy) plays many of these methods
    GameObject playButton;
    GameObject optionButton;
    GameObject quitButton;
    GameObject creditsButton;
    GameObject leaderboardButton;
    GameObject credits1;
    GameObject credits2;
    GameObject leaderboard;


   public void MainMenuScreen() {
        // Want to do a for each loop, but something isn't working
        // GameObject[] buttons = {playButton, optionButton, quitButton, creditsButton, leaderboardButton};
        // int index = 0;
        // foreach(GameObject button in GameObject.Find("Background").transform) {
        //     buttons[index] = button;
        //     index++;
        //     Debug.Log(buttons[index]);
        // }
        playButton = GameObject.Find("PlayButton");
        quitButton = GameObject.Find("QuitButton");
        optionButton = GameObject.Find("OptionsButton");
        creditsButton = GameObject.Find("CreditsButton");
        leaderboardButton = GameObject.Find("LeaderboardButton");
        credits1 = GameObject.Find("CreditsL1");
        credits2 = GameObject.Find("CreditsL2");
        leaderboard = GameObject.Find("Leaderboard");

        MainMenuVars(true);
        CreditsVars(false);
        LeaderboardVars(false);

   }
   public void MainMenuVars(bool isOn) {
       playButton.SetActive(isOn);
       quitButton.SetActive(isOn);
       optionButton.SetActive(isOn);
       creditsButton.SetActive(isOn);
       leaderboardButton.SetActive(isOn);

   } 
    public void CreditsVars(bool isOn) {
        credits1.SetActive(isOn);
        credits2.SetActive(isOn);
   }

   public void LeaderboardVars(bool isOn) {
       leaderboard.SetActive(isOn);
       // Have to use score = PlayerPrefs.GetFloat("score") to get the score
       float score = PlayerPrefs.GetFloat("score");
   }

   public void LoadGame()
   {
       SceneManager.LoadScene("Main Scene");
   }

   public void QuitGame()
   {
       Application.Quit();
       Debug.Log("Quit Game!");
   }


   public void Awake() {
       MainMenuScreen();
   }

}
