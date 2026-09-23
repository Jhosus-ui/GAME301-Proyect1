using UnityEngine;

public class Door : MonoBehaviour
{

    public Transform doorVisual;
    public float openDistance = 3f;
    private Vector3 closedPosition;


    private void Start()
    {
        closedPosition = doorVisual.localPosition;
    }

    public void OpenDoor()
    {
        doorVisual.localPosition = closedPosition + Vector3.up * openDistance;
        Debug.Log("Door opened");
    }

    public void CloseDoor()
    {
        doorVisual.localPosition = closedPosition;
        Debug.Log("Door closed");
    }
}
