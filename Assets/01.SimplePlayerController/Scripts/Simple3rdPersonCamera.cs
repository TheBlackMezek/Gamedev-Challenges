using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simple3rdPersonCamera : MonoBehaviour
{

    public float rotate_speed = 1f;
    public float minPitch = 0f;
    public float maxPitch = 89f;
    public float zoomSpeed = 1f;
    public float minZoom = 2f;
    public float maxZoom = 8f;
    public Transform camera;

    private void Awake() {
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    void Update()
    {
        //Pitch, only rotates camera
        Vector3 camAngles = gameObject.transform.eulerAngles;
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

        //Camera zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        float camera_z = camera.localPosition.z;
        camera_z += scroll * zoomSpeed;
        camera_z = Mathf.Clamp(camera_z, -maxZoom, -minZoom);
        camera.localPosition = new Vector3(0f, 0f, camera_z);
    }

}
