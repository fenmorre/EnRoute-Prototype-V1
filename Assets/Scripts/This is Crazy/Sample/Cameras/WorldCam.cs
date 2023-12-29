using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCam : MonoBehaviour
{
    public Transform cameraTransform;
    public float fastSpeed;
    public float normalSpeed;
    public float moveSpeed;
    public float moveTime;
    public float rotationAmount;
    public Vector3 zoomAmount;

    public Vector3 newPosition;
    public Quaternion newRotation;
    public Vector3 newZoom;

    void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;

        // Unlock and show cursor at the start
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        HandleMovementInput();
    }

    // Move using WASD
    void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = fastSpeed;
        }
        else
        {
            moveSpeed = normalSpeed;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += (transform.forward * moveSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += (transform.forward * -moveSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += (transform.right * moveSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += (transform.right * -moveSpeed);
        }

        // Rotation
        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }

        // Zooming
        if (Input.GetKey(KeyCode.R))
        {
            newZoom += zoomAmount;
        }
        if (Input.GetKey(KeyCode.F))
        {
            newZoom -= zoomAmount;
        }

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * moveTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * moveTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * moveTime);
    }

    // Toggle cursor visibility and lock state when switching to/from the World Camera
    public void ToggleCursorVisibility(bool visible, bool lockState)
    {
        Cursor.visible = visible;
        Cursor.lockState = lockState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}


