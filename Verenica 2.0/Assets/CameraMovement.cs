using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float zoomSpeed;
    private Camera cam;

    Keyboard keyboard;
    // Start is called before the first frame update
    void Start()
    {
        keyboard = Keyboard.current;
        cam = Camera.main;
    }

    private void FixedUpdate()
    {
        if (keyboard == null)
        {
            throw new System.Exception("No Keyboard");
        }

        if (keyboard.leftArrowKey.isPressed || keyboard.aKey.isPressed)
        {
            MoveLeft();
        }
        else if (keyboard.rightArrowKey.isPressed || keyboard.dKey.isPressed)
        {
            MoveRight();
        }

        if (keyboard.upArrowKey.isPressed || keyboard.wKey.isPressed)
        {
            ZoomIn();
        }
        else if (keyboard.downArrowKey.isPressed || keyboard.sKey.isPressed)
        {
            ZoomOut();
        }

    }

    private void MoveLeft()
    {
        cam.transform.position += Vector3.left * moveSpeed * Time.fixedDeltaTime;
    }

    private void MoveRight()
    {
        cam.transform.position += Vector3.right * moveSpeed * Time.fixedDeltaTime;
    }

    private void ZoomIn()
    {
        cam.orthographicSize -= zoomSpeed * Time.fixedDeltaTime;
    }

    private void ZoomOut()
    {
        cam.orthographicSize += zoomSpeed * Time.fixedDeltaTime;
    }
}
