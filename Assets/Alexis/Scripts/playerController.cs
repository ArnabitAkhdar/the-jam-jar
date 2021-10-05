using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playerController : MonoBehaviour
{
    #region Private
    private Animator animator;

    private bool /*isFallingBackDown = false,*/ isJumping = false;
    private bool isMovingLeft = false, isMovingRight = false;

    // private float airTime = 2f;
    private float jumpingVelocity = 1f;
    private float movementVelocity = 1f;

    private RaycastHit2D groundHit;

    private Rigidbody2D rb;

    private SpriteRenderer spriteRenderer;

    // Velocity.x = Movement speed, Velocity.y = Gravity multiplier
    private Vector2 velocity;
    #endregion

    #region Public
    public bool isAbleToMove = true;

    public Camera cam;

    public int jumpHeight = 8;
    public int movementSpeed = 0;
    #endregion

    void Start() 
    { 
        animator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() { if (isAbleToMove) { movement(); } }

    private void jump()
    {
        if (!isJumping)
        {
            animator.SetBool("isJumping", true);

            isJumping = true;

            jumpingVelocity = jumpHeight;

            movementVelocity = movementSpeed * .25f;

            rb.drag = 0f;
            rb.gravityScale = 1f;

            rb.AddForce(new Vector2(0f, jumpingVelocity * 1.5f), ForceMode2D.Impulse);
        }
        else
        {
            if (groundHit.collider != null)
            {
                animator.SetBool("isJumping", false);

                if(rb.gravityScale != 1f) { rb.gravityScale = 1f; }

                isJumping = false;
            }
            else 
            { 
                if (rb.velocity.y > 0f) { rb.gravityScale = 1f; } 
                else 
                {
                    movementVelocity = movementSpeed * 3f;

                    rb.gravityScale = 2f; 
                }
            }
        }
    }

    private void movement()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");

        int layerMask = 1 << 8; layerMask = ~layerMask;

        if (horizontalMovement != 0)
        {
            if (horizontalMovement > 0)
            {
                if (isMovingLeft && rb.velocity.x < 0f) { if (!isJumping) { rb.drag = 8f; } }
                else
                {
                    isMovingLeft = false;

                    rb.drag = 0f;
                }

                isMovingRight = true;

                spriteRenderer.flipX = false;
            }
            else if (horizontalMovement < 0)
            {
                if (isMovingRight && rb.velocity.x > 0f) { if (!isJumping) { rb.drag = 8f; } }
                else
                {
                    isMovingRight = false;

                    rb.drag = 0f;
                }

                isMovingLeft = true;

                spriteRenderer.flipX = true;
            }

            // movementVelocity = movementSpeed * 1.5f;

            if (!isJumping) { movementVelocity = movementSpeed * 1.5f; }
        }
        else
        {
            movementVelocity = 1f;

            if (rb.velocity.x != 0f) { if (!isJumping) { rb.drag = 6f; } }
            else { rb.drag = 0f; }
        }

        groundHit = Physics2D.Raycast(transform.position, Vector2.down, 2.75f, layerMask);

        if (!isJumping)
        {
            if (groundHit.collider != null) { if (rb.gravityScale != 1f) { rb.gravityScale = 1f; } }
            else { if (!isJumping) { rb.gravityScale = 4f; } }

            if (horizontalMovement != 0) { animator.SetBool("isMoving", true); }
            else { animator.SetBool("isMoving", false); }
        }

        if ((groundHit.collider != null && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Space))) || isJumping) { jump(); }

        rb.AddForce(new Vector2(horizontalMovement * movementVelocity, 0f) * Time.fixedDeltaTime, ForceMode2D.Impulse);

        updateCameraPosition();
    }

    private void updateCameraPosition() { cam.transform.position = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z); }

    public void updateVelocityY(int _velocityY) { velocity.y = _velocityY; }

    // User Interface Functions
    // public void updateGravityMultiplier(TMP_InputField _inputField) { if (_inputField.text != "" && float.Parse(_inputField.text) >= 0f && float.Parse(_inputField.text) <= 1f) { gravityMultiplier = float.Parse(_inputField.text); } }
    public void updateMovementSpeed(TMP_InputField _inputField) { if (_inputField.text != "" && int.Parse(_inputField.text) >= 0 && int.Parse(_inputField.text) <= 16) { movementSpeed = int.Parse(_inputField.text); } }
}
