using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private RaycastDebug raycastDebug;

    private Vector3 currDir;

    void Update()
    {
        currDir = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) currDir += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) currDir += Vector3.back;
        if (Input.GetKey(KeyCode.A)) currDir += Vector3.left;
        if (Input.GetKey(KeyCode.D)) currDir += Vector3.right;

        currDir.Normalize();
        // Debug.Log(raycastDebug.CanMove(currDir));
        if (currDir != Vector3.zero && raycastDebug.CanMove()) {
            transform.position += currDir * moveSpeed * Time.deltaTime;

            Quaternion targetRotation = Quaternion.LookRotation(currDir);
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }

    public Vector3 getDir() {
        return currDir;
    }
}
