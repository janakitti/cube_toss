using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectSelect : MonoBehaviour {
    public RectTransform selectionBox;
    public Camera camera;
    public MouseLook mouseLook;

    private Vector2 startPos;
    private GrabCube[] cubes;
    private bool isSelectMode = false;

	void Update () {
        if (Input.GetKeyDown("r"))
        {
            isSelectMode = !isSelectMode;
            mouseLook.SetIsLocked(!mouseLook.GetIsLocked());
        }
        if (isSelectMode)
        {
            // Record top left corner of rect on first click
            if (Input.GetMouseButtonDown(0))
            {
                startPos = Input.mousePosition;
            }
            // Update rect on drag
            if (Input.GetMouseButton(0))
            {
                UpdateSelectionBox(Input.mousePosition);
            }
            // Capture rect on mouse release
            if (Input.GetMouseButtonUp(0))
            {
                mouseLook.SetIsLocked(false);
                isSelectMode = !isSelectMode;
                CaptureSelectionBox();
            }
        }
	}

    // Resize the rect based on start and current mouse positions
    void UpdateSelectionBox(Vector2 curMousePos)
    {
        if(!selectionBox.gameObject.activeInHierarchy)
        {
            selectionBox.gameObject.SetActive(true);
        }

        float width = curMousePos.x - startPos.x;
        float height = curMousePos.y - startPos.y;

        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));

        // / 2 to account for achor being in middle of rect
        selectionBox.anchoredPosition = startPos + new Vector2(width / 2, height / 2);
    }

    // Record the dimensions of the rect and search for Cubes within
    void CaptureSelectionBox()
    {
        cubes = FindObjectsOfType<GrabCube>();
        selectionBox.gameObject.SetActive(false);
        Vector2 botLeft = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
        Vector2 topRight = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);

        foreach (GrabCube cube in cubes)
        {
            Vector3 screenPos = camera.WorldToScreenPoint(cube.transform.position);
            if (screenPos.x > botLeft.x && 
                screenPos.x < topRight.x && 
                screenPos.y > botLeft.y && 
                screenPos.y < topRight.y)
            {
                cube.SetIsMultiSelected(true);
            } else
            {
                cube.SetIsMultiSelected(false);
            }
        }
    }
}
