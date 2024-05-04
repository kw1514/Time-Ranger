using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBullet : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;

    BoxCollider2D myBoxCollider;

    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletLife;
    [SerializeField] ParticleSystem impactParticles;

    Travel travel;
    PlayerMovement player;
    AudioPlayer audioPlayer;


    private void Awake()
    {
        travel = FindObjectOfType<Travel>();
        player = FindObjectOfType<PlayerMovement>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }


    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        //original formula
        Vector3 direction = mousePos - transform.position;


        // Vector3 direction = new Vector3(
        //     //for reference
        //     //Mathf.Sin((Mathf.Clamp(Mathf.Abs((playerMovement.transform.position.x-transform.position.x)), 0,1)* playerMovement.transform.localScale.x)),

        //     //As the x position of the gun gets closer to the position of the rotation point, the closer to 0 it gets. The further away, the closer to 1/-1

        //     Mathf.Sin(Mathf.Clamp(player.transform.position.x - transform.position.x, -1, 1)) * -1,
        //     Mathf.Sin(transform.position.y - player.transform.position.y),
        //     transform.position.z);

        // Vector2 rotation = new Vector2(
        //     Mathf.Sin(Mathf.Clamp(player.transform.position.x - transform.position.x, -1, 1)) * -1,
        //     Mathf.Sin(transform.position.y - player.transform.position.y));
        Vector3 rotation= mousePos - transform.position;

        rb.velocity = new Vector2(direction.x, direction.y).normalized * bulletSpeed;

        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    void Update()
    {
        Destroy(gameObject, bulletLife);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") { return; }

        if (other.tag == "TimeElement" || other.tag == "FirstWall")
        {
            audioPlayer.PlayTimeImpactClip();
            Instantiate(impactParticles, transform.position, Quaternion.identity);
            travel.TimeTravel();
        }

        Destroy(gameObject);
    }
}
