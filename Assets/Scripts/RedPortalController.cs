using UnityEngine;

public class RedPortalController : MonoBehaviour
{

    [SerializeField] private Transform redPortal;
    [SerializeField] private Transform startingPoint;

    private void Start()
    {
        MovePortalTo(startingPoint);
    }

    public void MovePortalTo(Transform targetPoint)
    {

        if(targetPoint== null)
        {
            Debug.LogWarning("No Red Portal target point assigned.");
            return;

        }

        redPortal.position = targetPoint.position;
    }
}
