using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabObjects : MonoBehaviour
{
    [SerializeField] Transform grabPoint;
    [SerializeField] Transform rayPoint;
    [SerializeField] float rayDistance;

    private GameObject grabbedObject;
    private int layerIndex;
    
    void Start()
    {
        layerIndex = LayerMask.NameToLayer("Objects");
    }

    
    void Update()
    {
        //Debug.Log("hi");
        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, transform.right, rayDistance);

        if(hitInfo.collider != null && hitInfo.collider.gameObject.layer == layerIndex)
        {
            // grab Object
            if (Keyboard.current.eKey.wasPressedThisFrame && grabbedObject == null)
            {
                Debug.Log("Pick up");
                grabbedObject = hitInfo.collider.gameObject;
                grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;
                grabbedObject.transform.position = grabPoint.position;
                grabbedObject.transform.SetParent(transform);
            }
            // release Object
            else if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
                grabbedObject.transform.SetParent(null);
                grabbedObject = null;
            }
        }

        Debug.DrawRay(rayPoint.position, transform.right * rayDistance);
    }
}
