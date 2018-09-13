using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    // References to other scripts
    public PlayerController pController;
    public LevelController lController;

    // Bools used to manage the game
    private bool pause;
    public bool hasDied;

    private void Awake()
    {
        // Finds the scripts
        lController = FindObjectOfType<LevelController>();
        pController = FindObjectOfType<PlayerController>();

        // Reinitialises variables
        pause = false;
        hasDied = false;
    }

    // Is called to check if the player has died
    public bool HasPlayerCollided()
    {
        return pause;
    }

    // Called when you collide
    public void ObstacleCollide()
    {
        pause = true;
    }

    // Updates the player collide function when the player has collided
    private void Update()
    {
        if (HasPlayerCollided())
            hasDied = true;
    }
}
