using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Travel : MonoBehaviour
{
    //og value 12
    //value for playtest will be 408
    [SerializeField] float distanceBetweenWorlds;
    [SerializeField] public bool cyberCity;
    [SerializeField] public bool forest;
    [SerializeField] CinemachineConfiner confiner;
    [SerializeField] PolygonCollider2D cyberConfiner;
    [SerializeField] PolygonCollider2D forestConfiner;

    // BoxCollider2D myBoxCollider;
    PlayerMovement player;

    void Start()
    {
        //myBoxCollider = GetComponent<BoxCollider2D>();
        player = FindObjectOfType<PlayerMovement>();
        confiner.m_BoundingShape2D = cyberConfiner;
        distanceBetweenWorlds = 45;
        //cyberCity = true;
        //forest = false;
    }

    public void TimeTravel()
    {
        // For this to work the ground must be set to the Ground layer
        //if (!myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        BoxCollider2D collider = player.feetCollider;

        if (!collider.IsTouchingLayers(LayerMask.GetMask("Ground")) &&
        !collider.IsTouchingLayers(LayerMask.GetMask("Objects")) &&
        !collider.IsTouchingLayers(LayerMask.GetMask("Platform"))) { return; }


        if (forest && !cyberCity)
        {
            forest = false;
            cyberCity = true;
            //originally +15 now -8
            //Vector2 newPos = new Vector2(0, distanceBetweenWorlds + 8f);
            Vector2 newPos = new Vector2(0, distanceBetweenWorlds);
            transform.Translate(newPos);
            confiner.m_BoundingShape2D = cyberConfiner;;
            return;
        }
       
        if (cyberCity && !forest)
        {
            cyberCity = false;
            forest = true;
            //Vector2 newPos = new Vector2(0, -distanceBetweenWorlds + -8f);
            Vector2 newPos = new Vector2(0, -distanceBetweenWorlds);
            transform.Translate(newPos);
            confiner.m_BoundingShape2D = forestConfiner;
            return;
        }
        

    }
    //Thomas here, added a get method to check for the cyber city. This can modularized in the future,
    //but for now it works.
    public bool getCyberCity()
    {
        return cyberCity;
    }
}
