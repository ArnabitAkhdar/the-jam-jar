using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playerController : MonoBehaviour
{
    #region Private
    private Animator animator;

    private bool isJumping = false, isMoving = false;

    // 0 = Down, 1 = Left, 2 = Right, 3 = Up
    private int currentDirection = -1;

    private Vector3 movementVector;
    #endregion

    #region Public
    public bool isDiagonalMovementEnabled = false;

    public float gravityMultiplier = .5f;

    public int movementSpeed = 0;
    #endregion

    void Start() { animator = GetComponent<Animator>(); }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) { jump(); }

        movement();
    }

    private void jump()
    {
        if (!isJumping) 
        {
            animator.SetFloat("gravityMultiplier", 1 + (1 - gravityMultiplier));
            animator.SetBool("isJumping", true);

            isJumping = true;
        }
    }

    private void movement()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        if(!isJumping)
        {
            if (horizontalMovement != 0 || verticalMovement != 0) { animator.SetBool("isMoving", true); }
            else { animator.SetBool("isMoving", false); }
        }

        if (!isDiagonalMovementEnabled) 
        {
            // Down
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) { movementVector = new Vector3(0f, verticalMovement, 0f); }
            // Left
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) { movementVector = new Vector3(horizontalMovement, 0f, 0f); }
            // Right
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) { movementVector = new Vector3(horizontalMovement, 0f, 0f); }
            // Up
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) { movementVector = new Vector3(0f, verticalMovement, 0f); }
        }
        else { movementVector = new Vector3(horizontalMovement, verticalMovement, 0f); }

        if (!isJumping) { movementVector = Vector3.ClampMagnitude(movementVector * ((float)movementSpeed * Time.deltaTime), 1f); }
        else { movementVector = Vector3.ClampMagnitude(movementVector * (((float)movementSpeed * Time.deltaTime) * gravityMultiplier), 1f); }

        transform.position += movementVector;
    }

    public void endAnimation(string _case) { switch (_case) 
        { 
            case "Jump":
                animator.SetFloat("gravityMultiplier", 1);
                animator.SetBool("isJumping", false);

                isJumping = false;
                break; 
        } 
    }

    // User Interface Functions
    public void disableOrEnableDiagonalMovement(bool _value) { isDiagonalMovementEnabled = _value; }
    public void updateGravityMultiplier(TMP_InputField _inputField) { if (_inputField.text != "" && float.Parse(_inputField.text) >= 0f && float.Parse(_inputField.text) <= 1f) { gravityMultiplier = float.Parse(_inputField.text); } }
    public void updateMovementSpeed(TMP_InputField _inputField) { if (_inputField.text != "" && int.Parse(_inputField.text) >= 0 && int.Parse(_inputField.text) <= 16) { movementSpeed = int.Parse(_inputField.text); } }
}
