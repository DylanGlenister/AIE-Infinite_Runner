using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    //References to other scripts
    public LevelController lController;
    public PlayerController pController;

    private bool pause = false;
    public bool hasDied;

    private void Awake()
    {
        //Finds the scripts
        lController = FindObjectOfType<LevelController>();
        pController = FindObjectOfType<PlayerController>();
        hasDied = false;
    }

    //Is called to check if the player has died
    public bool HasPlayerCollided()
    {
        return pause;
    }

    //Called when you collide
    public void ObstacleCollide()
    {
        pause = true;
        hasDied = true;
    }
}
