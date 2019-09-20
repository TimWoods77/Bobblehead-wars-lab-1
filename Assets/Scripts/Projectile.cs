using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnBecameInvisible() { Destroy(gameObject); // destroys the bullets when they are no longer visible.
    }
    private void OnCollisionEnter(Collision collision) // called whenever the bullets makes collision with another gameobject, then destroys the bullet.
    {
      Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
