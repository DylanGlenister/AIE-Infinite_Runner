using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    bool spamProtec = false;

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
    public Button currentButton;

    private void Awake()
    {
        // Finds the scripts
        dController = FindObjectOfType<DeathController>();

        currentSelected = RetryButton;
        currentButton = currentSelected.GetComponent<Button>();
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

            //currentButton.Select();

            if (spamProtec == false && Input.GetAxis("DpadX") > 0)
            {
                if (currentSelected == RetryButton)
                {
                    currentSelected = MenuButton;
                    currentButton = currentSelected.GetComponent<Button>();
                }
                else if (currentSelected == MenuButton)
                {
                    currentSelected = ExitButton;
                    currentButton = currentSelected.GetComponent<Button>();
                }
                else if (currentSelected == ExitButton)
                {
                    currentSelected = RetryButton;
                    currentButton = currentSelected.GetComponent<Button>();
                }
                spamProtec = true;
            }
            else if (spamProtec == false && Input.GetAxis("DpadX") < 0)
            {
                if (currentSelected == RetryButton)
                {
                    currentSelected = ExitButton;
                    currentButton = currentSelected.GetComponent<Button>();
                }
                else if (currentSelected == MenuButton)
                {
                    currentSelected = RetryButton;
                    currentButton = currentSelected.GetComponent<Button>();
                }
                else if (currentSelected == ExitButton)
                {
                    currentSelected = MenuButton;
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
