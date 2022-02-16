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

    private void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector3 angles = gameObject.transform.eulerAngles;
        float mouseX = Input.GetAxis("Mouse X") * -rotate_speed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotate_speed * Time.deltaTime;
        angles += new Vector3(mouseY, mouseX, 0f);
        if (angles.y < 0f) {
            angles.y += 360f;
        }
        else if (angles.y > 360f) {
            angles.y -= 360f;
        }
        if (angles.x < minPitch) {
            angles.x = minPitch;
        }
        else if (angles.x > maxPitch) {
            angles.x = maxPitch;
        }
        gameObject.transform.eulerAngles = angles;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        float camera_z = camera.localPosition.z;
        camera_z += scroll * zoomSpeed;
        camera_z = Mathf.Clamp(camera_z, -maxZoom, -minZoom);
        camera.localPosition = new Vector3(0f, 0f, camera_z);
    }

}
