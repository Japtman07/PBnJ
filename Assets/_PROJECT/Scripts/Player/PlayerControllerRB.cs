using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerRB : MonoBehaviour
{
private InputSystem_Actions characterInput;
private Rigidbody rb;

[Header("Movement Values")]
[SerializeField] float moveSpeed = 5f;
[SerializeField] private float jumpForce = 10f;
[SerializeField] private float wallForce = 2f;
[SerializeField] private float wallJumpForce = 1f;
[SerializeField] private float rotationDuration = 180f;
private float moveInput;
private bool isGrounded = false;
private bool rotateInput = false;


[Header("Wall Check")]
[SerializeField] private Collider pb;
public bool isOnWall = false;
public bool isWallJumping = false;

[Header("UI")]
[SerializeField] GameObject pauseMenu; 

void Awake()
{
characterInput = new InputSystem_Actions();
characterInput.Enable();
if (characterInput == null)
{
Debug.LogError("Input System Actions not found.");
}     
rb = GetComponent<Rigidbody>();
if (rb == null)
{
Debug.LogError("CharacterController component not found.");
}
Cursor.lockState = CursorLockMode.Locked;
Cursor.visible = false;
}

// ---- HANDLES PLAYER MOVEMENT AND WALL JUMPING ---
void FixedUpdate()
{
if (isOnWall && !isWallJumping)
{
rb.linearVelocity = Vector3.zero;
rb.useGravity = false;
}
else
{
rb.useGravity = true;
rb.linearVelocity = new Vector3(moveInput * moveSpeed, rb.linearVelocity.y, 0);
}
}

// ---- HANDLES SUBSCRIBING TO INPUT EVENTS ---
void OnEnable()
{
characterInput.Player.Move.performed += OnPlayerMove;
characterInput.Player.Move.canceled += OnPlayerMove;

characterInput.Player.Jump.performed += OnJump;
characterInput.Player.Jump.canceled += OnJump;

characterInput.UI.Pause.performed += OnTogglePauseMenu;

characterInput.Player.Rotate.performed += OnRotate;
characterInput.Player.Rotate.canceled += OnRotate;
}
    
// ---- HANDLES UNSUBSCRIBING TO INPUT EVENTS ---
void OnDisable()
{
characterInput.Player.Move.performed -= OnPlayerMove;
characterInput.Player.Move.canceled -= OnPlayerMove;

characterInput.Player.Jump.performed += OnJump;
characterInput.Player.Jump.canceled += OnJump;

characterInput.UI.Pause.performed -= OnTogglePauseMenu;

characterInput.Player.Rotate.performed += OnRotate;
characterInput.Player.Rotate.canceled += OnRotate;
}

// ---- HANDLES PLAYER MOVEMENT ---
private void OnPlayerMove(InputAction.CallbackContext context)
{
moveInput = context.ReadValue<Vector2>().x;
}

// ---- HANDLES PLAYER JUMP ---
private void OnJump(InputAction.CallbackContext context)
{
if(context.performed && isGrounded)
{
rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, 0);
rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
isOnWall = false; 
}
else if(context.performed && isOnWall)
{
isWallJumping = true;

Vector3 contactPoint = pb.ClosestPoint(transform.position);
Vector3 dir = (transform.position - contactPoint).normalized;

rb.linearVelocity = Vector3.zero;

StartCoroutine(WallJumpCooldown());

rb.AddForce((Vector3.up * wallJumpForce) + (dir * wallForce), ForceMode.Impulse);
}
}

private IEnumerator WallJumpCooldown()
{
isWallJumping = true;
yield return new WaitForSeconds(0.5f);
isWallJumping = false;
}


private void OnTriggerEnter(Collider other)
{
if(other.CompareTag("Ground"))
{
isGrounded = true;
Debug.Log("isGrounded true");  
}
}

private void OnTriggerExit(Collider other)
{
if(other.CompareTag("Ground"))
{
isGrounded = false;
Debug.Log("isGrounded false");  
}
}

// ---- HANDLES WALL COLLISION ---
private void OnCollisionStay(Collision collision)
{
if (collision.gameObject.CompareTag("Wall"))
{
if (collision.GetContact(0).thisCollider == pb)
{
isOnWall = true;
Debug.Log("isOnWall true");
}
}
}

private void OnCollisionExit(Collision collision)
{
if( collision.gameObject.CompareTag("Wall"))
{
isOnWall = false;
Debug.Log("isOnWall false");
}
}

// ---- HANDLES PLAYER ROTATION ---
private void OnRotate(InputAction.CallbackContext context)
{
if(context.performed && !rotateInput)
{
StartCoroutine(RotateSmooth(180f, rotationDuration));
}
}

private IEnumerator RotateSmooth(float angle, float duration)
{
rotateInput = true;
float timeElapsed = 0;
Quaternion startRotation = transform.rotation;
Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, 0, 180);

while (timeElapsed < duration)
{
transform.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed / duration);
timeElapsed += Time.deltaTime;
yield return null;
}
transform.rotation = targetRotation;
rotateInput = false;
}

// ---- HANDLES PAUSE MENU TOGGLE ---
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
 



