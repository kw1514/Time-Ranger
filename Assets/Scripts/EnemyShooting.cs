using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] GameObject standardBullet;
    [SerializeField] Transform bulletOrigin;
    [SerializeField] int attackRange = 5;
    [SerializeField] bool canFire;
    [SerializeField] float timeBetweenFiring;

    PlayerMovement playerMovement;
    AudioPlayer audioPlayer;
    private float timer;

    Animator anim;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canFire)
        {
            anim.SetBool("attack", false);
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }
        if ((transform.position.x - playerMovement.transform.position.x < attackRange) && canFire)
        {
            anim.SetBool("attack", true);
            canFire = false;
            audioPlayer.PlayShootingClip();
            Instantiate(standardBullet, bulletOrigin.position, Quaternion.identity);
            
        }
    }
}
