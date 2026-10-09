using UnityEngine;

public class PortalPressurePad: MonoBehaviour
{
    [SerializeField] private RedPortalController portalController;
    [SerializeField] private Transform targetPoint;

    [SerializeField] private bool toggleMode = false;
    [SerializeField] private Transform returnPoint;

    [SerializeField] private ButtonVisual buttonVisual;
    [SerializeField] private AudioSource padAudio;

    private bool isToggled = false;

    private void OnTriggerEnter(Collider other)
    {
        // Only the player can activate the pressure pad
        if (!other.CompareTag("Player"))
        {
            return;
        }

        if (buttonVisual != null)
        {
            buttonVisual.PlayPressEffect();
        }

        if (padAudio != null)
        {
            padAudio.Play();
        }

        // In normal mode, move the Red Portal to the target point
        // In toggle mode, alternate between the target and return points
        if (!toggleMode)
        {
            portalController.MovePortalTo(targetPoint);
            return;
        }

        // Track the current toggle state to determine the next destination
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