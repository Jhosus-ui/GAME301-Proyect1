using UnityEngine;

public class Switchs : MonoBehaviour
{
    private bool isActivate = false;
    private bool playerInRange = false;

    public Transform switchVisual;
    private Vector3 originalPosition;
    public float pressDistance = 0.15f;

    [SerializeField] private AudioSource switchAudio;

    public SwitchManager SwitchManager;
    private void Start()
    {
        originalPosition = switchVisual.localPosition;
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

        // Move the switch visual to provide feedback when activated
        switchVisual.localPosition = originalPosition + Vector3.back * pressDistance;

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
        isActivate = false;
        switchVisual.localPosition = originalPosition;
    }
}