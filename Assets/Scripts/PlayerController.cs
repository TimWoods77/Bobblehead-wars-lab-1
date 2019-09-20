using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody head;
    public float moveSpeed = 50.0f;// determines how fast the character will move around
    private CharacterController characterController; // creates an instance variable to store characterController
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>(); // refrences the component 
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), // creates a new vector3 to store movement direction then calls SimpleMove() and passes
            0, Input.GetAxis("Vertical"));                               // in moveDirection * moveSpeed
        characterController.SimpleMove(moveDirection * moveSpeed);
    }
    void FixedUpdate() // handles physics.Used because were using force to move the bobblehead
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), // moves the head when you move spacemarine.
            0, Input.GetAxis("Vertical")); // calculates direction of movement
        if (moveDirection == Vector3.zero) // see if value equals vector3.zero if so marine stays still.
        {   // TODO 
        }  else
        {
            head.AddForce(transform.right * 150, ForceMode.Acceleration); // gets the head to bobble by multipling direction by force.
        }
    }
}
