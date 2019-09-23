using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;  //hero's game object
    public GameObject[] spawnPoints;// alien spawn point locations
    public GameObject alien; // prefab for alien
    public int maxAliensOnScreen; //determines how many aliens appear on the screen
    public int totalAliens;// total ammount of aliens
    public float minSpawnTime;// controls the rate at which aliens appear
    public float maxSpawnTime;
    public int aliensPerSpawn;// how much aliens that appear per spawn
    private int aliensOnScreen = 0;// tracks the ammount of aliens being displayed onto the screen
    private float generatedSpawnTime = 0;// tracks time between spawn events
    private float currentSpawnTime = 0;// tracks the milliseconds from last spawn

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
                }
            }
        }
    }

}
