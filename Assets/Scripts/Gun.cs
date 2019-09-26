using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform launchPosition;
    private AudioSource audioSource;
    public bool isUpgraded;// a flag that let's the script know to fire one or three bullets
    public float upgradeTime = 10.0f;// amount of time the upgrade lasts
    private float currentTime;//keeps track of how long the gun has been upgraded for

    private Rigidbody createBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab) as GameObject;// It encapsulates the bullet creation process. 
        bullet.transform.position = launchPosition.position;
        return bullet.GetComponent<Rigidbody>();//Returns a rigidbody after you create the bullet
    }

    void fireBullet()
    {
        Rigidbody bullet = createBullet(); bullet.velocity = transform.parent.forward * 100;// creates the bullet

        if (isUpgraded)
        {
            Rigidbody bullet2 = createBullet(); bullet2.velocity = (transform.right + transform.forward / 0.5f) * 100;// fires the next two bullets at angles. It calculates the angle by adding the forward direction to either
            Rigidbody bullet3 = createBullet(); bullet3.velocity = ((transform.right * -1) + transform.forward / 0.5f) * 100;//  the right- or left-hand direction and dividing in half. Since there is no explicit left property, you multiply the value of right by -1 to negate it.
        }
        if (isUpgraded)
        {
            audioSource.PlayOneShot(SoundManager.Instance.upgradedGunFire);// plays shooting sound
        }
        else
        {
            audioSource.PlayOneShot(SoundManager.Instance.gunFire);// plays shooting sound
        }
    }

    public void UpgradeGun()
    {
        isUpgraded = true;// lets the gun know it's been upgraded
        currentTime = 0; // sets counter to 0
    }


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();// gets a reference to the attached audio source
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
        currentTime += Time.deltaTime; // incraments the time.
        if (currentTime > upgradeTime && isUpgraded == true)// if time is > then time player has been upgraded it takes upgrade away
        {
            isUpgraded = false;
        }
    }
}
