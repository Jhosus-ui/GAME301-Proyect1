using UnityEngine;

public class Switchs : MonoBehaviour
{
    private bool isActivate = false;
    private bool playerInRange = false;

    private PlayerController PC;

    [SerializeField] private AudioSource switchAudio;

    public SwitchManager SwitchManager;

    public Animator SwitchAni;

    private void Start()
    {
        PC = FindFirstObjectByType<PlayerController>();
        SwitchAni.SetInteger("State", 0);

    }
    private void Update()
    {
        // Allow interaction only when the player is nearby
        // and the switch has not already been activated
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isActivate)
        {
            Activate();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (!isActivate)
            {
                PC.ShowDirection("E");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
    private void Activate()
    {
        isActivate = true;
        SwitchAni.SetInteger("State", 1);
        SwitchManager.ActivateSwitch();

        if (switchAudio != null)
        {
            switchAudio.Play();
        }
    }
    // Called by SwitchManager to restore the switch
    // when the player runs out of time
    public void ResetSwitch()
    {

        Debug.Log("ResetSwitch was called!");
        isActivate = false;
        SwitchAni.SetInteger("State", 0);
    }

}
