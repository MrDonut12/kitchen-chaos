using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private float speed = 5.4f;
    private void Update()
    {
        Vector3 InputVector = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            InputVector.z = +1;
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
            InputVector.x = +1;
        }

        InputVector = InputVector.normalized;
        transform.position += InputVector * Time.deltaTime * speed;
        Debug.Log(InputVector);
    }
}
