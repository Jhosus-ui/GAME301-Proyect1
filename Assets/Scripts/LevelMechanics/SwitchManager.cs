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

    [Header("Timer Audio")]
    [SerializeField] private AudioSource timerAudio;
    [SerializeField] private float tickInterval = 1f;

    private float tickCountdown = 0f;

    public Door door;
    public void ActivateSwitch()
    {
        activatedSwitches++;

        // Start the countdown only when the first switch is activated
        if (activatedSwitches == 1)
        {
             currentTime = timelimit;
            timerRunning = true;
            tickCountdown = 0f;
            Debug.Log("Timer started");
        }

        Debug.Log("Activated Switches: " + activatedSwitches);

        // Stop the countdown and open the door once all required switches are active
        if (activatedSwitches >= requiredSwitches)
        {
            timerRunning = false;
            door.OpenDoor();
            Debug.Log("Door opened");
        }
    }

    // Restore the puzzle to its initial state when the time limit expires
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

            // Use a separate countdown to control the ticking sound
            // without affecting the puzzle's time limit
            tickCountdown -= Time.deltaTime;

            if (tickCountdown <= 0f)
            {
                if (timerAudio != null)
                {
                    timerAudio.Play();
                }
                else
                {
                    Debug.LogWarning("Timer AudioSource is missing!");
                }

                // Gradually shorten the interval between ticks as time runs out
                float timeRatio = Mathf.Clamp01(currentTime / timelimit);

                tickCountdown = Mathf.Lerp(0.2f,tickInterval,timeRatio);
            }

            if (currentTime <= 0f)
            {
                ResetAllSwitches();
            }
       }
    }
}
