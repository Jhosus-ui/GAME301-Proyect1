using UnityEngine;

public class PortalVisual : MonoBehaviour
{

    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private Vector3 rotationaxis= Vector3.forward;

    private void Update()
    {
        // Rotate the portal visual around its local axis
        // Time.deltaTime keeps the rotation speed independent of frame rate
        transform.Rotate(rotationaxis, rotationSpeed * Time.deltaTime,Space.Self);
    }
}
