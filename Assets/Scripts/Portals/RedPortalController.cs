using UnityEngine;

public class RedPortalController : MonoBehaviour
{

    [SerializeField] private Transform redPortal;
    [SerializeField] private Transform startingPoint;

    private void Start()
    {
        // Place the Red Portal at its initial position when the level starts
        MovePortalTo(startingPoint);
    }

    public void MovePortalTo(Transform targetPoint)
    {
        // Prevent moving the portal if no destination point was assigned
        if (targetPoint== null)
        {
            Debug.LogWarning("No Red Portal target point assigned.");
            return;

        }

        redPortal.position = targetPoint.position;
    }
}
