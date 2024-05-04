using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletIcon : MonoBehaviour
{
    private bool time;

    Animator anim;
    PlayerMovement playerMovement;
    
    void Start()
    {
        GameObject Player = GameObject.Find("Player");
        playerMovement = Player.GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        time = playerMovement.time;
        if (time == true) {
            anim.SetBool("time?", true);
        } else {
            anim.SetBool("time?", false);
        }
    }
}