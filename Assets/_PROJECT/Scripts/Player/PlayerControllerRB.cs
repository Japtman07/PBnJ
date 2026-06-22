using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerRB : MonoBehaviour
{
    
private InputSystem_Actions characterInput;
private Rigidbody rb;
private float moveInput;
private bool isGrounded = false;

[Header("Movement Values")]
[SerializeField] float moveSpeed = 5f;
[SerializeField] private float jumpForce = 10f;

[Header("UI")]
[SerializeField] GameObject pauseMenu; 


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
rb = GetComponent<Rigidbody>();
if (rb == null)
{
Debug.LogError("CharacterController component not found.");
}
// Lock the cursor to the center of the screen and hide it
Cursor.lockState = CursorLockMode.Locked;
Cursor.visible = false;
}

void FixedUpdate()
{
if (rb == null) return;
// Set horizontal velocity based on input (preserve existing y and z velocity)
rb.linearVelocity = new Vector3(moveInput * moveSpeed, rb.linearVelocity.y, 0);
}

// Subscribe to input events when the script is enabled and unsubscribe when disabled
void OnEnable()
{
characterInput.Player.Move.performed += OnPlayerMove;
characterInput.Player.Move.canceled += OnPlayerMove;

characterInput.Player.Jump.performed += OnJump;

characterInput.UI.Pause.performed += OnTogglePauseMenu;
}

// Unsubscribe from input events to prevent memory leaks and unintended behavior  
void OnDisable()
{
characterInput.Player.Move.performed -= OnPlayerMove;
characterInput.Player.Move.canceled -= OnPlayerMove;

characterInput.Player.Jump.performed -= OnJump;

characterInput.UI.Pause.performed -= OnTogglePauseMenu;
}

// Method to handle player movement input
private void OnPlayerMove(InputAction.CallbackContext context)
{
// // Reads the 1D Axis value (Left/Right)
moveInput = context.ReadValue<Vector2>().x;
}

// Method to handle player jump
private void OnJump(InputAction.CallbackContext context)
{
if(isGrounded)
{
rb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse); 
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

private void OnTriggerEnter(Collider other)
{
if(other.CompareTag("Ground"))
{
isGrounded = true;
Debug.Log("play is grounded");
}
}

private void OnTriggerExit(Collider other)
{
if(other.CompareTag("Ground"))
{
isGrounded = false;
Debug.Log("play is not grounded");
}
}


}



