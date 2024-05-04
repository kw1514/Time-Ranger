using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    private float timer;
    public float travelTimer;

    [SerializeField] Transform gun;

    [Header("Standard")]
    [SerializeField] GameObject standardBullet;
    [SerializeField] float timeBetweenFiring;
    [SerializeField] bool canFire;

    [Header("Time Travel")]
    [SerializeField] GameObject timeBullet;
    [SerializeField] public float timeBetweenTravel;
    [SerializeField] public bool canTravel;

    private float rotZ = 0f;
    private float constraintT = 85f;
    private float constraintB = -85f;

    PlayerMovement player;
    AudioPlayer audioPlayer;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        // change to false for checkpoint activation
        canTravel = false;
    }


    void Update()
    {
        bool timeShot = player.IsTimeShot();
        bool standardShot = player.IsStandardShot();
        Vector2 faceDir = player.getMoveInput();
        float faceDirDir = 1f;
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        //This flips the rotation of the arm to prevent inverted controls. Note, does get stuck at extremes?
        Vector3 rotation = (mousePos - transform.position) * (Mathf.Sign(player.transform.localScale.x) * 1);

        if (faceDir.x != 0)
        {
            //removing this fixes the issue with the arm not moving while shooting?
            //I think I did this to emulate how terraria kind of locks your shooting in one direction when
            //holding down fire?
            //faceDirDir = faceDir.y;
            
        }
        //clamped value so that it cannot go absurdly above head / behind back
        //if (Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg < 0 && rotZ >= constraintT * faceDirDir)
        if ((mousePos.x < player.transform.position.x) && player.transform.localScale.x > 0)
        {
            //rotZ = constraintT * faceDirDir;
            player.transform.localScale = new Vector2(-player.transform.localScale.x, player.transform.localScale.y);

        }

        //else if (Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg > 0 && rotZ <= constraintB * faceDirDir)
        else if ((mousePos.x > player.transform.position.x) && player.transform.localScale.x < 0)
        {
            //rotZ = constraintB * faceDirDir;
            player.transform.localScale = new Vector2(player.transform.localScale.x * -1, player.transform.localScale.y);
        }
        else
        {
            rotZ = Mathf.Clamp(Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg, -90f, 90f);
        }
        //float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }

        if (!canTravel && player.timeTravelUnlocked)
        {
            travelTimer += Time.deltaTime;
            if (travelTimer > timeBetweenTravel)
            {
                canTravel = true;
                travelTimer = 0;
            }
        }

        if ((Input.GetMouseButton(0) && canFire))
        {
            canFire = false;

            if (standardShot)
            {
                audioPlayer.PlayShootingClip();
                Instantiate(standardBullet, gun.position, Quaternion.identity);
            }
            else if (timeShot && canTravel == true && player.myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) ||
            player.myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Objects")) ||
            player.myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Platform")))
            {
                audioPlayer.PlayTimeShootingClip();
                Instantiate(timeBullet, gun.position, Quaternion.identity);
                canTravel = false;
            }
        }
    }

    public void SetCanTravel(bool active)
    {
        canTravel = active;
    }
}
