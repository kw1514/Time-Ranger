using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject openDoor;
    [SerializeField] GameObject closedDoor;
    [SerializeField] bool isCyber;
    // [SerializeField] bool isOpen;

    AudioPlayer audioPlayer;

    void Start()
    {
        openDoor.SetActive(false);
        closedDoor.SetActive(true);
        // isOpen = false;

        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    public void OpenDoor()
    {
        // if (!isOpen)
        // {
            if (isCyber)
            {
                audioPlayer.PlayCyberDoorClip();
            }
            else
            {
                audioPlayer.PlayForestDoorClip();
            }

            openDoor.SetActive(true);
            closedDoor.SetActive(false);
            // isOpen = true;
        //}
    }
}
