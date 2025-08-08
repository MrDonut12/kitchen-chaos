using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool isWalking;
    [SerializeField] private float move_speed = 5.4f;
    [SerializeField] private float rotate_speed = 24f;
    [SerializeField] private float jumpForce = 3f;
    private bool isJump = false;
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        // init
        isJump = false;


        Vector3 InputVector = new Vector3(0, 0, 0);
        
        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            isJump = true;
        }
        if (Input.GetKey(KeyCode.W)) InputVector.z = 1;
        if (Input.GetKey(KeyCode.A)) InputVector.x = -1;
        if (Input.GetKey(KeyCode.S)) InputVector.z = -1;
        if (Input.GetKey(KeyCode.D)) InputVector.x = 1;



        InputVector = InputVector.normalized; 
        rb.MovePosition(rb.position + InputVector * Time.deltaTime * move_speed);

        isWalking = (InputVector != Vector3.zero);

        if (isWalking) transform.forward = Vector3.Lerp(transform.forward, (InputVector), Time.deltaTime * rotate_speed);
        
        if (isJump && IsGrounded())
        {
            // transform.forward = InputVector;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }


        // Debug line
        Debug.DrawRay(transform.position, Vector3.down * .3f, Color.red) ;
        if (IsGrounded())
            Debug.Log("IS GROUNDED");
    }

    public bool IsWalking() { return isWalking; }
    private bool IsGrounded() { return Physics.Raycast(transform.position, Vector3.down, .3f); }
}
