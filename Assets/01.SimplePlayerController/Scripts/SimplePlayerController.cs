using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerController : MonoBehaviour
{

    public CharacterController controller;
    public float move_speed = 1f;

    void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;
        if (direction.magnitude >= 0.1f) {
            controller.Move(direction * move_speed * Time.deltaTime);
        }
    }
}
