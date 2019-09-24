using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaWall : MonoBehaviour
{
    private Animator arenaAnimator;

    // Start is called before the first frame update
    void Start()
    {
        GameObject arena = transform.parent.gameObject;// gets parent gameobject
        arenaAnimator = arena.GetComponent<Animator>();// once it gets the reference gameobject it calls the getcomponent to reference the animator
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
     arenaAnimator.SetBool("IsLowered", true);// when IsLowered is activated this sets it to true
    }
    void OnTriggerExit(Collider other)
    {
     arenaAnimator.SetBool("IsLowered", false);// raises the wall when the space marine leaves the trigger
    }
}
