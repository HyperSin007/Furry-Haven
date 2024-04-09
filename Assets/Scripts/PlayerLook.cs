using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    public Camera playerCamera;
    private float xRotation = 0f;
    public float xSensitivity = 5f;
    public float ySensitivity = 5f;
    // Start is called before the first frame update //
    public void ProcessLook(Vector2 lookInput)
    {
        float mouseX = lookInput.x;
        float mouseY = lookInput.y;
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}
