using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Jump")]
    [SerializeField] AudioClip jumpClip;
    [SerializeField] [Range(0f, 1f)] float jumpVolume = 1f;

    [Header("Player Die")]
    [SerializeField] AudioClip playerDieClip;
    [SerializeField] [Range(0f, 1f)] float playerDieVolume = 1f;

    [Header("Enemy Die")]
    [SerializeField] AudioClip enemyDieClip;
    [SerializeField] [Range(0f, 1f)] float enemyDieVolume = 1f;

    [Header("Checkpoint")]
    [SerializeField] AudioClip checkpointClip;
    [SerializeField] [Range(0f, 1f)] float checkpointVolume = 1f;

    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f, 1f)] float shootingVolume = 1f;

    [Header("Time Shooting")]
    [SerializeField] AudioClip timeShootingClip;
    [SerializeField] [Range(0f, 1f)] float timeShootingVolume = 1f;

    [Header("Regular Shot")]
    [SerializeField] AudioClip regularImpactClip;
    [SerializeField] [Range(0f, 1f)] float regularImpactVolume = 1f;

    [Header("Time Shot")]
    [SerializeField] AudioClip timeImpactClip;
    [SerializeField] [Range(0f, 1f)] float timeImpactVolume = 1f;

    [Header("Ammunition Switch")]
    [SerializeField] AudioClip ammoSwitchClip;
    [SerializeField] [Range(0f, 1f)] float ammoSwitchVolume = 1f;

    // The Interactibles sound
    [Header("Switch")]
    [SerializeField] AudioClip switchClip;
    [SerializeField] [Range(0f, 1f)] float switchVolume = 1f;

    [Header("Cyber Door")]
    [SerializeField] AudioClip cyberDoorClip;
    [SerializeField] [Range(0f, 1f)] float cyberDoorVolume = 1f;

    [Header("Forest Door")]
    [SerializeField] AudioClip forestDoorClip;
    [SerializeField] [Range(0f, 1f)] float forestDoorVolume = 1f;

    [Header("Pick Up")]
    [SerializeField] AudioClip pickupClip;
    [SerializeField] [Range(0f, 1f)] float pickupVolume = 1f;

    [Header("Coin Pick Up")]
    [SerializeField] AudioClip coinPickupClip;
    [SerializeField] [Range(0f, 1f)] float coinPickupVolume = 1f;


    public void PlayJumpClip()
    {
        PlayClip(jumpClip, jumpVolume);
    }

    public void PlayPlayerDieClip()
    {
        PlayClip(playerDieClip, playerDieVolume);
    }

    public void PlayEnemyDieClip()
    {
        PlayClip(enemyDieClip, enemyDieVolume);
    }

    public void PlayCheckpointClip()
    {
        PlayClip(checkpointClip, checkpointVolume);
    }

    public void PlayShootingClip()
    {
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayTimeShootingClip()
    {
        PlayClip(timeShootingClip, timeShootingVolume);
    }


    public void PlayRegularImpactClip()
    {
        PlayClip(regularImpactClip, regularImpactVolume);
    }

    public void PlayTimeImpactClip()
    {
        PlayClip(timeImpactClip, timeImpactVolume);
    }

    public void PlayAmmoSwitchClip()
    {
        PlayClip(ammoSwitchClip, ammoSwitchVolume);
    }


    public void PlaySwitchClip()
    {
        PlayClip(switchClip, switchVolume);
    }

    public void PlayCyberDoorClip()
    {
        PlayClip(cyberDoorClip, cyberDoorVolume);
    }

    public void PlayForestDoorClip()
    {
        PlayClip(forestDoorClip, forestDoorVolume);
    }

    public void PlayPickupClip()
    {
        PlayClip(pickupClip, pickupVolume);
    }

     public void PlayCoinPickupClip()
    {
        PlayClip(coinPickupClip, coinPickupVolume);
    }


    void PlayClip(AudioClip clip, float volume)
    {
        if(clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }
}
