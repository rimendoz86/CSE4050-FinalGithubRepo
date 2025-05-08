using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class sceneController : MonoBehaviour
{
    public GameObject WinPanel;
    
    public GameObject LosePanel;

    public TMP_Text LoseReasonText; // Reference to UI Text element for reason

    public TMP_Text WinReasonText; // Reference to UI Text element for win reason

    // adding sound
    [SerializeField] private AudioClip victorySound;

    private void Start()
    {
        // Make sure panels are hidden at the start
        if (WinPanel != null) 
        {
            WinPanel.SetActive(false);
        }
        if (LosePanel != null)
        { 
            LosePanel.SetActive(false);
        }

        if (WinReasonText != null) WinReasonText.text = "";
        
        if (LoseReasonText != null) LoseReasonText.text = ""; // Clear on start

    }

    public void ShowLosePanel(string reason = "")
    {
        // stopping bg music when player loses
        soundManager.instance?.StopMusic();
        if (LosePanel != null)
        {
            LosePanel.SetActive(true);
            
            if (LoseReasonText != null)
            {
                LoseReasonText.text = reason;
            }

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
    }

    public void ShowWinPanel(string reason = "")
    {
        // plays victory sound then stops it
        SoundFXManager.instance.PlaySoundFXClip(victorySound, transform, 1f); 
        soundManager.instance?.StopMusic();
        if (WinPanel != null)
        {
            WinPanel.SetActive(true);
            if (WinReasonText != null)
            {
              WinReasonText.text = reason;
            }
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0; // optional: pause game
        }
    }
    public void RetryLevel()
    {
        Time.timeScale = 1; // Unpause the game
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StaticData.HealthData = 100;
        soundManager.instance?.PlayMusic(); // restarts background music
        SceneManager.LoadScene(1); // Reload current scene
    }
    public void LoadMainMenu()
    {
        Time.timeScale = 1; // Reset time scale to normal in case game was paused
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu"); // Replace "MainMenu" with your actual scene name if different
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
