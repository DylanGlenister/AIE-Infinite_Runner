using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    // References to other scripts
    public DeathController dController;

    // Storage for references to UI elements
    public GameObject background;
    public GameObject DeathMessage;
    public GameObject MenuItems;

    public GameObject RetryButton;
    public GameObject MenuButton;
    public GameObject ExitButton;

    public GameObject currentSelected;

    private void Awake()
    {
        // Finds the scripts
        dController = FindObjectOfType<DeathController>();
    }

    private void Update()
    {
        // Enables all ui elements
        if (dController.hasDied)
        {
            // This will spam - Fix it
            background.SetActive(true);
            DeathMessage.SetActive(true);
            MenuItems.SetActive(true);

            if (Input.GetAxis("DpadX") < 1)
            {
                if (currentSelected == RetryButton)
                    currentSelected = MenuButton;
                else if (currentSelected == MenuButton)
                    currentSelected = ExitButton;
                else if (currentSelected == ExitButton)
                    currentSelected = RetryButton;
            }
            else if (Input.GetAxis("DpadX") > 1)
            {
                if (currentSelected == RetryButton)
                    currentSelected = ExitButton;
                else if (currentSelected == MenuButton)
                    currentSelected = RetryButton;
                else if (currentSelected == ExitButton)
                    currentSelected = MenuButton;
            }

            if (Input.GetButtonDown("Submit"))
            {
                if (currentSelected == RetryButton)
                    RetryButtonClick();
                else if (currentSelected == MenuButton)
                    MenuButtonClick();
                else if (currentSelected == ExitButton)
                    QuitButtonClick();
            }
        }
    }

    public void RetryButtonClick()
    {
        // Reloads the current scene to restart the level
        SceneManager.LoadScene(1);
    }

    public void MenuButtonClick()
    {
        // Loads the menu scene
        SceneManager.LoadScene(0);
    }


    public void QuitButtonClick()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

}
