using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{
    public GameObject player;//
    public Transform elevator;//raises the marine to the top of the elevator after he wins
    private Animator arenaAnimator;// kicks off the animation
    private SphereCollider sphereCollider;// initiates the entire sequence

    // Start is called before the first frame update
    void Start()
    {
        arenaAnimator = GetComponent<Animator>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        Camera.main.transform.parent.gameObject.GetComponent<CameraMovement>().enabled = false; // gets the camera then disables the movement
        player.transform.parent = elevator.transform;// player is made into child of platform
        player.GetComponent<PlayerController>().enabled = false;// disables players control of the marine
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.elevatorArrived);// alerts player of elevator arrival
        arenaAnimator.SetBool("OnElevator", true);// kicks off the animation
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
