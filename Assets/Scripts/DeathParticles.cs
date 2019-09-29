using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticles : MonoBehaviour
{
    private ParticleSystem deathParticles;// refers to partical system
    private bool didStart = false;// let's you know that the partical system has started to play

    // Start is called before the first frame update
    void Start()
    {
        deathParticles = GetComponent<ParticleSystem>();
    }

    public void Activate()
    {
        didStart = true;// starts the partical system
        deathParticles.Play();// informs the script that it has started
    }

    // Update is called once per frame
    void Update()
    {
        if (didStart && deathParticles.isStopped)// once partical system stops playing it deletes the death particle to allow it to only play once
        {
            Destroy(gameObject);
        }
    }

    public void SetDeathFloor(GameObject deathFloor)// checks to see if partical system has been loaded
    {
        if (deathParticles == null)// if haven't been called and death particles isn't populated it populates it
        {
            deathParticles = GetComponent<ParticleSystem>();
        }
        deathParticles.collision.SetPlane(0, deathFloor.transform);
    }
}
