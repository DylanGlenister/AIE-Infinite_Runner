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
            background.SetActive(true);
            DeathMessage.SetActive(true);
            MenuItems.SetActive(true);
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
