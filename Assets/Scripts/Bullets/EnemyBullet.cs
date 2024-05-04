using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletLife;

    private Rigidbody2D rb;
    BoxCollider2D myBoxCollider;

    AudioPlayer audioPlayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        audioPlayer = FindObjectOfType<AudioPlayer>();

        Vector3 direction = new Vector3(-1, 0, 0);

        rb.velocity = new Vector2(direction.x, direction.y).normalized * bulletSpeed;


    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, bulletLife);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        /* does not currently work?
        if (myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Player"))) { return; }
        */
        if (other.tag == "Enemy") { return; }
        //else if (other.tag == "Enemy") { Destroy(other.gameObject); }

        audioPlayer.PlayRegularImpactClip();
        Destroy(gameObject);
    }
}
