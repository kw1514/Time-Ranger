using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    GameObject Background1;
    GameObject Background2;



    void Start()
    {
        Background1 = GameObject.FindGameObjectWithTag("Background1");
        Background2 = GameObject.FindGameObjectWithTag("Background2");

        Background1.SetActive(true);
        Background2.SetActive(true);
    }


    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.tag == "CyberTarget" && other.gameObject.tag == "Bullet")
        {
            Background2.SetActive(true);
            Background1.SetActive(false);
            //Background2.SetActive(true);
            //Destroy(gameObject);
        }
        else if (gameObject.tag == "ForestTarget" && other.gameObject.tag == "Bullet")
        {
            //Background1 = GameObject.FindGameObjectWithTag("Background1");
            // Background2 = GameObject.FindGameObjectWithTag("Background2");
            Background1.SetActive(true);
            Background2.SetActive(false);
            //Destroy(gameObject);
        }
    }
}
