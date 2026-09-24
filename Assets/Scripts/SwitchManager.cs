using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    [Header("Switches")]
    private int activatedSwitches = 0;
    public int requiredSwitches = 3;

    [Header("Timers")]
    public float timelimit = 5f;

    private float currentTime;
    private bool timerRunning = false;

    [Header("List Switchs")]
    public Switchs[] switches;

    public Door door;
    public void ActivateSwitch()
    {
        activatedSwitches++;

        if (activatedSwitches == 1)
        {
             currentTime = timelimit;
            timerRunning = true;
            Debug.Log("Timer started");
        }

        Debug.Log("Activated Switches: " + activatedSwitches);

        if (activatedSwitches >= requiredSwitches)
        {
            timerRunning = false;
            door.OpenDoor();
            Debug.Log("Door opened");
        }
    }

    public void ResetAllSwitches()
    {
        activatedSwitches = 0;
        timerRunning = false;
        currentTime = 0f;

        foreach (Switchs currentSwitch in switches)
        {
            currentSwitch.ResetSwitch();
        }

        door.CloseDoor();
        Debug.Log("All switches have been reset.");
    }

    public void Update()
    {
       if (timerRunning)
       {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0f)
            {
                ResetAllSwitches();
            }
       }
    }
}
