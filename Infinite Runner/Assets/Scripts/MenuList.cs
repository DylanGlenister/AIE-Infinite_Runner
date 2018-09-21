using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuList : MonoBehaviour
{
    bool spamProtec = false;

    public Highscores hs;

    public GameObject PlayButton;
    public GameObject LeaderboardButton;
    public GameObject ExitButton;
    public GameObject LeaderboardScreen;

    public TextMeshProUGUI firstScoreText;
    public TextMeshProUGUI secondScoreText;
    public TextMeshProUGUI thirdScoreText;
    public TextMeshProUGUI forthScoreText;
    public TextMeshProUGUI fifthScoreText;

    public GameObject currentSelected;
    public Button currentButton;

    private void Awake()
    {
        currentSelected = PlayButton;
        currentButton = currentSelected.GetComponent<Button>();
        hs = FindObjectOfType<Highscores>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if (currentSelected == PlayButton)
                Play();
            else if (currentSelected == LeaderboardButton)
                Debug.Log("Yes");
            else if (currentSelected == ExitButton)
                Quit();
        }

        //currentButton.Select();

        if (spamProtec == false && Input.GetAxis("DpadY") < 0)
        {
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
        {
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
        LeaderboardScreen.SetActive(true);
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
