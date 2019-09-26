using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;// Allows you to use UnityEvent in code

public class Alien : MonoBehaviour
{
    public UnityEvent OnDestroy;// custom unityevent type that can be configured in the inspector. calls an OnDestroy call which The OnDestroy event will occur with each call to an alien.
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
        Die();// kills the alien instance and allows other objects to trigger the death behaviour
      SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDeath);
    }

    public void Die()
    {
        OnDestroy.Invoke();// notifies everyone and GameManager of the aliens death so it can send out condolence cards to it's family lol
        OnDestroy.RemoveAllListeners();// removes any listeners listening to event
        Destroy(gameObject);// kills the alien
    }
}