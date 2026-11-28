using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform leftDoorVisual;
    [SerializeField] private Transform rightDoorVisual;

    public float openDistance = 3f;

    private Vector3 leftClosedPosition;
    private Vector3 rightClosedPosition;
    public AudioSource DoorAS;
    public AudioClip DoorSFX;
    public float DoorSpeed;
    public float DoorTravelAmount;
    public DoorState DS;
    public Vector3 openingDirection;

    private void Start()
    {
        // Saves the initial world positions so both pieces
        // can always return to their original closed position
        leftClosedPosition = leftDoorVisual.position;
        rightClosedPosition = rightDoorVisual.position;
    }
    private void Update()
    {
        //switch statement with a state machine so we can easily expand on the door's behavior depending on what state it's in.
        switch (DS) 
        {
            case DoorState.Opening:
                //DoorSpeed controls how fast the doors reach the target destination (openDistance), Door Travel Amount controls how much progress the door has made towards the destination
                DoorTravelAmount += DoorSpeed * Time.deltaTime;
                if (DoorTravelAmount < openDistance)
                {
                    leftDoorVisual.position =
                        leftClosedPosition - openingDirection * DoorTravelAmount;

                    rightDoorVisual.position =
                        rightClosedPosition + openingDirection * DoorTravelAmount;
                }
                else
                {
                    DS = DoorState.Open;
                    
                }
                break;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            rightDoorVisual.position = rightClosedPosition;
            leftDoorVisual.position = leftClosedPosition;
            DS = DoorState.Closed;
            DoorTravelAmount = 0;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            OpenDoor();
        }
    }

    // Called by SwitchManager when all required switches are activated
    public void OpenDoor()
    {
        DS = DoorState.Opening;
        DoorAS.PlayOneShot(DoorSFX, 0.25f);
        //Checks what direction the door needs to open
        openingDirection =
            (rightDoorVisual.position - leftDoorVisual.position).normalized;

    }

    // Restore the door to its closed state when the puzzle resets
    public void CloseDoor()
    {
        DoorAS.PlayOneShot(DoorSFX, 0.25f);
        leftDoorVisual.position = leftClosedPosition;
        rightDoorVisual.position = rightClosedPosition;
        DS = DoorState.Closed;
        DoorTravelAmount = 0;
    }
    public enum DoorState
    {
        Closed = 0,
        Opening = 1,
        Open = 2,
        Closing = 3
    }
}