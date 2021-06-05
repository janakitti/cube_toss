using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TossCube : MonoBehaviour {

    GameObject grabbedObject;
    float grabbedObjectSize;

    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(1))
        {
            if (grabbedObject == null)
            {
                TryGrabObject(GetMouseOnObject(5));
            } else
            {
                DropObject();
            }
        }
        if (grabbedObject != null)
        {
            Vector3 newPosition = gameObject.transform.position + Camera.main.transform.forward * grabbedObjectSize;
            grabbedObject.transform.position = newPosition;
        }
	}

    GameObject GetMouseOnObject(float range)
    {
        Vector3 position = gameObject.transform.position;
        RaycastHit raycastHit;
        Vector3 target = position + Camera.main.transform.forward * range;

        if (Physics.Linecast(position, target, out raycastHit))
        {
            return raycastHit.collider.gameObject;
        }
        return null;

    }

    void TryGrabObject(GameObject grabObject)
    {
        if (grabObject == null || !CanGrab(grabObject))
        {
            return;
        }

        grabbedObject = grabObject;
        grabbedObjectSize = grabbedObject.GetComponent<MeshRenderer>().bounds.size.magnitude;
    }

    void DropObject()
    {
        if (grabbedObject == null)
        {
            return;
        }

        if (grabbedObject.GetComponent<Rigidbody>() != null)
        {
            grabbedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        grabbedObject = null;
    }

    bool CanGrab(GameObject cand)
    {
        return cand.GetComponent<Rigidbody>() != null;
    }

}
