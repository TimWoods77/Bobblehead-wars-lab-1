using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform launchPosition;

    void fireBullet()
    {   // 1   
        GameObject bullet = Instantiate(bulletPrefab) as GameObject; // creates a gameObject instance for a particular prefab
        // 2  
        bullet.transform.position = launchPosition.position;
        // 3   
        bullet.GetComponent<Rigidbody>().velocity = transform.parent.forward * 100;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetMouseButtonDown(0)) // checks to see if the left mouse button is held down
      {
       if (!IsInvoking("fireBullet")) // checking to see if the bullet is being invoked
       {
        InvokeRepeating("fireBullet", 0f, 0.1f); // calls a method to invoke the bullet if it is not invoked
       }
      }
        if (Input.GetMouseButtonUp(0)) // stops gun fire once mouse is left go
        {
            CancelInvoke("fireBullet");
        }
    }
}
