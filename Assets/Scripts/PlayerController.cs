using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody head;
    public LayerMask layerMask; // lets you indicate what layers the ray can hit
    public Animator bodyAnimator;
    public Rigidbody marineBody; // marine's body
    public float moveSpeed = 50.0f;// determines how fast the character will move around
    public float[] hitForce;// arrays of force values for the camera
    public float timeBetweenHits = 2.5f;// grace period after marine sustains damage
    private Vector3 currentLookTarget = Vector3.zero;// where you want the marine to stare
    private CharacterController characterController; // creates an instance variable to store characterController
    private bool isDead = false;// keeps track of players current death state
    private bool isHit = false;// flag that tells us the marine took a hit
    private float timeSinceHit = 0;// tracks amount of time in the grace period 
    private int hitNumber = -1;// number of times hero got hit also to get the shake intensity


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
            bodyAnimator.SetBool("IsMoving", false);
        }  else
        {
            head.AddForce(transform.right * 150, ForceMode.Acceleration); // gets the head to bobble by multipling direction by force.
            bodyAnimator.SetBool("IsMoving", true);
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
        if (isHit)
        {
            timeSinceHit += Time.deltaTime;//This tabulates time since the last hit to the hero. 
            if (timeSinceHit > timeBetweenHits)//If that time exceeds timeBetweenHits, the player can take more hits.
            {
                isHit = false; timeSinceHit = 0;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Alien alien = other.gameObject.GetComponent<Alien>(); if (alien != null)// checks to see if the colliding object has an alien script attached to it or not
        {
            // 1 
            if (!isHit)
            {
                hitNumber += 1;// hit number increases by one 
                // 2      
                CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();// references the camera shake after your hero gets hit
                if (hitNumber < hitForce.Length)// checks to see if the hitNumber is less than the number of force values of shakes to determine whether or not the hero is dead or not
                // 3       
                {
                    cameraShake.intensity = hitForce[hitNumber];// sets force for the shaking effect than shakes the camera
                    cameraShake.Shake();
                }
                else
                {
                    Die();
                }
                isHit = true;// plays grunt sound and kills alien
                // 4      
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.hurt);
            }     alien.Die();
        }
    }

    public void Die()
    {
        bodyAnimator.SetBool("IsMoving", false);// set to false because space marine is dead to prevent a zombie from running around
        marineBody.transform.parent = null;// removes current game object from the parent
        marineBody.isKinematic = false;// enables a collider to allow the marine's body to drop and roll
        marineBody.useGravity = true;
        marineBody.gameObject.GetComponent<CapsuleCollider>().enabled = true;
        marineBody.gameObject.GetComponent<Gun>().enabled = false;// disables the gun to prevent firing after death
        Destroy(head.gameObject.GetComponent<HingeJoint>());// destroys the joints to remove head from the body  
        head.transform.parent = null;//removes the gameobject from parent
        head.useGravity = true;
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.marineDeath);// destroys current object while playing death sound
        Destroy(gameObject); 
    }
}
