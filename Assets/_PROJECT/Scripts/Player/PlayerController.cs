using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
private InputSystem_Actions characterInput;
private CharacterController controller;
private Vector2 moveInput;
private Vector3 verticalVelocity;

[SerializeField] GameObject pauseMenu; 
[SerializeField] float moveSpeed = 5f;
[SerializeField] private float jumpForce = 1f;
[SerializeField] private float gravity = -9.8f;


void Awake()
{
// Initialize the input system and character controller
characterInput = new InputSystem_Actions();
characterInput.Enable();
if (characterInput == null)
{
Debug.LogError("Input System Actions not found.");
}
// Get the CharacterController component       
controller = GetComponent<CharacterController>();
if (controller == null)
{
Debug.LogError("CharacterController component not found.");
}
// Lock the cursor to the center of the screen and hide it
Cursor.lockState = CursorLockMode.Locked;
Cursor.visible = false;
}

void Update()
{
// Ground check
if (controller != null)
{
if (controller.isGrounded && verticalVelocity.y < 0)
{
verticalVelocity.y = -2f;
}
else
{
verticalVelocity.y += gravity * Time.deltaTime;
}
// Horizontal movement
Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
// Apply movement
controller.Move((move * moveSpeed + verticalVelocity) * Time.deltaTime);
}
}

// Subscribe to input events when the script is enabled and unsubscribe when disabled
void OnEnable()
{
characterInput.Player.Move.performed += x => OnPlayerMove(x);
characterInput.Player.Move.canceled += x => OnPlayerMove(x);

characterInput.Player.Jump.performed += OnJump;

characterInput.UI.Pause.performed += OnTogglePauseMenu;
}

// Unsubscribe from input events to prevent memory leaks and unintended behavior  
void OnDisable()
{
characterInput.Player.Move.performed -= x => OnPlayerMove(x);
characterInput.Player.Move.canceled -= x => OnPlayerMove(x);

characterInput.Player.Jump.performed -= OnJump;

characterInput.UI.Pause.performed -= OnTogglePauseMenu;
}

// Method to handle player movement input
private void OnPlayerMove(InputAction.CallbackContext context)
{
Vector2 input = context.ReadValue<Vector2>();
moveInput = input.normalized * moveSpeed;
}

// Method to handle player jump input
private void OnJump(InputAction.CallbackContext context)
{
if (controller != null && controller.isGrounded)
{
verticalVelocity.y = jumpForce;           
}
}

// Method to handle pause menu toggle
private void OnTogglePauseMenu(InputAction.CallbackContext context)
{
if (pauseMenu != null)
{       
bool isPaused = !pauseMenu.activeSelf;
pauseMenu.SetActive(isPaused);

if (isPaused)
{             
Time.timeScale = 0f;              
Cursor.lockState = CursorLockMode.None;
Cursor.visible = true;
characterInput.Player.Disable();
                
}
else
{              
Time.timeScale = 1f;
Cursor.lockState = CursorLockMode.Locked;
Cursor.visible = false;
characterInput.Player.Enable();                
}
}

}


}



