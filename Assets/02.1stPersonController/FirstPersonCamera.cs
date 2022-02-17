using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{

    public float rotate_speed = 1f;
    public float minPitch = 0f;
    public float maxPitch = 89f;

    private void Awake() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Pitch, only rotates camera
        Vector3 camAngles = gameObject.transform.eulerAngles;
        if (camAngles.x > 180f) {
            camAngles.x -= 360f;
        }
        float mouseY = Input.GetAxis("Mouse Y") * -rotate_speed * Time.deltaTime;
        camAngles.x = Mathf.Clamp(camAngles.x+mouseY, minPitch, maxPitch);
        gameObject.transform.eulerAngles = camAngles;

        //Yaw, rotates player model
        Vector3 bodyAngles = gameObject.transform.parent.eulerAngles;
        float mouseX = Input.GetAxis("Mouse X") * rotate_speed * Time.deltaTime;
        bodyAngles += new Vector3(0f, mouseX, 0f);
        if (bodyAngles.y < 0f) {
            bodyAngles.y += 360f;
        }
        else if (bodyAngles.y > 360f) {
            bodyAngles.y -= 360f;
        }
        gameObject.transform.parent.eulerAngles = bodyAngles;
    }

}
