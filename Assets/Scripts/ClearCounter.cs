using UnityEngine;

public class ClearCounter : MonoBehaviour
{

    private Transform SelectedBox;
    private Transform KitchenBox;
    [SerializeField] private bool isInteract;
    private void Awake()
    {
 
    }


    public void Interact()
    {
        isInteract = true;
    }


    public void Pickup()
    {

    }
    
    public bool IsInteract()
    {
        return isInteract;
    }
}
