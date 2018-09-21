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

    public TextMeshProUGUI scoreText;
    
    private void Awake()
    {
        // Finds the scripts
        dController = FindObjectOfType<DeathController>();
        sController = FindObjectOfType<ScoreController>();
        hs = FindObjectOfType<Highscores>();
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

            if (spamProtec == false && Input.GetAxis("DpadX") > 0)
            {
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
            {
                spamProtec = false;
            }

            if (Input.GetButtonDown("Submit"))
            {
                if (currentSelected == retryButton)
                    RetryButtonClick();
                else if (currentSelected == menuButton)
                    MenuButtonClick();
                else if (currentSelected == exitButton)
                    QuitButtonClick();
            }
        }
        else
        {
            scoreText.text = sController.Score.ToString(); 
        }
    }

    public void RetryButtonClick()
    {
        hs.AddScore(sController.Score);
        hs.SaveScoresToFile();
        // Reloads the current scene to restart the level
        SceneManager.LoadScene(1);
    }

    public void MenuButtonClick()
    {
        hs.AddScore(sController.Score);
        hs.SaveScoresToFile();
        // Loads the menu scene
        SceneManager.LoadScene(0);
    }

    public void QuitButtonClick()
    {
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