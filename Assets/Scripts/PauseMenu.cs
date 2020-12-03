using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GameIsPaused = false;
    public GameObject pauseMenu;

    void Start()
    {
        // on start of the game the pause menu is deactivated
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // This event listener will determine whether the user pause or resumes the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Debug.Log("Recived Input, Game Paused is False");
                GameIsPaused = false;
                ResumeGame();

            }
            else
            {
                Debug.Log("Recived Input, Game Paused is True");
                GameIsPaused = true;
                PauseGame();
            }
        }
    }

    // This function pauses the game and activates the Pause Menu UI, and unlocks the Cursor so the user is able to 
    // interact with the Pause UI 

    public void PauseGame()
    {
        Debug.Log("Paused Game");
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        // Cursor.visible = true;
    }

    // This function resumes the game and deactivates the Pause Menu UI, and locks the Cursor to the center of the game window
    public void ResumeGame()
    {
        Debug.Log("Resume Game");
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false; 
    }

    // This function will take the user back to the Main Menu Scene 
    public void LoadMenu()
    {
        Debug.Log("Loading Main Menu");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    // This function will exit the application 
    public void QuitGame()
    {
        Debug.Log("Quiting Game!");
        Application.Quit();
    }
}

// I tried my best to comment my code - <3 Peter  