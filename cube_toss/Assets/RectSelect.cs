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

	// Use this for initialization
	void Start () {
        cubes = FindObjectsOfType<GrabCube>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("r"))
        {
            isSelectMode = !isSelectMode;
            mouseLook.SetIsLocked(!mouseLook.GetIsLocked());
            Debug.Log(isSelectMode);
        }
        if (isSelectMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPos = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
                mouseLook.SetIsLocked(false);
                isSelectMode = !isSelectMode;
                CaptureSelectionBox();
            }
            if (Input.GetMouseButton(0))
            {
                UpdateSelectionBox(Input.mousePosition);
            }
        }

	}

    void UpdateSelectionBox(Vector2 curMousePos)
    {
        if(!selectionBox.gameObject.activeInHierarchy)
        {
            selectionBox.gameObject.SetActive(true);
        }

        float width = curMousePos.x - startPos.x;
        float height = curMousePos.y - startPos.y;

        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectionBox.anchoredPosition = startPos + new Vector2(width / 2, height / 2);
    }

    void CaptureSelectionBox()
    {
        selectionBox.gameObject.SetActive(false);
        Vector2 botLeft = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
        Vector2 topRight = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);

        foreach (GrabCube cube in cubes)
        {
            Vector3 screenPos = camera.WorldToScreenPoint(cube.transform.position);
            if (screenPos.x > botLeft.x && screenPos.x < topRight.x && screenPos.y > botLeft.y && screenPos.y < topRight.y)
            {
                cube.SetIsMultiSelected(true);
            } else
            {
                cube.SetIsMultiSelected(false);
            }
        }
    }
}
