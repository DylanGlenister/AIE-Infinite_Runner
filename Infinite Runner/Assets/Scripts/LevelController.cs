using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public PlayerController pController;
    public DeathController dController;
    public ScoreController sController;

    //Randomly generated number used for platform spawns
    private int randomNo;

    //Difficulty settings
    private int diffiulty = 0;

    //Playform movement speed
    public float platformMoveSpeed = 100.0f;

    //Reference to the platform prefabs
    public GameObject platformEmpty;
    public GameObject platformTest;
    public GameObject platformD01;
    public GameObject platformD02;
    public GameObject platformD03;
    public GameObject platformD04;
    public GameObject platformD05;
    public GameObject platformD06;
    public GameObject platformD07;
    public GameObject platformD08;
    public GameObject platformD09;
    public GameObject platformD010;


    //A collection of lists used to manage the platforms
    List<GameObject> platforms = new List<GameObject>();
    List<GameObject> newPlatforms = new List<GameObject>();
    List<GameObject> deletedPlatforms = new List<GameObject>();

    private void Awake()
    {
        //Adds the starting platforms to the platform list
        platforms.AddRange(GameObject.FindGameObjectsWithTag("LevelStart"));

        pController = FindObjectOfType<PlayerController>();
        dController = FindObjectOfType<DeathController>();
        sController = FindObjectOfType<ScoreController>();
    }

    private void FixedUpdate()
    {
        //Controls difficulty based on score (distance travelled)
        if (sController.Score < 200)
        {
            if (diffiulty != 0)
                diffiulty = 0;
        }
        else if (sController.Score >= 200 && sController.Score < 600)
        {
            if (diffiulty != 1)
                diffiulty = 1;
        }
        else if (sController.Score >= 600 && sController.Score < 1000)
        {
            if (diffiulty != 2)
                diffiulty = 2;
        }
        else
        {
            if (diffiulty != 3)
                diffiulty = 3;
        }

        //Resets the newPlatforms and deletedPlatforms list
        newPlatforms.Clear();
        deletedPlatforms.Clear();

        //For each platform in the platforms list we check if the position is out of player sight and spawns a new platform at the end of the line, 
        //then deletes the current platform and adds it to the deletedPlatforms list
        foreach (GameObject platform in platforms)
        {
            if (platform.transform.position.z < -100)
            {
                //Randomizes the platforms then accounts for difficulty
                randomNo = Random.Range(0, 15);
                //Debug.Log(randomNo);
                if (randomNo >= 0 && randomNo < 6)
                {
                    GameObject newPlatform = Instantiate(platformEmpty, new Vector3(0, -0.5f, platform.transform.position.z + 1200.0f), Quaternion.identity);
                    newPlatforms.Add(newPlatform);
                }
                else if (randomNo >= 6 && randomNo < 7)
                {
                    GameObject newPlatform = Instantiate(platformD01, new Vector3(0, -0.5f, platform.transform.position.z + 1200.0f), Quaternion.identity);
                    newPlatforms.Add(newPlatform);
                }
                else if (randomNo >= 7 && randomNo < 8)
                {
                    GameObject newPlatform = Instantiate(platformD02, new Vector3(0, -0.5f, platform.transform.position.z + 1200.0f), Quaternion.identity);
                    newPlatforms.Add(newPlatform);
                }
                else if (randomNo >= 8 && randomNo < 9)
                {
                    GameObject newPlatform = Instantiate(platformD03, new Vector3(0, -0.5f, platform.transform.position.z + 1200.0f), Quaternion.identity);
                    newPlatforms.Add(newPlatform);
                }
                else if (randomNo >= 9 && randomNo < 10)
                {
                    GameObject newPlatform = Instantiate(platformD04, new Vector3(0, -0.5f, platform.transform.position.z + 1200.0f), Quaternion.identity);
                    newPlatforms.Add(newPlatform);
                }
                else if (randomNo >= 10 && randomNo < 11)
                {
                    GameObject newPlatform = Instantiate(platformD05, new Vector3(0, -0.5f, platform.transform.position.z + 1200.0f), Quaternion.identity);
                    newPlatforms.Add(newPlatform);
                }
                else if (randomNo >= 11 && randomNo < 12)
                {
                    GameObject newPlatform = Instantiate(platformD06, new Vector3(0, -0.5f, platform.transform.position.z + 1200.0f), Quaternion.identity);
                    newPlatforms.Add(newPlatform);
                }
                else if (randomNo >= 12 && randomNo < 13)
                {
                    GameObject newPlatform = Instantiate(platformD07, new Vector3(0, -0.5f, platform.transform.position.z + 1200.0f), Quaternion.identity);
                    newPlatforms.Add(newPlatform);
                }
                else if (randomNo >= 13 && randomNo < 14)
                {
                    GameObject newPlatform = Instantiate(platformD08, new Vector3(0, -0.5f, platform.transform.position.z + 1200.0f), Quaternion.identity);
                    newPlatforms.Add(newPlatform);
                }
                else if (randomNo >= 14 && randomNo < 15)
                {
                    GameObject newPlatform = Instantiate(platformD09, new Vector3(0, -0.5f, platform.transform.position.z + 1200.0f), Quaternion.identity);
                    newPlatforms.Add(newPlatform);
                }
                else if (randomNo == 15)
                {
                    GameObject newPlatform = Instantiate(platformD010, new Vector3(0, -0.5f, platform.transform.position.z + 1200.0f), Quaternion.identity);
                    newPlatforms.Add(newPlatform);
                }
                Destroy(platform);
                deletedPlatforms.Add(platform);
            }
        }

        //Clears the deleted platforms list
        foreach (GameObject platform in deletedPlatforms)
        {
            platforms.Remove(platform);
        }

        //Adds all new platforms into the main platforms list
        platforms.AddRange(newPlatforms);

        //Moves every platform to simulate player movement
        foreach (GameObject platform in platforms)
        {
            if (dController.HasPlayerCollided() == false)
            {
                Vector3 newPosition = platform.transform.position + new Vector3(0, 0, -platformMoveSpeed * Time.deltaTime);
                platform.transform.position = newPosition;
            }
        }
    }
}