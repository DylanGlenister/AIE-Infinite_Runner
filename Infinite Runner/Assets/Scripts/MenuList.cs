using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuList : MonoBehaviour
{
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
