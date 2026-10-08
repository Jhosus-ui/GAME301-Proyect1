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

        if (activatedSwitches == 1)
        {
             currentTime = timelimit;
            timerRunning = true;
            tickCountdown = 0f;
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
            tickCountdown -= Time.deltaTime;

            if (tickCountdown <= 0f)
            {
                Debug.Log("Timer tick triggered");
                if (timerAudio != null)
                {
                    timerAudio.Play();
                }
                else
                {
                    Debug.LogWarning("Timer AudioSource is missing!");
                }

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
