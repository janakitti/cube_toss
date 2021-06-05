using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabCube : MonoBehaviour {

    public Material defaultMaterial;
    public Material selectedMaterial;

    private Vector3 mOffset;
    private float mZCoord;

    private Vector3 prevGrabPos;

    private bool isGrabbed;
    private bool isReleased;
    private bool isMultiSelected;
    private bool isMultiGrabbed = false;

    public void SetIsMultiSelected(bool b)
    {
        isMultiSelected = b;
    }
    
    private void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();

        isGrabbed = true;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    
    private void OnMouseUp()
    {
        isReleased = true;
    }
    
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update () {
        /*
        if (isReleased)
        {
            Vector3 endPos = gameObject.transform.position - prevGrabPos;
            Debug.Log(endPos);
            float speed = endPos.magnitude / Time.deltaTime;
            gameObject.GetComponent<Rigidbody>().velocity = speed * endPos.normalized;
            isReleased = false;
            isGrabbed = false;
        } else
        {
            if (isGrabbed)
            {

                prevGrabPos = gameObject.transform.position;

                transform.position = GetMouseWorldPos() + mOffset;
                GetComponent<Renderer>().material = selectedMaterial;
            }
            else
            {
                GetComponent<Renderer>().material = defaultMaterial;
            }

        }
        */
        if (isMultiSelected)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isMultiGrabbed)
                {
                    Debug.Log("RELEASE");
                } else
                {
                    Debug.Log("MULTI GRAB");
                    mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
                    mOffset = gameObject.transform.position - GetMouseWorldPos();
                }
            }
            if (Input.GetMouseButton(0))
            {
                transform.position = GetMouseWorldPos() + mOffset;
            }
        }

    }
}
