using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] Transform groundRay;
    [SerializeField] float groundRayDistance;
    [SerializeField] PhysicsMaterial2D noFrictionMaterial;
    [SerializeField] GameObject CyberFallDectector;
    [SerializeField] GameObject ForestFallDectector;
    [SerializeField] public BoxCollider2D feetCollider;
    public Vector3 respawnPoint { get; private set; }

    [Header("Shooting")]
    [SerializeField] bool timeShot;
    [SerializeField] bool standardShot;

    [Header("Interact")]
    [SerializeField] Transform rayPoint;
    [SerializeField] float rayDistance;
    [SerializeField] bool isCarrying;
    [SerializeField] PhysicsMaterial2D platformMaterial;
    [SerializeField] public bool timeTravelUnlocked;

    private GameObject grabbedObject = null;
    private int layerIndex;
    private int jumpIndex;
    [SerializeField] public bool time;

    Animator anim;
    //bool isMoving;
    UIDisplay display;
    AudioPlayer audioPlayer;
    Travel travel;
    Checkpoint checkpoint;

    Vector2 moveInput;
    Rigidbody2D myRigidbody;

    // will be changed based on the collider type on the player
    public BoxCollider2D myBoxCollider { get; private set; }

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        travel = FindObjectOfType<Travel>();
        checkpoint = FindObjectOfType<Checkpoint>();

        anim.SetBool("isMoving", false);
        respawnPoint = transform.position;

        display = FindObjectOfType<UIDisplay>();

        isCarrying = false;
        timeTravelUnlocked = false;

        // layerIndex = LayerMask.NameToLayer("Objects");
        jumpIndex = LayerMask.NameToLayer("JumpWall");
    }

    void Update()
    {
        Move();
        if (moveInput.x == 0)
        {
            // anim.Play("PlayerIdle");
            anim.SetBool("isMoving", false);
        }
        else
        {
            if (Input.GetMouseButton(0) == false)
            { transform.localScale = new Vector2(moveInput.x * Mathf.Abs(transform.localScale.x), transform.localScale.y); }
            anim.SetBool("isMoving", true);
        }
        if (isCarrying == true)
        {
            anim.SetBool("isCarrying", true);
        }
        else
        {
            anim.SetBool("isCarrying", false);
        }

        // checks to see if the player is on a platform or not. Uses a smaller collider so that
        // the player doesn't stick to the side of the platform.
        if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Platform")))
        {
            myBoxCollider.sharedMaterial = platformMaterial;
        }
        else
        {
            myBoxCollider.sharedMaterial = noFrictionMaterial;
        }

        CyberFallDectector.transform.position = new Vector2(transform.position.x, CyberFallDectector.transform.position.y);
        ForestFallDectector.transform.position = new Vector2(transform.position.x, ForestFallDectector.transform.position.y);

        CheckForInteractions();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        // For this to work the ground must be set to the Ground layer
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) &&
        !feetCollider.IsTouchingLayers(LayerMask.GetMask("Objects")) &&
        !feetCollider.IsTouchingLayers(LayerMask.GetMask("Platform"))) { return; }

        // makes it so the player can't jump forever. The ray is below the player and checks
        // if the player is touching the ground
        // RaycastHit2D groundInfo = Physics2D.Raycast(groundRay.position, transform.right, rayDistance);
        // if (groundInfo.collider == null) { return; }

        if (value.isPressed)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, transform.right, rayDistance);

            if (hitInfo.collider != null && hitInfo.collider.gameObject.layer == jumpIndex)
            // if (myBoxCollider.IsTouchingLayers(LayerMask.GetMask("JumpWall")))
            {
                // if (moveInput.x != 0)
                // {
                //     myRigidbody.velocity += new Vector2(-5f, jumpSpeed);
                // }
                // else 
                // {
                //     myRigidbody.velocity += new Vector2(5f, jumpSpeed);
                // }
            }
            else
            {
                audioPlayer.PlayJumpClip();
                myRigidbody.velocity += new Vector2(0f, jumpSpeed);
            }
        }

        // when you are wall jumping, jump in a curve
    }

    void OnSwitch(InputValue value)
    {
        if (value.isPressed)
        {
            // if the current bullet is the standardBullet it will switch to the timeBullet.
            if (standardShot && !timeShot && timeTravelUnlocked)
            {
                audioPlayer.PlayAmmoSwitchClip();
                standardShot = false;
                timeShot = true;
                time = true;
            }
            // if the current bullet is the timeBullet it will switch to standardBullet.
            else if (timeShot && !standardShot)
            {
                audioPlayer.PlayAmmoSwitchClip();
                timeShot = false;
                standardShot = true;
                time = false;
            }
        }
    }

    void OnInteract(InputValue value)
    {
        GameObject interactable = null;
        if (value.isPressed)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, transform.right, rayDistance);
            if (hitInfo.collider != null && hitInfo.collider.gameObject.layer == layerIndex)
            {
                interactable = hitInfo.collider.gameObject;

                if (interactable.tag == "Box")
                {
                    PickUpBox(hitInfo);
                }
                else if (interactable.tag == "Switch" && !isCarrying)
                {
                    PullSwitch(hitInfo);
                }
                else if (interactable.tag == "Bullet" && !timeTravelUnlocked)
                {
                    timeTravelUnlocked = true;
                    audioPlayer.PlayPickupClip();
                    interactable.SetActive(false);
                    display.PopUp("Congratulations! You found my special time vortex bullets! " +
                    "They will come in handy.");
                }
            }
            else if (hitInfo.collider == null && interactable == null && feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))
            || feetCollider.IsTouchingLayers(LayerMask.GetMask("Platform")) || feetCollider.IsTouchingLayers(LayerMask.GetMask("Objects")))
            {
                DropBox();
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            transform.position = respawnPoint;
        }
        else if (collision.tag == "Checkpoint")
        {
            // if (checkpoint.cyber == true)
            // {
            //     travel.cyberCity = true;
            //     travel.forest = false;
            // }
            // else if (checkpoint.forest == false)
            // {
            //     travel.cyberCity = false;
            //     travel.forest = true;
            // }
            respawnPoint = transform.position;
            display.PopUp("Checkpoint Reached!");
        }
    }


    void Move()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, myRigidbody.velocity.y);

        myRigidbody.velocity = playerVelocity;
        
    }


    void CheckForInteractions()
    {
        layerIndex = LayerMask.NameToLayer("Objects");
        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, transform.right, rayDistance);

        if (hitInfo.collider != null && hitInfo.collider.gameObject.layer == layerIndex
        && hitInfo.collider.tag != "Untagged" && !isCarrying)
        {
            display.setInteract(true);
        }
        else if (hitInfo.collider == null)
        {
            display.setInteract(false);
        }
        else if (hitInfo.collider.tag == "FirstWall")
        {
            display.PopUp("Try using my time vortex bullets to get around this wall. " +
            "Switch your current ammunition by pressing Q");
        }
    }

    // object being picked up needs to be on the Objects layer and have the Box tag
    void PickUpBox(RaycastHit2D hitInfo)
    {
        if (hitInfo.collider != null && hitInfo.collider.gameObject.layer == layerIndex)
        {
            if (grabbedObject == null)
            {
                audioPlayer.PlayPickupClip();
                grabbedObject = hitInfo.collider.gameObject;
                grabbedObject.SetActive(false);
                grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;
                grabbedObject.transform.SetParent(transform);
                isCarrying = true;
            }
        }
    }

    void DropBox()
    {
        audioPlayer.PlayPickupClip();
        grabbedObject.SetActive(true);
        grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
        grabbedObject.transform.SetParent(null);

        // Changes the offset depending on which way the player is facing
        if (myBoxCollider.transform.localScale.x > 0)
        {
            grabbedObject.transform.position = new Vector3(myBoxCollider.transform.position.x + 1.2f, myBoxCollider.transform.position.y,
                                                       myBoxCollider.transform.position.z);
        }
        else if (myBoxCollider.transform.localScale.x < 0)
        {
            grabbedObject.transform.position = new Vector3(myBoxCollider.transform.position.x + -1.2f, myBoxCollider.transform.position.y,
                                                           myBoxCollider.transform.position.z);
        }
        grabbedObject = null;
        isCarrying = false;
    }

    void PullSwitch(RaycastHit2D hitInfo)
    {
        GameObject switchParent = hitInfo.collider.gameObject;
        GameObject onSwitch = GetChildWithName(switchParent, "OnSwitch");
        GameObject offSwitch = GetChildWithName(switchParent, "OffSwitch");
        GameObject parent = GetParent(switchParent);
        

        onSwitch.SetActive(true);
        audioPlayer.PlaySwitchClip();
        offSwitch.SetActive(false);
        switchParent.tag = "Untagged";

        OpenDoor(parent);
    }

    void OpenDoor(GameObject parent)
    {
        GameObject closedDoor = GetChildWithName(parent, "ClosedDoor");
        GameObject openDoor = GetChildWithName(parent, "OpenDoor");

        // plays the audio based on the location of the player
        if (travel.getCyberCity())
        {
            audioPlayer.PlayCyberDoorClip();
        } 
        else 
        {
            audioPlayer.PlayForestDoorClip();
        }
        openDoor.SetActive(true);
        closedDoor.SetActive(false);
    }

    GameObject GetChildWithName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }

    GameObject GetParent(GameObject obj) 
    {
        Transform trans = obj.transform;
        Transform parentTrans = trans.root;
        if (parentTrans != null)
        {
            return parentTrans.gameObject;
        }
        else
        {
            return null;
        }
    }

    public bool IsTimeShot()
    {
        return timeShot;
    }

    public bool IsStandardShot()
    {
        return standardShot;
    }

    public Vector2 getMoveInput()
    {
        return moveInput;
    }
}
