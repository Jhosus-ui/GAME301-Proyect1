using UnityEngine;
using UnityEngine.Animations;

public class PortalGun : MonoBehaviour
{

    public Camera playerCamera;
    public float maxDistance = 100f;
    public Transform bluePortal;
    public LayerMask portalSurface;
    public PlayerController playerController;

    [SerializeField] private LayerMask portalBlocker;

    public void Start()
    {
        bluePortal.gameObject.SetActive(false);
    }
    private void Update()
    {
        // A single left click attempts to place the Blue Portal.
        // The player no longer needs to enter a separate aiming mode.
        if (Input.GetMouseButtonDown(0))
        {
            ShootPortal();
        }
    }

    private void ShootPortal()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance, portalSurface))
        {
            Vector3 direction = hit.point - playerController.transform.position;
            float distance = direction.magnitude;

            if (Physics.Raycast(playerController.transform.position,direction.normalized, distance,portalBlocker))
            {
                Debug.Log("Portal shot blocked.");
                return;
            }

            // The shot is valid, so the character faces the direction
            // where the Blue Portal is about to be placed.
            playerController.FaceShootDirection(hit.point);

            bluePortal.gameObject.SetActive(true);

            bluePortal.gameObject.SetActive(true);
            Debug.Log("Portal shot hit: " + hit.collider.name);
            Debug.Log("Hit Position: " + hit.point);

            bluePortal.position = hit.point + hit.normal * 0.02f;
        }
    }
}
