using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardController : MonoBehaviour
{
    public CharacterController controller;
    InputAction moveAction;
    InputAction jumpAction;


    //Movement variables
    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;

    //Ground check variables
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    void Start() {
        //Find references to actions
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    //Update called once per frame, so movement is based on framerate? May find an alternative to this later - not preferrable.
    void Update() {
        //Check if we hit the ground and reset falling velocity, so it does not carry over next time we are airborn
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        Vector2 moveValue = moveAction.ReadValue<Vector2>();

        Vector3 move = transform.right * moveValue.x + transform.forward * moveValue.y;
        controller.Move(move * speed * Time.deltaTime);

        //If jump button is pressed and player is grounded, jump
        if (jumpAction.IsPressed() && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
