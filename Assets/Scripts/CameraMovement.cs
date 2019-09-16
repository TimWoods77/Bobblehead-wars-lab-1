using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject followTarget; // what camera follows
    public float moveSpeed; // speed at which it moves at
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (followTarget != null) // checks to see if any target is available if not camera won't follow
        {
          transform.position = Vector3.Lerp(transform.position, // calculates required position of cameraMount
          followTarget.transform.position, Time.deltaTime * moveSpeed);
        }
    }
}
