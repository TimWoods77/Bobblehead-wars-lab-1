using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Alien : MonoBehaviour
{
    public Transform target;// where alien should go
    private NavMeshAgent agent;
    public float navigationUpdate;//time in milliseconds that the alien updates its path
    private float navigationTime = 0; // keeps track of the time that had past since the last update
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();// gets a reference from the navmesh so you can add it to code
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            navigationTime += Time.deltaTime; // checks to see if a certain amount of time passed then updates the path
            if (navigationTime > navigationUpdate)
            {
              agent.destination = target.position; navigationTime = 0;
            }
        }
    }
    void OnTriggerEnter(Collider other)// makes the collision into a trigger
    {
      Destroy(gameObject);// deletes the alien instance 
      SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDeath);
    }
}