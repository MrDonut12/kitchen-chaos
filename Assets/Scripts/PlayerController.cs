using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool isWalking;
    [SerializeField] private float move_speed = 5.4f;
    [SerializeField] private float rotate_speed = 10.4f;
    private void Update()
    {
        Vector3 InputVector = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            InputVector.z = 1;
        }

        if (Input.GetKey(KeyCode.A))
        {

            InputVector.x = -1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            InputVector.z = -1;
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            InputVector.x = 1;
        }

        InputVector = InputVector.normalized;
        transform.position += InputVector * Time.deltaTime * move_speed;
        // transform.forward = InputVector;
        isWalking = (InputVector != Vector3.zero);
        
        transform.forward = Vector3.Lerp(transform.forward, (InputVector), Time.deltaTime * rotate_speed);
        Debug.Log(InputVector);

    }

    public bool IsWalking() { return isWalking; }
}
