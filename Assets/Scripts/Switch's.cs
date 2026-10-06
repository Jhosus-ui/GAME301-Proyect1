using UnityEngine;

public class Switchs : MonoBehaviour
{
    private bool isActivate = false;
    private bool playerInRange = false;

    public Transform switchVisual;
    private Vector3 originalPosition;
    public float pressDistance = 0.15f;

    public SwitchManager SwitchManager;
    private void Start()
    {
        originalPosition = switchVisual.localPosition;
    }
    private void Update()
    {
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

            Debug.Log("Press E to activate the switch");
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

        switchVisual.localPosition = originalPosition + Vector3.back * pressDistance;
        Debug.Log("Switch activated");

        SwitchManager.ActivateSwitch();
    }
    public void ResetSwitch()
    {
        isActivate = false;
        switchVisual.localPosition = originalPosition;
        Debug.Log("Switch deactivated");
    }
}