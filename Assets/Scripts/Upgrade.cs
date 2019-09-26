using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public Gun gun;
    void OnTriggerEnter(Collider other)// when player collides with power-up it upgrades gun to upgrade mode 
    {
        gun.UpgradeGun();
        Destroy(gameObject);// then destroys itself
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.powerUpPickup);// sound that plays when player picks up upgrade
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
