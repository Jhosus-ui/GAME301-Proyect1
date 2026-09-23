using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    [Header("Switches")]
    private int activatedSwitches = 0;
    public int requiredSwitches = 3;

    public Door door;
    public void ActivateSwitch()
    {
        activatedSwitches++;
        Debug.Log("Activated Switches: " + activatedSwitches);

        if (activatedSwitches >= requiredSwitches)
        {
            door.OpenDoor();
        }
    }

    public void DeactivateSwitch()
    {
         activatedSwitches--;
        Debug.Log("Activated Switches: " + activatedSwitches);
        if (activatedSwitches < requiredSwitches)
        {
            door.CloseDoor();
        }
    }
}
