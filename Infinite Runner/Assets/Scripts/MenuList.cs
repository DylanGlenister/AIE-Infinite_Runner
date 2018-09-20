using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuList : MonoBehaviour
{
    public GameObject PlayButton;
    public GameObject OptionsButton;
    public GameObject ExitButton;

    public GameObject currentSelected;

    private void Start()
    {
        currentSelected = PlayButton;
    }

    private void Update()
    {
        if (Input.GetAxis("DpadY") < 1)
        {
            if (currentSelected == PlayButton)
                currentSelected = OptionsButton;
            else if (currentSelected == OptionsButton)
                currentSelected = ExitButton;
            else if (currentSelected == ExitButton)
                currentSelected = PlayButton;
        }
        else if (Input.GetAxis("DpadY") > 1)
        {
            if (currentSelected == PlayButton)
                currentSelected = ExitButton;
            else if (currentSelected == OptionsButton)
                currentSelected = PlayButton;
            else if (currentSelected == ExitButton)
                currentSelected = OptionsButton;
        }

        if (Input.GetButtonDown("Submit"))
        {
            if (currentSelected == PlayButton)
                Play();
            else if (currentSelected == OptionsButton)
                Debug.Log("Yes");
            else if (currentSelected == ExitButton)
                Quit();
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
