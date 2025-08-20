using UnityEngine;

public class ClearCounter : MonoBehaviour
{

    private Transform SelectedBox;
    private Transform KitchenBox;
    [SerializeField] private bool isInteract;
    private void Awake()
    {
        SelectedBox = transform.Find("SelectedBox");
        KitchenBox = SelectedBox.Find("KitchenCounter");
    }


    public void Interact()
    {
        isInteract = true;
        KitchenBox.GetComponent<MeshRenderer>().enabled = isInteract;
    }


    public void Pickup()
    {

    }
    
    public bool IsInteract()
    {
        return isInteract;
    }
}
