using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool isWalking;
    [SerializeField] private bool isJump;

    [SerializeField] private float move_speed = 5.4f;
    [SerializeField] private float rotate_speed = 24f;
    [SerializeField] private float jumpForce = 3f;

    [SerializeField] private GameInput gameInput;
    private Rigidbody rb;

    private Vector3 InputVector;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    private void Update()
    {
        isJump = gameInput.IsJumpPressed();
        InputVector = gameInput.getNormalizedVector();
        isWalking = (InputVector != Vector3.zero);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + InputVector * Time.fixedDeltaTime * move_speed);
        if (isWalking) transform.forward = Vector3.Lerp(transform.forward, (InputVector), Time.fixedDeltaTime * rotate_speed);

        if (isJump && IsGrounded())
        {
            // transform.forward = InputVector;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }



        // Debug line
        Debug.DrawRay(transform.position, Vector3.down * .3f, Color.red);
        if (IsGrounded())
            Debug.Log("IS GROUNDED");

    }

    public bool IsWalking() { return isWalking; }
    public bool IsJump() { return isJump; }
    private bool IsGrounded() { return Physics.Raycast(transform.position, Vector3.down, .3f); }
}
