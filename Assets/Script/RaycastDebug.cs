using UnityEngine;

public class RaycastDebug : MonoBehaviour
{
    private const float MaxDistance = .2f;
    [SerializeField] private bool canMove = true;
    [SerializeField] private Player player;


    private RaycastHit hit;
    private Vector3 origin;
    private Vector3 dir;

    void Start() {
        origin = transform.position;

    }

    void Update() {
        origin = transform.position;
        dir = player.getDir();
        canMove = !Physics.Raycast(origin, dir.normalized, out hit, MaxDistance);
        DrawGizmos();
    }


    private void DrawGizmos()
    {
        Color color = canMove ? Color.green : Color.red;
        Debug.DrawRay(origin, dir.normalized * MaxDistance, color);
    }

    public bool CanMove()
    {
        return canMove;
    }
}
