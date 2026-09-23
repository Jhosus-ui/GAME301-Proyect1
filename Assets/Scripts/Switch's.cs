using UnityEngine;

public class Switchs : MonoBehaviour
{
    private bool isActivate = false;
    public Transform switchVisual;
    private Vector3 originalPosition;
    public float pressDistance = 0.15f;

    public SwitchManager SwitchManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isActivate)
            {
                isActivate = true;

                switchVisual.localPosition = originalPosition + Vector3.down * pressDistance;
                Debug.Log("Switch activated");

                SwitchManager.ActivateSwitch();
            }
        }
    }

    private void Start()
    {
        originalPosition = switchVisual.localPosition;
    }
}


