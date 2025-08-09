using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private string IS_WALKING = "IsWalking";
    [SerializeField] private PlayerController Player;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        animator.SetBool(IS_WALKING, Player.IsWalking());   
    }
}
