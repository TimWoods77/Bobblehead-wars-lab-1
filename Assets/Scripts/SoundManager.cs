using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip gunFire;
    public AudioClip upgradedGunFire;
    public AudioClip hurt;
    public AudioClip alienDeath;
    public AudioClip marineDeath;
    public AudioClip victory;
    public AudioClip elevatorArrived;
    public AudioClip powerUpPickup;
    public AudioClip powerUpAppear;
    public static SoundManager Instance = null; // stores a single instance to the SoundManager
    private AudioSource soundEffectAudio;// alows us to play sound effects

    public void PlayOneShot(AudioClip clip)
    {
        soundEffectAudio.PlayOneShot(clip);// a wrapper call to PlayOneShot
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)// singleton pattern that makes sure that there is only one copy of the object in existence
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        AudioSource[] sources = GetComponents<AudioSource>(); foreach (AudioSource source in sources)// returns all components of a particular type
        {
            if (source.clip == null)// checks all audio sources before it adds the sound effects
            {
                soundEffectAudio = source;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
