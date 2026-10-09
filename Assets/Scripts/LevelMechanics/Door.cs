using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform leftDoorVisual;
    [SerializeField] private Transform rightDoorVisual;
    [SerializeField] private float openDistance = 1.5f;

    private Vector3 leftClosedPosition;
    private Vector3 rightClosedPosition;

    private void Start()
    {
        // Saves the initial world positions so both pieces
        // can always return to their original closed position
        leftClosedPosition = leftDoorVisual.position;
        rightClosedPosition = rightDoorVisual.position;
    }

    // Called by SwitchManager when all required switches are activated
    public void OpenDoor()
    {
        Vector3 openingDirection =
            (rightDoorVisual.position - leftDoorVisual.position).normalized;

        // Each half moves away from the center in opposite directions
        leftDoorVisual.position =
            leftClosedPosition - openingDirection * openDistance;

        rightDoorVisual.position =
            rightClosedPosition + openingDirection * openDistance;
    }

    // Restore the door to its closed state when the puzzle resets
    public void CloseDoor()
    {
        leftDoorVisual.position = leftClosedPosition;
        rightDoorVisual.position = rightClosedPosition;
    }
}