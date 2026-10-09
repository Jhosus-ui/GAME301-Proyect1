using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5.0f;
    public float jumpHeight = 2.0f;
    public float gravity = -9.81f;

    [Header("Player Visual")]
    public Transform playerVisual;
    public float rotationSpeed = 10.0f;

    [Header("Free Camera")]
    [SerializeField] private CameraMovement cameraMovement;

    [Header("Portal Shot Rotation")]
    [SerializeField] private float shootRotationDuration = 0.25f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private Vector3 shootDirection;
    private float shootRotationTimer = 0f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    private void Update()
    {
        isGrounded = controller.isGrounded;

        // Maintain ground contact when the player is not jumping.
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Disable horizontal movement in free camera mode,
        // while keeping gravity active.
        if (cameraMovement != null && cameraMovement.IsFreeCameraActive)
        {
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
            return;
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * speed * Time.deltaTime);

        // Calculate the initial upward velocity required
        // to reach the configured jump height.
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Temporarily prioritize the portal shot direction
        // over the normal movement-facing rotation
        if (shootRotationTimer > 0f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(shootDirection);

            playerVisual.rotation = Quaternion.Slerp(playerVisual.rotation,targetRotation,rotationSpeed * Time.deltaTime);
            shootRotationTimer -= Time.deltaTime;
        }
        else if (move != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);

            playerVisual.rotation = Quaternion.Slerp(playerVisual.rotation,targetRotation,rotationSpeed * Time.deltaTime);
        }
    }

    // Called by PortalGun to briefly face the portal shot direction
    public void FaceShootDirection(Vector3 targetPoint)
    {
        shootDirection = targetPoint - playerVisual.position;
        shootDirection.y = 0f;

        if (shootDirection != Vector3.zero)
        {
            shootDirection.Normalize();
            shootRotationTimer = shootRotationDuration;
        }
    }
}