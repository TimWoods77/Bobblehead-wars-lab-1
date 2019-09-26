using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;  //hero's game object
    public GameObject[] spawnPoints;// alien spawn point locations
    public GameObject alien; // prefab for alien
    public Gun gun;// reference to gun script
    public GameObject upgradePrefab;// gameobject player must collide with
    public int maxAliensOnScreen; //determines how many aliens appear on the screen
    public int totalAliens;// total ammount of aliens
    public float minSpawnTime;// controls the rate at which aliens appear
    public float maxSpawnTime;// controls the rate at which maximum aliens appear
    public float upgradeMaxTimeSpawn = 7.5f;//the maximum time that will pass before the upgrade spawns. 
    public int aliensPerSpawn;// how much aliens that appear per spawn
    private bool spawnedUpgrade = false;// tracks whether or not the upgrade has spawned 
    private int aliensOnScreen = 0;// tracks the ammount of aliens being displayed onto the screen
    private float generatedSpawnTime = 0;// tracks time between spawn events
    private float currentSpawnTime = 0;// tracks the milliseconds from last spawn
    private float actualUpgradeTime = 0;// track the current time until the upgrade spawns.
    private float currentUpgradeTime = 0;// track the current time until the upgrade spawns.

    // Start is called before the first frame update
    void Start()
    {
        actualUpgradeTime = Random.Range(upgradeMaxTimeSpawn - 3.0f, upgradeMaxTimeSpawn);// a random number generated where the minimum value is the maximum of 3
        actualUpgradeTime = Mathf.Abs(actualUpgradeTime);// makes sure the number is a positive number
    }

    // Update is called once per frame
    void Update()
    {
        currentUpgradeTime += Time.deltaTime;//adds the amount of time from the past frame.
        if (currentUpgradeTime > actualUpgradeTime)
        {
            // 1   
            if (!spawnedUpgrade)// after the random time has past this checks to see if the upgrade had already spawned
            {
                // 2   
                int randomNumber = Random.Range(0, spawnPoints.Length - 1);// spawns upgrade in one of the aliens spawn points
                GameObject spawnLocation = spawnPoints[randomNumber];
                // 3 
                GameObject upgrade = Instantiate(upgradePrefab) as GameObject;// spawns the upgrade and associates the gun with it
                Upgrade upgradeScript = upgrade.GetComponent<Upgrade>();
                upgradeScript.gun = gun;
                upgrade.transform.position = spawnLocation.transform.position;
                // 4
                spawnedUpgrade = true;// tells code that an upgrade has been spawned
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.powerUpAppear);// sound when power-up is available
            }
        }

        currentSpawnTime += Time.deltaTime;// accumulates time passed by each frame
        if (currentSpawnTime > generatedSpawnTime)// creates spwn times from min to max times
        {
            currentSpawnTime = 0;// resets timer after a spawn occured
            generatedSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);// spawn time randomizer
        }
        if (aliensPerSpawn > 0 && aliensOnScreen < totalAliens)// stops spawns when max aliens is met
        {
          List<int> previousSpawnLocations = new List<int>();// makes an array to keep track of where you spawn aliens each wave.
          if (aliensPerSpawn > spawnPoints.Length)// limits # of aliens that can be spawned per point
            {
                aliensPerSpawn = spawnPoints.Length - 1;
            }
            aliensPerSpawn = (aliensPerSpawn > totalAliens) ? // if alien exceeds maximum spawns it will reduce the amount of spawns
            aliensPerSpawn - totalAliens : aliensPerSpawn; // never creates more then the maximum amount of alien spawns that you configure
            for (int i = 0; i < aliensPerSpawn; i++)// for each spawned alien the loop interates once
            {
                if (aliensOnScreen < maxAliensOnScreen)// checks to see if the alienOnScreen is less than the maximum
                {
                    aliensOnScreen += 1;// Then incriments total screen amount
                                        // 1 
                    int spawnPoint = -1;// marked at -1 to state that a spawn point isn't selected
                                        // 2
                    while (spawnPoint == -1)// loop continues until it finds a spawn point or until there is no longer a spawn point
                    {
                        // 3  
                        int randomNumber = Random.Range(0, spawnPoints.Length - 1);// produces arandom spawn point position
                                                                                   // 4   
                        if (!previousSpawnLocations.Contains(randomNumber))// checks previousSpawnLocations to see if the random number is a current spawn point if no match it becomes your spawn point
                        {
                            previousSpawnLocations.Add(randomNumber);
                            spawnPoint = randomNumber;
                        }
                    }
                    GameObject spawnLocation = spawnPoints[spawnPoint];// grabs current spawn location generated in last code
                    GameObject newAlien = Instantiate(alien) as GameObject;// creates an instance of any prefab that is passed into it
                    newAlien.transform.position = spawnLocation.transform.position;// positions the alien at the spawn points
                    Alien alienScript = newAlien.GetComponent<Alien>();// gets the reference from the Alien script
                    alienScript.target = player.transform;// sets the alien towards the space marine's current position
                    Vector3 targetRotation = new Vector3(player.transform.position.x, // rotates the alien towards the hero using the alien's y axis position
                       newAlien.transform.position.y, player.transform.position.z);
                    newAlien.transform.LookAt(targetRotation);
                    alienScript.OnDestroy.AddListener(AlienDestroyed);// notifies the GameManger everytime an alien gets destroyed
                }
            }
        }
    }

    public void AlienDestroyed()
    {
        aliensOnScreen -= 1;// decreases number of aliens on screen
        totalAliens -= 1;
    }
    
       // Debug.Log("dead alien");
    
}
