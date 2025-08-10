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
    private bool canMove;

    [SerializeField] private GameInput gameInput;
    private Rigidbody rb;

    private float playerRadius = 0.7f;
    private float playerHeight = 2f;
    private float castDistance;

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

        castDistance = Time.deltaTime * move_speed;
        Vector3 point1 = transform.position;
        Vector3 point2 = transform.position + Vector3.up * playerHeight;
        canMove = !Physics.CapsuleCast(point1, point2, playerRadius, InputVector, castDistance);
    }

    private void FixedUpdate()
    {
        if (canMove)
            rb.MovePosition(rb.position + InputVector * Time.fixedDeltaTime * move_speed);

        if (isWalking) 
            transform.forward = Vector3.Lerp(transform.forward, (InputVector), Time.fixedDeltaTime * rotate_speed);

        if (isJump && IsGrounded())
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);



        // Debug line

        Debug.DrawRay(transform.position, Vector3.down * .3f, Color.red);
        if (IsGrounded())
            Debug.Log("IS GROUNDED");

    }

    public bool IsWalking() { return isWalking; }
    public bool IsJump() { return isJump; }
    private bool IsGrounded() { return Physics.Raycast(transform.position, Vector3.down, .3f); }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Vector3 point1 = transform.position;
        Vector3 point2 = transform.position + Vector3.up * playerHeight;
        Vector3 dir = InputVector.normalized * castDistance;

        Gizmos.color = Color.yellow;
        DrawCapsule(point1, point2, playerRadius, dir);
    }

    private void DrawCapsule(Vector3 point1, Vector3 point2, float radius, Vector3 offset)
    {
        
        Gizmos.DrawWireSphere(point1, radius);
        Gizmos.DrawWireSphere(point2, radius);

        
        Gizmos.DrawWireSphere(point1 + offset, radius);
        Gizmos.DrawWireSphere(point2 + offset, radius);

        
        Gizmos.DrawLine(point1, point1 + offset);
        Gizmos.DrawLine(point2, point2 + offset);
    }
}
