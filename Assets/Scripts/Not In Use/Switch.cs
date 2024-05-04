using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    // dont' SerializeField these, set them in the pullSwitch method with the raycast info.
    // [SerializeField] GameObject onSwitch;
    // [SerializeField] GameObject offSwitch;
    // [SerializeField] bool isThrown;
    //[SerializeField] Switch switchScript;
    //[SerializeField] GameObject switchObject;
    // [SerializeField] public GameObject openDoor;
    // [SerializeField] public GameObject closedDoor;
    //[SerializeField] GameObject doorObject;
    [SerializeField] Door door;
    //[SerializeField] GameObject doorObject;

    //AudioPlayer audioPlayer;
    //Door door;

    void Start()
    {
        // audioPlayer = FindObjectOfType<AudioPlayer>();
        // isThrown = false;
       // Debug.Log(gameObject);
        // door = FindObjectOfType<Door>();
        // onSwitch.SetActive(false);
    }

    // private void Update()
    // {
    //     if (gameObject.tag != "Untagged")
    //     {
    //         onSwitch.SetActive(false);
    //         offSwitch.SetActive(true);
    //     }
    // }

    public void PullSwitch()
    {
            // onSwitch.SetActive(true);
            // audioPlayer.PlaySwitchClip();
            // offSwitch.SetActive(false);
            // gameObject.tag = "Untagged";
            // Debug.Log("pull " + gameObject);
            door.OpenDoor();
            // isThrown = true;
            // door.enabled = false;
    }

    // public bool GetIsThrown()
    // {
    //     return isThrown;
    // }
}
