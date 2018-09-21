using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuList : MonoBehaviour
{
    // It protec from spam
    bool spamProtec = false;

    // Reference to the highscore script
    public Highscores hs;

    // References to all the required UI elements
    public GameObject menuList;
    public GameObject PlayButton;
    public GameObject LeaderboardButton;
    public GameObject ExitButton;
    public GameObject LeaderboardScreen;
    public GameObject backButton;

    // References to all the requires text boxes for highscores
    public TextMeshProUGUI firstScoreText;
    public TextMeshProUGUI secondScoreText;
    public TextMeshProUGUI thirdScoreText;
    public TextMeshProUGUI forthScoreText;
    public TextMeshProUGUI fifthScoreText;
    
    // The currently selected options (for PS4)
    public GameObject currentSelected;
    public Button currentButton;

    private void Awake()
    {
        // Finds the script
        hs = FindObjectOfType<Highscores>();
        // Initialises the currently selected options (for PS4)
        currentSelected = PlayButton;
        currentButton = currentSelected.GetComponent<Button>();
    }

    private void Update()
    {
        // Activates the currently selected button (for PS4)
        if (Input.GetButtonDown("Submit"))
        {
            if (currentSelected == PlayButton)
            {
                Play();
            }
            else if (currentSelected == LeaderboardButton)
            {
                // Opens the highscores screen
                Highscores();
                currentSelected = backButton;
            }
            else if (currentSelected == backButton)
            {
                // Closes the highscores screen
                currentSelected = LeaderboardButton;
                LeaderboardScreen.SetActive(false);
                menuList.SetActive(true);
            }
            else if (currentSelected == ExitButton)
            {
                Quit();
            }
        }

        // Highlights the currently selected button (For PS4)
        //currentButton.Select();

        // The horizontal axis for the Dpad ( <0 is left and >0 is right
        if (spamProtec == false && Input.GetAxis("DpadY") < 0)
        {
            // Changes the currently selected based on the previously selected
            // This is probably not the best way to do this
            if (currentSelected == PlayButton)
            {
                currentSelected = LeaderboardButton;
                currentButton = currentSelected.GetComponent<Button>();
            }
            else if (currentSelected == LeaderboardButton)
            {
                currentSelected = ExitButton;
                currentButton = currentSelected.GetComponent<Button>();
            }
            else if (currentSelected == ExitButton)
            {
                currentSelected = PlayButton;
                currentButton = currentSelected.GetComponent<Button>();
            }
            spamProtec = true;
        }
        else if (spamProtec == false && Input.GetAxis("DpadY") > 0)
        {
            if (currentSelected == PlayButton)
            {
                currentSelected = ExitButton;
                currentButton = currentSelected.GetComponent<Button>();
            }
            else if (currentSelected == LeaderboardButton)
            {
                currentSelected = PlayButton;
                currentButton = currentSelected.GetComponent<Button>();
            }
            else if (currentSelected == ExitButton)
            {
                currentSelected = LeaderboardButton;
                currentButton = currentSelected.GetComponent<Button>();
            }
            spamProtec = true;
        }
        else if (Input.GetAxis("DpadY") < 0.1f && Input.GetAxis("DpadY") > -0.1f)
        {           // This resets the spamProtec bool when neither left or right is being pressed
            spamProtec = false;
        }
    }

    public void Play()
    {
        // Loads the game scene
        SceneManager.LoadScene(1);
    }

    public void Highscores()
    {
        // Displays the highscores screen and loads the scores from the array
        LeaderboardScreen.SetActive(true);
        menuList.SetActive(false);
        hs.LoadScoresFromFile();
        firstScoreText.text = hs.scoreArray[0].ToString();
        secondScoreText.text = hs.scoreArray[1].ToString();
        thirdScoreText.text = hs.scoreArray[2].ToString();
        forthScoreText.text = hs.scoreArray[3].ToString();
        fifthScoreText.text = hs.scoreArray[4].ToString();
    }

    public void Quit()
    {
        // Quits the application if exe file
    #if UNITY_STANDALONE
        Application.Quit();
    #endif

        // Stops the editor playing if in unity
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }
}
