using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    bool spamProtec = false;

    // References to other scripts
    public DeathController dController;
    public ScoreController sController;
    public Highscores hs;

    // Storage for references to UI elements
    public GameObject background;
    public GameObject deathMessage;
    public GameObject menuItems;
    public GameObject retryButton;
    public GameObject menuButton;
    public GameObject exitButton;
    
    public GameObject currentSelected;
    public Button currentButton;

    // Reference to the textbox that displays the score
    public TextMeshProUGUI scoreText;
    
    private void Awake()
    {
        // Finds the scripts
        dController = FindObjectOfType<DeathController>();
        sController = FindObjectOfType<ScoreController>();
        hs = FindObjectOfType<Highscores>();
        // Initialises the currently selected options (for PS4)
        currentSelected = retryButton;
        currentButton = currentSelected.GetComponent<Button>();
    }

    private void Update()
    {

        // Enables all ui elements
        if (dController.hasDied)
        {
            // This will spam - Fix it
            background.SetActive(true);
            deathMessage.SetActive(true);
            menuItems.SetActive(true);

            //currentButton.Select();

            // Activates the currently selected button (for PS4)
            if (Input.GetButtonDown("Submit"))
            {
                if (currentSelected == retryButton)
                    RetryButtonClick();
                else if (currentSelected == menuButton)
                    MenuButtonClick();
                else if (currentSelected == exitButton)
                    QuitButtonClick();
            }

            // The horizontal axis for the Dpad ( <0 is left and >0 is right
            if (spamProtec == false && Input.GetAxis("DpadX") > 0)
            {
                // Changes the currently selected based on the previously selected
                // This is probably not the best way to do this
                if (currentSelected == retryButton)
                {
                    currentSelected = menuButton;
                    currentButton = currentSelected.GetComponent<Button>();
                }
                else if (currentSelected == menuButton)
                {
                    currentSelected = exitButton;
                    currentButton = currentSelected.GetComponent<Button>();
                }
                else if (currentSelected == exitButton)
                {
                    currentSelected = retryButton;
                    currentButton = currentSelected.GetComponent<Button>();
                }
                spamProtec = true;
            }
            else if (spamProtec == false && Input.GetAxis("DpadX") < 0)
            {
                if (currentSelected == retryButton)
                {
                    currentSelected = exitButton;
                    currentButton = currentSelected.GetComponent<Button>();
                }
                else if (currentSelected == menuButton)
                {
                    currentSelected = retryButton;
                    currentButton = currentSelected.GetComponent<Button>();
                }
                else if (currentSelected == exitButton)
                {
                    currentSelected = menuButton;
                    currentButton = currentSelected.GetComponent<Button>();
                }
                spamProtec = true;
            }
            else if (Input.GetAxis("DpadX") < 0.1f && Input.GetAxis("DpadX") > -0.1f)
            {           // This resets the spamProtec bool when neither left or right is being pressed
                spamProtec = false;
            }

        }
        else
        {
            // Updates the score text
            scoreText.text = sController.Score.ToString(); 
        }
    }

    public void RetryButtonClick()
    {
        // Saves the score and reloads the current scene to restart the level
        hs.AddScore(sController.Score);
        hs.SaveScoresToFile();
        SceneManager.LoadScene(1);
    }

    public void MenuButtonClick()
    {
        // Saves the score and loads the menu scene
        hs.AddScore(sController.Score);
        hs.SaveScoresToFile();
        SceneManager.LoadScene(0);
    }

    public void QuitButtonClick()
    {
        // Saves the score and exists the program (doesnt work on PS4 lol)
        hs.AddScore(sController.Score);
        hs.SaveScoresToFile();

#if UNITY_STANDALONE
        Application.Quit();
#endif

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}