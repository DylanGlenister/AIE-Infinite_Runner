using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    // References to other scripts
    public PlayerController pController;
    public DeathController dController;
    public ScoreController sController;

    // Randomly generated number used for platform spawns
    public int randomNo;

    // Difficulty settings
    public uint diffiulty = 0;

    // Playform movement speed
    public float platformMoveSpeed = 100.0f;

    // Reference to the platform prefabs
    public GameObject platform_Empty;
    public GameObject platform_Test;
    // Difficulty 0
    public GameObject platform_D0_1;
    public GameObject platform_D0_2;
    public GameObject platform_D0_3;
    public GameObject platform_D0_4;
    public GameObject platform_D0_5;
    // Difficulty 1
    public GameObject platform_D1_1;
    public GameObject platform_D1_2;
    public GameObject platform_D1_3;
    public GameObject platform_D1_4;
    public GameObject platform_D1_5;
    // Difficulty 2
    public GameObject platform_D2_1;
    public GameObject platform_D2_2;
    public GameObject platform_D2_3;
    public GameObject platform_D2_4;
    public GameObject platform_D2_5;
    // Difficulty 3
    public GameObject platform_D3_1;
    public GameObject platform_D3_2;
    public GameObject platform_D3_3;
    public GameObject platform_D3_4;
    public GameObject platform_D3_5;

    //enum Platforms
    //{
    //    aEmpty,
    //    aTest,
    //    D0_1,
    //    D0_2,
    //    D0_3,
    //    D0_4,
    //    D0_5,
    //    D0_6,
    //    D0_7,
    //    D0_8,
    //    D0_9,
    //    D0_10,
    //    D2_1,
    //    D3_1,
    //    D3_2,
    //    D3_3,
    //    D3_4,
    //}

    // A collection of lists used to manage the platforms
    List<GameObject> platforms = new List<GameObject>();
    List<GameObject> newPlatforms = new List<GameObject>();
    List<GameObject> deletedPlatforms = new List<GameObject>();

    private void Awake()
    {
        // Finds the scipts
        pController = FindObjectOfType<PlayerController>();
        dController = FindObjectOfType<DeathController>();
        sController = FindObjectOfType<ScoreController>();

        // Adds the starting platforms to the platform list
        platforms.AddRange(GameObject.FindGameObjectsWithTag("LevelStart"));
    }

    private void FixedUpdate()
    {
        // Controls difficulty based on score (distance travelled)
        if (sController.Score > 800 && diffiulty == 0)
            diffiulty = 1;
        else if (sController.Score > 2400 && diffiulty == 1)
            diffiulty = 2;
        else if (sController.Score > 5000 && diffiulty == 2)
            diffiulty = 3;

        // Resets the newPlatforms and deletedPlatforms list
        newPlatforms.Clear();
        deletedPlatforms.Clear();

        // For each platform in the platforms list we check if the position is out of player sight and spawns a new platform at the end of the line, 
        // then deletes the current platform and adds it to the deletedPlatforms list
        foreach (GameObject platform in platforms)
        {
            if (platform.transform.position.z < -100)
            {
                // Randomizes the platforms then accounts for difficulty
                randomNo = Random.Range(0, 5);
                //Debug.Log(randomNo);
                if (randomNo > 4)
                {
                    GameObject newPlatform = Instantiate(platform_Empty, new Vector3(0, -0.5f, platform.transform.position.z + 1200.0f), Quaternion.identity);
                    newPlatforms.Add(newPlatform);
                }
                else
                {
                    switch (diffiulty)
                    {
                        case 0:
                            if (randomNo == 0)
                            {
                                GameObject newPlatform = Instantiate(platform_D0_1, new Vector3(0, -0.5f, platform.transform.position.z + 1200.0f), Quaternion.identity);
                                newPlatforms.Add(newPlatform);
                            }
                            else if (randomNo == 1)
                            {
                                GameObject newPlatform = Instantiate(platform_D0_2, new Vector3(0, -0.5f, platform.transform.position.z + 1200.0f), Quaternion.identity);
                                newPlatforms.Add(newPlatform);
                            }
                            else if (randomNo == 2)
                            {
                                GameObject newPlatform = Instantiate(platform_D0_3, new Vector3(0, -0.5f, platform.transform.position.z + 1200.0f), Quaternion.identity);
                                newPlatforms.Add(newPlatform);
                            }
                            else if (randomNo == 3)
                            {
                                GameObject newPlatform = Instantiate(platform_D0_4, new Vector3(0, -0.5f, platform.transform.position.z + 1200.0f), Quaternion.identity);
                                newPlatforms.Add(newPlatform);
                            }
                            else if (randomNo == 4)
                            {
                                GameObject newPlatform = Instantiate(platform_D0_5, new Vector3(0, -0.5f, platform.transform.position.z + 1200.0f), Quaternion.identity);
                                newPlatforms.Add(newPlatform);
                            }
                            break;
                        case 1:
                            if (randomNo == 0)
                            {
                                GameObject newPlatform = Instantiate(platform_D1_1, new Vector3(0, -0.5f, platform.transform.position.z + 1200.0f), Quaternion.identity);
                                newPlatforms.Add(newPlatform);
                            }
                            else if (randomNo == 1)
                            {
                                GameObject newPlatform = Instantiate(platform_D1_2, new Vector3(0, -0.5f, platform.transform.position.z + 1200.0f), Quaternion.identity);
                                newPlatforms.Add(newPlatform);
                            }
                            else if (randomNo == 2)
                            {
                                GameObject newPlatform = Instantiate(platform_D1_3, new Vector3(0, -0.5f, platform.transform.position.z + 1200.0f), Quaternion.identity);
                                newPlatforms.Add(newPlatform);
                            }
                            else if (randomNo == 3)
                            {
                                GameObject newPlatform = Instantiate(platform_D1_4, new Vector3(0, -0.5f, platform.transform.position.z + 1200.0f), Quaternion.identity);
                                newPlatforms.Add(newPlatform);
                            }
                            else if (randomNo == 4)
                            {
                                GameObject newPlatform = Instantiate(platform_D1_5, new Vector3(0, -0.5f, platform.transform.position.z + 1200.0f), Quaternion.identity);
                                newPlatforms.Add(newPlatform);
                            }
                            break;
                        case 2:
                            if (randomNo == 0)
                            {
                                GameObject newPlatform = Instantiate(platform_D2_1, new Vector3(0, -0.5f, platform.transform.position.z + 1200.0f), Quaternion.identity);
                                newPlatforms.Add(newPlatform);
                            }
                            else if (randomNo == 1)
                            {
                                GameObject newPlatform = Instantiate(platform_D2_2, new Vector3(0, -0.5f, platform.transform.position.z + 1200.0f), Quaternion.identity);
                                newPlatforms.Add(newPlatform);
                            }
                            else if (randomNo == 2)
                            {
                                GameObject newPlatform = Instantiate(platform_D2_3, new Vector3(0, -0.5f, platform.transform.position.z + 1200.0f), Quaternion.identity);
                                newPlatforms.Add(newPlatform);
                            }
                            else if (randomNo == 3)
                            {
                                GameObject newPlatform = Instantiate(platform_D2_4, new Vector3(0, -0.5f, platform.transform.position.z + 1200.0f), Quaternion.identity);
                                newPlatforms.Add(newPlatform);
                            }
                            else if (randomNo == 4)
                            {
                                GameObject newPlatform = Instantiate(platform_D2_5, new Vector3(0, -0.5f, platform.transform.position.z + 1200.0f), Quaternion.identity);
                                newPlatforms.Add(newPlatform);
                            }
                            break;
                        case 3:
                            if (randomNo == 0)
                            {
                                GameObject newPlatform = Instantiate(platform_D3_1, new Vector3(0, -0.5f, platform.transform.position.z + 1200.0f), Quaternion.identity);
                                newPlatforms.Add(newPlatform);
                            }
                            else if (randomNo == 1)
                            {
                                GameObject newPlatform = Instantiate(platform_D3_2, new Vector3(0, -0.5f, platform.transform.position.z + 1200.0f), Quaternion.identity);
                                newPlatforms.Add(newPlatform);
                            }
                            else if (randomNo == 2)
                            {
                                GameObject newPlatform = Instantiate(platform_D3_3, new Vector3(0, -0.5f, platform.transform.position.z + 1200.0f), Quaternion.identity);
                                newPlatforms.Add(newPlatform);
                            }
                            else if (randomNo == 3)
                            {
                                GameObject newPlatform = Instantiate(platform_D3_4, new Vector3(0, -0.5f, platform.transform.position.z + 1200.0f), Quaternion.identity);
                                newPlatforms.Add(newPlatform);
                            }
                            else if (randomNo == 4)
                            {
                                GameObject newPlatform = Instantiate(platform_D3_5, new Vector3(0, -0.5f, platform.transform.position.z + 1200.0f), Quaternion.identity);
                                newPlatforms.Add(newPlatform);
                            }
                            break;
                    }
                }
                // Removes the platform behind the player
                Destroy(platform);
                deletedPlatforms.Add(platform);
            }
        }

        // Clears the deleted platforms list
        foreach (GameObject platform in deletedPlatforms)
        {
            platforms.Remove(platform);
        }

        // Adds all new platforms into the main platforms list
        platforms.AddRange(newPlatforms);

        // Moves every platform to simulate player movement
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