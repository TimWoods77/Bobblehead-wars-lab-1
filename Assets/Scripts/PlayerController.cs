using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody head;
    public float moveSpeed = 50.0f;// determines how fast the character will move around
    public LayerMask layerMask; // lets you indicate what layers the ray can hit
    private Vector3 currentLookTarget = Vector3.zero;// where you want the marine to stare
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
        RaycastHit hit;// creates an empty raycast hit. If hit populates with the object
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);// cast ray from main camera to mouse position
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green); // draws a ray in scene
        if (Physics.Raycast(ray, out hit, 1000, layerMask, // this casts the ray by passing in the ray with the hit, then goes 1000m since it is the length of the ray, lastly the layerMask allows the ray to know what you are trying to hit.
            QueryTriggerInteraction.Ignore))// tells triggers not to activate
        {
            if (hit.point != currentLookTarget)// comprises the coordinates of the raycast hit
            {
                currentLookTarget = hit.point; // updates the raycast hit
            }
            // 1 
            Vector3 targetPosition = new Vector3(hit.point.x,    transform.position.y, hit.point.z); // rotation for the marine
            // 2 
            Quaternion rotation = Quaternion.LookRotation(targetPosition -    transform.position); // calculates first Quaternion to determine the rotation
            // 3 
            transform.rotation = Quaternion.Lerp(transform.rotation,    rotation, Time.deltaTime * 10.0f);// returns rotation of where the marine should stand and lerp allows the full turn.
        }
    }
}
