using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private DeathController dController;

    void Awake()
    {
        dController = FindObjectOfType<DeathController>();
    }

    //Script is put onto every obstacle that is spawned in, it tests if the player has collided into it
    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Player")
            dController.ObstacleCollide();
    }
}
