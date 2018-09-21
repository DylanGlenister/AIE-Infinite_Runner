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
    private float scoreDelay;

    private void Awake()
    {
        // Finds the scripts
        dController = FindObjectOfType<DeathController>();

        scoreDelay = scoreTimer;
    }

    private void FixedUpdate()
    {
        // Increases the score as along as the player hasnt died
        if (!dController.hasDied && scoreDelay <= 0)
        {
            scoreDelay = scoreTimer;
            Score++;
        }
        else
        {
            scoreDelay -= Time.deltaTime;
        }
    }
}
