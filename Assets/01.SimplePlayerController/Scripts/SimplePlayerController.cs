using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerController : MonoBehaviour
{

    public CharacterController controller;
    public float move_speed = 1f;
    public float jumpForce = 10f;
    public float gravity = -9.8f;

    private float yVelocity = 0f;

    void Update()
    {
        //Horizontal movement
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        direction = gameObject.transform.TransformDirection(direction);
        Vector3 movement = direction * move_speed * Time.deltaTime;

        //Gravity & vertical movement
        if (controller.isGrounded) {
            yVelocity = 0f;
            if (Input.GetAxisRaw("Jump") > 0f) {
                yVelocity = jumpForce;
            }
        }
        yVelocity += gravity * Time.deltaTime;
        movement.y = yVelocity * Time.deltaTime;

        if (direction.magnitude >= 0.0f) {
            controller.Move(movement);
        }
    }
}
