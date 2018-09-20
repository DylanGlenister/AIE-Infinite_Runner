using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuList : MonoBehaviour
{
    bool spamProtec = false;

    public GameObject PlayButton;
    public GameObject OptionsButton;
    public GameObject ExitButton;

    public GameObject currentSelected;
    public Button currentButton;

    private void Start()
    {
        currentSelected = PlayButton;
        currentButton = currentSelected.GetComponent<Button>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if (currentSelected == PlayButton)
                Play();
            else if (currentSelected == OptionsButton)
                Debug.Log("Yes");
            else if (currentSelected == ExitButton)
                Quit();
        }

        currentButton.Select();

        if (spamProtec == false && Input.GetAxis("DpadY") < 0)
        {
            if (currentSelected == PlayButton)
            {
                currentSelected = OptionsButton;
                currentButton = currentSelected.GetComponent<Button>();
            }
            else if (currentSelected == OptionsButton)
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
            else if (currentSelected == OptionsButton)
            {
                currentSelected = PlayButton;
                currentButton = currentSelected.GetComponent<Button>();
            }
            else if (currentSelected == ExitButton)
            {
                currentSelected = OptionsButton;
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
