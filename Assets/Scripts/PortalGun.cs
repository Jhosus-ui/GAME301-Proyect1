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
        if(Input.GetMouseButton(1))
        {
            Aim();
            if (Input.GetMouseButtonDown(0))
            {
                ShootPortal();
            }
        }
        else
        {
            playerController.StopAiming();
        }
    }

    private void Aim()
    {
        Ray ray= playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, maxDistance, portalSurface))
        {
            playerController.SetAimPoint(hit.point);
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

            if (Physics.Raycast(playerController.transform.position, direction.normalized, distance, portalBlocker))
            {

                Debug.Log("Portal shot blocked.");
                return;
            }

            bluePortal.gameObject.SetActive(true);
            Debug.Log("Portal shot hit: " + hit.collider.name);
            Debug.Log("Hit Position: " + hit.point);

            bluePortal.position = hit.point + hit.normal * 0.02f;
        }


    }
}
