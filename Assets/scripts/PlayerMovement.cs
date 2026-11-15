using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{ 
    [SerializeField] float movementSpeed = 15f;
    [SerializeField] float sprintMultiplier = 1.5f; 
    [SerializeField] float gravity = -50f;
    [SerializeField] float jumpHeight = 4f;
    float verticalVelocity; 
    float totalSpeed;
    //Boolean jumpPressed; 

    PlayerInput playerInput;
    CharacterController controller;
    //
    Vector3 move; 

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>(); 
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        handleMovement();
        handleJumping(); 
    }

    void handleMovement()
    {
        Vector2 inputValue = playerInput.actions["Locomotion"].ReadValue<Vector2>();
        InputAction sprintAction = playerInput.actions["Sprint"];
    

        totalSpeed = movementSpeed;
        if (sprintAction.WasPressedThisFrame())
        {
            StartCoroutine(dash());    
        }

        controller.Move(movingController() * totalSpeed * handleDash() * Time.deltaTime);
    }

    private IEnumerator dash()
    {
        totalSpeed = movementSpeed * sprintMultiplier * 2;
        controller.Move(movingController() * totalSpeed * handleDash() * Time.deltaTime);
        yield return new WaitForSeconds(1); 
    }

    private Vector3 movingController()
    {
        Vector2 inputValue = playerInput.actions["Locomotion"].ReadValue<Vector2>();
        Vector3 cameraRight = Camera.main.transform.right;
        cameraRight.y = 0f;
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0f;

        Vector3 motion = cameraRight * inputValue.x + cameraForward * inputValue.y;
        motion.y = verticalVelocity / movementSpeed;
        return motion;
    }

    void handleJumping()
    {   
        InputAction jumpAction = playerInput.actions["Jump"];

        if (controller.isGrounded)
        {   
            verticalVelocity = -2f;
            if (jumpAction.WasPressedThisFrame())
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
    }

    float handleDash()
    {
        float dashVelocity = 1;
        
        

        return dashVelocity;
    }
}
