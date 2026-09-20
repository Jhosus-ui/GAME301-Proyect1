using UnityEngine;

public class Switchs : MonoBehaviour
{
    private bool isActivate = false;
    public Transform switchVisual;
    private Vector3 originalPosition;
    public float pressDistance = 0.15f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActivate = true;

            switchVisual.localPosition=originalPosition+Vector3.down*pressDistance;
            Debug.Log("Switch activated");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActivate = false;

            switchVisual.localPosition=originalPosition;
            Debug.Log("Switch deactivated");
        }
    }

    private void Start()
    {
        originalPosition = switchVisual.localPosition;
    }
}


