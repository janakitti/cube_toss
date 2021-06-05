using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabCube : MonoBehaviour {

    private Vector3 mOffset;
    private float mZCoord;

    private Vector3 prevGrabPos;

    private bool isGrabbed;

    /*
    private void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
        
    }*/

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    /*
    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mOffset;
    }

    private void OnMouseUp()
    {
        Vector3 endPos = gameObject.transform.position - prevGrabPos;
        float speed = (gameObject.transform.position - prevGrabPos).magnitude / Time.deltaTime;
        gameObject.GetComponent<Rigidbody>().velocity = speed * endPos.normalized;
    }*/

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            if (isGrabbed)
            {

                isGrabbed = false;
                Vector3 endPos = gameObject.transform.position - prevGrabPos;
                float speed = (gameObject.transform.position - prevGrabPos).magnitude / Time.deltaTime;
                gameObject.GetComponent<Rigidbody>().velocity = speed * endPos.normalized;
            } else
            {
                isGrabbed = true;
                mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
                mOffset = gameObject.transform.position - GetMouseWorldPos();
            }
        } 
        if (isGrabbed)
        {

            prevGrabPos = gameObject.transform.position;

            transform.position = GetMouseWorldPos() + mOffset;
        }
        
       

    }
}
