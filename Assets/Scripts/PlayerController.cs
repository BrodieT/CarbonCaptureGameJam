using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AttackModule))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField, Tooltip("The speed that the character will move"), Min(0)]
    private float movementSpeed = 5.0f;

    [Header("Jump & Grounded Settings")]
    [SerializeField, Tooltip("How long after pressing the jump button can the jump be triggered"), Min(0)]
    private float jumpPressRememberTime = 1.0f;
    [SerializeField, Tooltip("How much 'coyote' time the player has"), Min(0)]
    private float groundedRememberTime = 1.0f;
    [SerializeField, Tooltip("How big a jump the player can perform"), Min(0)]
    private float jumpHeight = 10.0f;
    [SerializeField, Tooltip("Where the player's feet are")]
    private Vector2 feetPosition = new Vector2(0, -0.5f);
    [SerializeField, Tooltip("The radius around the feet position that the ground is checked")]
    private float groundCheckRadius = 0.5f;

    [Header("Animations")]
    [SerializeField, Tooltip("The jump animation trigger")]
    private string jumpAnimation = "Jump";


    [Header("Gravity Settings")]
    [SerializeField, Tooltip("How much Gravity is applied to the player")]
    private float gravityForce = -9.8f;

    //The vertical velocity the player has
    private float verticalVelocity = 0.0f;
    //Tracks whether the player is grounded
    private bool isGrounded = false;

    public static PlayerController instance;



    //Jump timers
    private float jumpPressTimer = 0.0f;
    private float groundedTimer = 0.0f;

    //Component Caches
    private Animator playerAnimator = default; //used to animate the character
    private Rigidbody2D playerRigidbody = default; //used for character movement
    private AttackModule playerAttack = default; //used for ranged attacks

    private bool isControlling = true;
    private bool isInDialogue = false;
    private DialogueTrigger currentDialogueRef = default;
    private Vector3 velocityStore = new Vector3();
    public void EnterDialogue(DialogueTrigger dialogue)
    {
        isControlling = false;
        isInDialogue = true;
        currentDialogueRef = dialogue;
        velocityStore = playerRigidbody.velocity;
        playerRigidbody.velocity = Vector3.zero;
        playerRigidbody.Sleep();
    }

    public void ExitDialogue()
    {
        isControlling = true;
        isInDialogue = false;
        currentDialogueRef = null;
        playerRigidbody.WakeUp();
        playerRigidbody.velocity = velocityStore;
        velocityStore = Vector3.zero;
    }


    private void Awake()
    {
        instance = this;

        //Attempt to find the animator on this object
        if (transform.TryGetComponent<Animator>(out Animator myAnim))
        {
            playerAnimator = myAnim;
        }

        //Find the rigidbody on this object - doesnt need a TryGetComponent as Rigidbody2D is a required component
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAttack = GetComponent<AttackModule>();
    }



    private void Update()
    {
        if (isControlling)
        {
            //Decrement the timers
            groundedTimer -= Time.deltaTime;
            jumpPressTimer -= Time.deltaTime;
            //Check if the plauyer is grounded
            isGrounded = IsGrounded();
        }
    }

    private void FixedUpdate()
    {
        if (isControlling)
        {
            //Apply gravity
            verticalVelocity += gravityForce * Time.fixedDeltaTime;

            //If the player is grounded restart the grounded timer
            if (isGrounded)
            {
                groundedTimer = groundedRememberTime;

                //Clamp vertical velocity
                if (verticalVelocity < -2.0f)
                {
                    verticalVelocity = -2.0f;
                }
            }

            //if the jump button was pressed recently and the player was recently grounded
            if (jumpPressTimer > 0.0f && groundedTimer > 0.0f)
            {
                //perform the jump
                DoJump();
            }

            //Move the character to the right horizontally and apply the vertical velocity
            playerRigidbody.velocity = new Vector2(movementSpeed, verticalVelocity);
        }
    }

    //This function performs the jump
    private void DoJump()
    {
        //Stop the timers
        groundedTimer = 0.0f;
        jumpPressTimer = 0.0f;

        //Play the animation if applicable
        if (playerAnimator)
        {
            playerAnimator.SetTrigger(jumpAnimation);
        }

        //If grounded, increase vertical velocity by square root of the jump height * -2 * gravity
        verticalVelocity = Mathf.Sqrt(jumpHeight * -2.0f * gravityForce);
    }

    //This function checks an overlap circle at the players feet to check if they are grounded
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y) + feetPosition, groundCheckRadius).Length > 1;
    }

    //If the jump button is pressed then restart the jump press timer
    public void JumpInput(InputAction.CallbackContext context)
    {
        if(context.performed && isControlling)
        {
            jumpPressTimer = jumpPressRememberTime;
        }
    }

    public void AttackInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isControlling)
            {
                playerAttack.Attack();
            }
            else if(isInDialogue && currentDialogueRef)
            {
                currentDialogueRef.NextBeat();
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        //Show the grounded detection circle and change colour accordingly at runtime
        Gizmos.color = Color.red;

            Gizmos.color = IsGrounded() ? Color.green : Color.red;

        Gizmos.DrawWireSphere(transform.position + new Vector3(feetPosition.x, feetPosition.y, transform.position.z), groundCheckRadius);
    }
}
