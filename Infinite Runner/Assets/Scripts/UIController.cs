using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class UIController : MonoBehaviour {

    public DeathController dController;

    public GameObject Panel;
    public GameObject QuitBtntxt;
    public GameObject MenuList;

    private void Awake()
    {
        dController = FindObjectOfType<DeathController>();
    }

    private void Update()
    {
        if (dController.hasDied) {
            Panel.SetActive(true);
            QuitBtntxt.SetActive(true);
            MenuList.SetActive(true);
        }
    }

    public void RetryButtonClick() {
        SceneManager.LoadScene(1);
    }

    public void MenuButtonClick()
    {
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
