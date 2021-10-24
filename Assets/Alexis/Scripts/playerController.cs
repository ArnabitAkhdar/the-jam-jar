using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    #region Private
    private Animator animator;

    private bool hasStoredRigidbody = false;
    private bool /*isFallingBackDown = false,*/ isJumping = false;
    private bool isMovingLeft = false, isMovingRight = false;

    // private float airTime = 2f;
    private float jumpingVelocity = 1f;
    private float movementVelocity = 1f;
    private float storedRbDrag = 0f, storedRbGravityScale = 0f;

    private RaycastHit2D groundHit;

    private Rigidbody2D rb;

    private SpriteRenderer spriteRenderer;

    // Velocity.x = Movement speed, Velocity.y = Gravity multiplier
    private Vector2 storedRbVelocity, velocity;
    private Vector2 updatedCameraPosition;
    #endregion

    #region Public
    public bool isAbleToMove = true;

    public Camera cam;

    public int jumpHeight = 8;
    public int movementSpeed = 0;

    public LayerMask layerMask;

    public Vector2 cameraBoundX, cameraBoundY;
    #endregion

    void Start() 
    { 
        animator = GetComponent<Animator>();
        animator.enabled = false;

        GetComponent<botanicalDexJournal>().loadDexJournalInformation();

        rb = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        updateCameraPosition();
    }

    private void FixedUpdate() 
    { 
        if (!isAbleToMove)
        {
            if(!hasStoredRigidbody)
            {
                animator.enabled = false;

                hasStoredRigidbody = true;

                storedRbDrag = rb.drag;
                storedRbGravityScale = rb.gravityScale;
                storedRbVelocity = rb.velocity;

                rb.constraints = RigidbodyConstraints2D.FreezePosition;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
        else 
        {
            if (!animator.enabled) { animator.enabled = true; }

            if(hasStoredRigidbody)
            {
                hasStoredRigidbody = false;
                
                rb.constraints = ~RigidbodyConstraints2D.FreezePosition;
                rb.drag = storedRbDrag;
                rb.gravityScale = storedRbGravityScale;
                rb.velocity = storedRbVelocity;
            }
            
            movement(); 
        }
    
        if(transform.position.y <= -50f) { SceneManager.LoadScene(1); }
    }

    private void jump()
    {
        if (!isJumping)
        {
            animator.SetBool("isJumping", true);

            isJumping = true;

            jumpingVelocity = jumpHeight;

            rb.drag = 0f;
            rb.gravityScale = 1f;

            rb.AddForce(new Vector2(0f, jumpingVelocity * 1.5f), ForceMode2D.Impulse);
        }
        else
        {
            if (rb.velocity.y > 0f) { rb.gravityScale = 1f; }
            else
            {
                if (groundHit.collider != null)
                {
                    animator.SetBool("isJumping", false);

                    if (rb.gravityScale != 1f) { rb.gravityScale = 1f; }

                    isJumping = false;
                }

                movementVelocity = movementSpeed / 4f;

                rb.gravityScale = 3f;
            }
        }
    }

    private void movement()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");

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

            movementVelocity = movementSpeed;
        }
        else
        {
            movementVelocity = 1f;

            if (rb.velocity.x != 0f) { if (!isJumping) { rb.drag = 6f; } }
            else { rb.drag = 0f; }
        }

        groundHit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, layerMask);

        if (!isJumping)
        {
            if (groundHit.collider != null) { /*Debug.Log("Hit!");*/ if (rb.gravityScale != 1f) { rb.gravityScale = 1f; } }
            else { /*Debug.Log("No Hit!");*/ if (!isJumping) { rb.gravityScale = 2f; } }

            if (horizontalMovement != 0) 
            {
                if (rb.velocity.x > 5f || rb.velocity.x < -5f) 
                { 
                    animator.SetBool("isRunning", true);

                    if (animator.GetBool("isWalking")) { animator.SetBool("isWalking", false); }
                }
                else 
                {
                    animator.SetBool("isWalking", true);

                    if (animator.GetBool("isRunning")) { animator.SetBool("isRunning", false); }
                }
            }
            else { animator.SetBool("isRunning", false); animator.SetBool("isWalking", false); }
        }

        if ((groundHit.collider != null && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Space))) || isJumping) { jump(); }

        rb.AddForce(new Vector2(horizontalMovement * movementVelocity, 0f) * Time.fixedDeltaTime, ForceMode2D.Impulse);

        updateCameraPosition();
    }

    private void updateCameraPosition() 
    {
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z);

        if (cam.transform.position.x >= cameraBoundX.y) { updatedCameraPosition.x = cameraBoundX.y; }
        else if (cam.transform.position.x <= cameraBoundX.x) { updatedCameraPosition.x = cameraBoundX.x; }
        else { updatedCameraPosition.x = transform.position.x; }

        if (cam.transform.position.y >= cameraBoundY.y) { updatedCameraPosition.y = cameraBoundY.y; }
        else if (cam.transform.position.y <= cameraBoundY.x) { updatedCameraPosition.y = cameraBoundY.x; }
        else { updatedCameraPosition.y = transform.position.y; }

        cam.transform.position = new Vector3(updatedCameraPosition.x, updatedCameraPosition.y, cam.transform.position.z); 
    }

    public void updateVelocityY(int _velocityY) { velocity.y = _velocityY; }

    // User Interface Functions
    // public void updateGravityMultiplier(TMP_InputField _inputField) { if (_inputField.text != "" && float.Parse(_inputField.text) >= 0f && float.Parse(_inputField.text) <= 1f) { gravityMultiplier = float.Parse(_inputField.text); } }
    public void updateMovementSpeed(TMP_InputField _inputField) { if (_inputField.text != "" && int.Parse(_inputField.text) >= 0 && int.Parse(_inputField.text) <= 16) { movementSpeed = int.Parse(_inputField.text); } }
}
