using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;// Allows you to use UnityEvent in code

public class Alien : MonoBehaviour
{
    public UnityEvent OnDestroy;// custom unityevent type that can be configured in the inspector. calls an OnDestroy call which The OnDestroy event will occur with each call to an alien.
    public Transform target;// where alien should go
    public Rigidbody head;// helps launch alien head
    public bool isAlive = true;//tracks alien's state
    public float navigationUpdate;//time in milliseconds that the alien updates its path
    private NavMeshAgent agent;
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
            if (isAlive)
            {    
             navigationTime += Time.deltaTime; // checks to see if a certain amount of time passed then updates the path
             if (navigationTime > navigationUpdate)
             {
               agent.destination = target.position;
               navigationTime = 0;
             }

            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (isAlive)// checks to see if alien is alive first
        {
            Die(); SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDeath);
        }
    }

    public void Die()
    {
        isAlive = false;// all of this code does is launch the alien's head off the body then destroys the animator for the alien
        head.GetComponent<Animator>().enabled = false;
        head.isKinematic = false;
        head.useGravity = true;
        head.GetComponent<SphereCollider>().enabled = true;
        head.gameObject.transform.parent = null;
        head.velocity = new Vector3(0, 26.0f, 3.0f);
        OnDestroy.Invoke();
        OnDestroy.RemoveAllListeners();// this code notifies the listeners removes them then deletes game object
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDeath);
        head.GetComponent<SelfDestruct>().Initiate();
        Destroy(gameObject);
    }
}