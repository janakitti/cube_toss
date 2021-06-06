using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabCube : MonoBehaviour {

    public Material defaultMaterial;
    public Material selectedMaterial;
    public Material multiSelectedMaterial;

    // Offset from where mouse was clicked in 3D space
    private Vector3 mouseOffset;
    // z coordinate of the Cube
    private float zCube;

    // Keeps track of previous Cube position for creating a throw vector
    private Vector3 prevCubePos;

    // Indicates if a Cube being grabbed on its own
    private bool isGrabbed;
    // Indicates if a Cube was just released
    private bool isReleased;

    // Indicates if Cube is part of a multi selection
    private bool isMultiSelected;
    // Indicates if a multi-selected Cube is being grabbed
    private bool isMultiGrabbed = false;


    public void SetIsMultiSelected(bool isMultiSelected)
    {
        this.isMultiSelected = isMultiSelected;
    }
    
    private void OnMouseDown()
    {
        // Capture z coordinate of Cube and difference between Cube position and mouse click position
        zCube = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mouseOffset = gameObject.transform.position - CalcMouseToWorldPos();

        isGrabbed = true;
    }

    // Calculate the point in 3D space based on mouse position on screen and z coordinate of Cube
    private Vector3 CalcMouseToWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCube;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    
    private void OnMouseUp()
    {
        isReleased = true;
    }

    void Update () {
        if (isMultiSelected)
        {
            GetComponent<Renderer>().material = multiSelectedMaterial;
            // When user first clicks after multi selecting
            if (Input.GetMouseButtonDown(0))
            {
                zCube = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
                mouseOffset = gameObject.transform.position - CalcMouseToWorldPos();
                isMultiGrabbed = true;
            }
            // While multi selection is being dragged
            if (Input.GetMouseButton(0))
            {
                // Record current Cube position before updating
                prevCubePos = gameObject.transform.position;
                transform.position = CalcMouseToWorldPos() + mouseOffset;
            }
            // When multi selection is released
            if (Input.GetMouseButtonUp(0) && isMultiGrabbed)
            {
                GetComponent<Renderer>().material = defaultMaterial;
                Vector3 throwDirection = gameObject.transform.position - prevCubePos;

                // Divide by deltaTime to ensure speeds are not dependent on framerate
                float speed = throwDirection.magnitude / Time.deltaTime;
                gameObject.GetComponent<Rigidbody>().velocity = speed * throwDirection.normalized;
                isMultiSelected = false;
                isMultiGrabbed = false;
            }
        } else
        {
            // When single Cube is released
            if (isReleased)
            {
                Vector3 throwDirection = gameObject.transform.position - prevCubePos;

                // Divide by deltaTime to ensure speeds are not dependent on framerate
                float speed = throwDirection.magnitude / Time.deltaTime;
                gameObject.GetComponent<Rigidbody>().velocity = speed * throwDirection.normalized;
                isReleased = false;
                isGrabbed = false;
            }
            else
            {
                // When single Cube is being dragged
                if (isGrabbed)
                {
                    prevCubePos = gameObject.transform.position;
                    transform.position = CalcMouseToWorldPos() + mouseOffset;
                    GetComponent<Renderer>().material = selectedMaterial;
                }
                else
                {
                    GetComponent<Renderer>().material = defaultMaterial;
                }

            }
        }

    }
}
