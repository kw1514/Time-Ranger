using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] ParticleSystem fireEffect;
    [SerializeField] public bool cyber;
    [SerializeField] public bool forest;

    AudioPlayer audioPlayer;
    BoxCollider2D boxCollider;

    private void Start() 
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z);

        if (other.tag == "Player")
        {
            audioPlayer.PlayCheckpointClip();
            ParticleSystem instance = Instantiate(fireEffect, position, Quaternion.identity);
            boxCollider.enabled = false;
        }    
    }
}
