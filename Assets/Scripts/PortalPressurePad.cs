using UnityEngine;

public class PortalPressurePad: MonoBehaviour
{
    [SerializeField] private RedPortalController portalController;
    [SerializeField] private Transform targetPoint;

    [SerializeField] private bool toggleMode = false;
    [SerializeField] private Transform returnPoint;

    private bool isToggled = false;

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player"))
        {
            return;
        }

        if (!toggleMode)
        {
            portalController.MovePortalTo(targetPoint);
            return;
        }

        if (!isToggled)
        {
            portalController.MovePortalTo(targetPoint);
            isToggled = true;
        }
        else
        {
            portalController.MovePortalTo(returnPoint);
            isToggled = false;
        }
    }
}