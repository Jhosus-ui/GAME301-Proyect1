using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpHeight = 2.0f;
    public float gravity = -9.81f;
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    public Transform playerVisual;
    public float rotationSpeed = 10.0f;

    private bool isAiming = false;
    private Vector3 aimPoint;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        // Checks if the player is touching the ground
        // A small downward velocity keeps the CharacterController grounded
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // While TAB is held normal player movement is disabled because
        // Gravity remains active so the player cannot freeze in the air
        if (Input.GetKey(KeyCode.Tab)) 
        {
            velocity.y +=gravity * Time.deltaTime; 
            controller.Move(velocity * Time.deltaTime);
            return; 
        }

        // Reads the WASD input and converts it into movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * speed * Time.deltaTime);

        // The required upward velocity is calculated using the jump height and gravity
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (isAiming)
        {
            Vector3 aimDirection = aimPoint - playerVisual.position;
            aimDirection.y = 0f;

            if(aimDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(aimDirection);
                playerVisual.rotation = Quaternion.Slerp(playerVisual.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
        else if (move != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            playerVisual.rotation = Quaternion.Slerp(playerVisual.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

    }

    public void SetAimPoint(Vector3 point)
    {
        isAiming = true;
        aimPoint = point;
    }

    public void StopAiming()
    {
        isAiming = false;
    }
}