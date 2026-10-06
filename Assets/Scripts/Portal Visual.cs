using UnityEngine;

public class PortalVisual : MonoBehaviour
{

    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private Vector3 rotationaxis= Vector3.forward;

    private void Update()
    {
        //Rotates around the portal visual's own local axis.
        transform.Rotate(rotationaxis, rotationSpeed * Time.deltaTime,Space.Self);
    }
}
