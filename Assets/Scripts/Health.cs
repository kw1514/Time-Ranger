using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public bool isPlayer;
    [SerializeField] public bool isEnemy;
    [SerializeField] int health = 50;
    [SerializeField] float respawnDelay;

    static int constHealth;

    [Header("Particle Effects")]
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] ParticleSystem dieEffect;

    PlayerMovement player;
    AudioPlayer audioPlayer;
    Travel travel;
    Checkpoint checkpoint;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        travel = FindObjectOfType<Travel>();
        checkpoint = FindObjectOfType<Checkpoint>();

        if (isPlayer)
        {
            constHealth = health;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());

            damageDealer.Hit();
        }
    }

    public int GetHealth()
    {
        return health;
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        PlayHitEffect();

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isEnemy)
        {
            audioPlayer.PlayEnemyDieClip();
            Destroy(gameObject);
        }
        else if (isPlayer)
        {
            audioPlayer.PlayPlayerDieClip(); 
            StartCoroutine(WaitAndRespawn(respawnDelay));
        }
        else
        {
            // Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    public void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    public void PlayDieEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem instance = Instantiate(dieEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    IEnumerator WaitAndRespawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.position = player.respawnPoint;
        health = constHealth;
    }
}
