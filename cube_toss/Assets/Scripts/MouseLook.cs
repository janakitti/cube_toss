using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {

    public float mouseSensitivity = 100f;
    public Transform playerBody;
    private bool isLocked = false;

    float xRotation = 0f;

    public bool GetIsLocked()
    {
        return isLocked;
    }

    public void SetIsLocked(bool isLocked)
    {
        this.isLocked = isLocked;

        // Lock/unlock cursor on screen
        if (this.isLocked)
        {
            Cursor.lockState = CursorLockMode.None;
        } else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	void Update () {

        if (!isLocked)
        {
            // Head rotation
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
