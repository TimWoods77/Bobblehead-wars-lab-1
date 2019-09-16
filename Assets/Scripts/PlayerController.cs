using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 50.0f;// determines how fast the character will move around
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;// gets the current position of the space marine
        pos.x += moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;// controls movement left and right then multiplied by moveSpeed to get current position and reads how long it took to get to the new position
        pos.z += moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;// controls movement up and down then multiplies by moveSpeed to get current position and reads how long it took to get to the new position
        transform.position = pos; // Updates space marine's location
    }
}
