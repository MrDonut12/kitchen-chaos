using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5.4f;
    [SerializeField] private float rotateSpeed = 24f;
    [SerializeField] private float jumpForce = 3f;

    [Header("Collision Settings")]
    [SerializeField] private float playerRadius = 0.7f;
    [SerializeField] private float playerHeight = 2.25f;
    [SerializeField] private float groundCheckDistance = 0.3f;
    [SerializeField] private float interactDistance = 1f;

    private bool isWalking;
    private bool isJump;
    private bool canMove;

    private Rigidbody rb;
    private Vector3 inputVector;
    private Vector3 lastInputVector;

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : System.EventArgs
    {
        public ClearCounter selectedCounter;
    }

    [Header("Other")]
    [SerializeField] private ClearCounter selectedCounter;
    [SerializeField] private GameInput gameInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;


        gameInput.OneInteractionEvent += OnInteractPerformed;
    }

    private void Update()
    {

        isJump = gameInput.IsJumpPressed();
        inputVector = gameInput.getNormalizedVector();
        isWalking = inputVector != Vector3.zero;

        if (isWalking)
            lastInputVector = inputVector;

        float castDistance = Time.deltaTime * moveSpeed;
        Vector3 point1 = transform.position;
        Vector3 point2 = transform.position + Vector3.up * playerHeight;
        canMove = !Physics.CapsuleCast(point1, point2, playerRadius, inputVector, castDistance);
        if (!canMove)
        {
            //attemp only X movement
            Vector3 moveDirX = new Vector3(inputVector.x, 0, 0).normalized;
            canMove = moveDirX != Vector3.zero && !Physics.CapsuleCast(point1, point2, playerRadius, moveDirX, castDistance);
            if (canMove)
            {
                inputVector = moveDirX;
            }
            else
            {
                //attemp only Z movement
                Vector3 moveDirZ = new Vector3(0, 0, inputVector.z).normalized;
                canMove = moveDirZ != Vector3.zero && !Physics.CapsuleCast(point1, point2, playerRadius, moveDirZ, castDistance);
                if (canMove)
                {
                    inputVector = moveDirZ;
                }
                else
                {
                    //cannot move in any direction
                    
                }
    }

    private void FixedUpdate()
    {
        HandleMove();
        HandleInteract();
    }

    private void HandleInteract()
    {
        if (Physics.Raycast(transform.position, lastInputVector, out RaycastHit hit, interactDistance))
        {
            if (hit.transform.TryGetComponent(out ClearCounter cc))
            {
                if (cc != selectedCounter)
                {
                    SetSelectedCounter(cc);
                }
            }
            else
                SetSelectedCounter(null);

        }
        else
            SetSelectedCounter(null);
    }
    private void HandleMove()
    {
        if (canMove)
            rb.MovePosition(rb.position + inputVector * Time.fixedDeltaTime * moveSpeed);

        if (isWalking)
            transform.forward = Vector3.Lerp(transform.forward, inputVector, Time.fixedDeltaTime * rotateSpeed);

        if (isJump && IsGrounded())
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance, Color.red);
        Debug.DrawRay(transform.position, lastInputVector * interactDistance, Color.green);
    }

 
    private void OnInteractPerformed(object sender, EventArgs e)
    {
        TryInteract();
    }

    private void TryInteract()
    {
        if (selectedCounter != null)
            selectedCounter.Interact();
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
    }
    public bool IsWalking() { return isWalking; }
    public bool IsJump() { return isJump; }
    
    private void SetSelectedCounter (ClearCounter clearCounter)
    {
        this.selectedCounter = clearCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }
    
    
    // Debug Gizmos
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = Color.yellow;
        Vector3 point1 = transform.position;
        Vector3 point2 = transform.position + Vector3.up * playerHeight;
        Vector3 dir = inputVector.normalized * (Time.deltaTime * moveSpeed);

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
