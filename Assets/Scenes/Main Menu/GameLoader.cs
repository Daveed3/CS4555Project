using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using TMPro;
public class GameLoader : MonoBehaviour
{
    // OnClick method (in hierarchy) plays many of these methods
    GameObject playButton;
    GameObject optionButton;
    GameObject quitButton;
    GameObject creditsButton;
    GameObject leaderboardButton;
    GameObject backButton;
    GameObject credits1;
    GameObject credits2;
    GameObject leaderboard;
    GameObject iField;
    public int[] TopScores;
    public string[] TopScoreNames;
    private int recordedScores;
    TMPro.TextMeshProUGUI textMPLeaderboard;
    TMP_InputField inputField;
    Scene gameScene;
    string playerName;


   public void MainMenuScreen() {
       // Testing names with scores, so deleting on purpose rn. Comment out later
       PlayerPrefs.DeleteAll();

       
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
        backButton = GameObject.Find("BackButton");
        credits1 = GameObject.Find("CreditsL1");
        credits2 = GameObject.Find("CreditsL2");
        leaderboard = GameObject.Find("Leaderboard");
        iField = GameObject.Find("NameInputField");
        Debug.Log(TopScoreNames.Length + " " + TopScores.Length);
        recordedScores = 5;
        TopScores = new int[recordedScores];
        TopScoreNames = new string[recordedScores];
        for (int i=0; i<recordedScores; i++) {
            TopScores[i] = PlayerPrefs.GetInt("score" + i, 0);
            TopScoreNames[i] = PlayerPrefs.GetString("name"+i, "N/A");
        }
        textMPLeaderboard = GameObject.Find("LeaderboardScores").GetComponent<TMPro.TextMeshProUGUI>();
        inputField = GameObject.Find("NameInputField").GetComponent<TMPro.TMP_InputField>();
        string currName = inputField.text;
        playerName = currName;
        // if (currName == "") {
        //     playerName = currName;
        // } else {
        //     playerName = PlayerPrefs.GetString("playerName");
        // }



        MainMenuVars(true);
        CreditsVars(false);
        LeaderboardVars(false);

   }

   public void LoadGame()
   {
       PlayerPrefs.SetString("playerName", playerName);
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }

   public void LoadCredits() {
        MainMenuVars(false);
        CreditsVars(true);
        LeaderboardVars(false);
   }

   public void LoadLeaderboard() {
        MainMenuVars(false);
        CreditsVars(false);
        LeaderboardVars(true);
   }
   public void LoadMainMenu() {
        MainMenuVars(true);
        CreditsVars(false);
        LeaderboardVars(false);
   }

   public void QuitGame()
   {
       Application.Quit();
       Debug.Log("Quit Game!");
   }
    public void MainMenuVars(bool isOn) {
       playButton.SetActive(isOn);
       quitButton.SetActive(isOn);
       optionButton.SetActive(isOn);
       creditsButton.SetActive(isOn);
       leaderboardButton.SetActive(isOn);
       backButton.SetActive(!isOn);
       iField.SetActive(isOn);
   } 
    public void CreditsVars(bool isOn) {
        credits1.SetActive(isOn);
        credits2.SetActive(isOn);
   }

   public void LeaderboardVars(bool isOn) {
       leaderboard.SetActive(isOn);
       // Get current score
       // Have to use score = PlayerPrefs.GetFloat("score") to get the score
       int score = PlayerPrefs.GetInt("score", 0);
       string playerName = PlayerPrefs.GetString("playerName", "Player");
       
       // Compare scores
       for (int i=0; i<recordedScores; i++) {
           if (score > TopScores[i]) {
               int temp = TopScores[i];
               string tempName = TopScoreNames[i];

               TopScores[i] = score;
               TopScoreNames[i] = playerName;

               score = temp;
               playerName = tempName;
           }
       }

        // Set text to scores of the players
       textMPLeaderboard.text = getScores();

       
        // Save scores
        for (int i=0; i<recordedScores; i++) {
            PlayerPrefs.SetInt("score"+i, TopScores[i]);
            PlayerPrefs.SetString("name"+i, TopScoreNames[i]);
        }

   }
   private string getScores() {
       string scores = "";

        // Return all the recorded scores as a string for leaderboard
        for (int i=0; i<recordedScores; i++) {
            scores += $"\n\t\t{TopScoreNames[i]}\t\t\t{TopScores[i]}";
        }

       return scores;
   }

   public void resetScores() {
       PlayerPrefs.DeleteKey("score");
   }


   public void Awake() {
       MainMenuScreen();
   }

}
