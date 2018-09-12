using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public DeathController dController;

    public int Score;

    public float scoreTimer;

    private void Awake()
    {
        dController = FindObjectOfType<DeathController>();
    }

    private void FixedUpdate()
    {
        if (!dController.hasDied)
        {
            scoreTimer = 0;
            Score++;
        }
    }
}
