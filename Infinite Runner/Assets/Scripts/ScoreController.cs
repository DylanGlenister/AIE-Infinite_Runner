using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    // References to other scripts
    public DeathController dController;

    // Variables used for score keeping
    public int Score;
    public float scoreTimer;

    private void Awake()
    {
        // Finds the scripts
        dController = FindObjectOfType<DeathController>();
    }

    private void FixedUpdate()
    {
        // Increases the score as along as the player hasnt died
        if (!dController.hasDied)
        {
            scoreTimer = 0;
            Score++;
        }
    }
}
