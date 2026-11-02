using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UIElements;
using System.Runtime.CompilerServices;

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
    public TMP_Text ButtonPrompt;
    [Header("Audio Stuff")]
    public AudioSource FootstepsSource;
    public AudioSource MiscSource;
    public AudioClip CurrentFootstepSFX;
    public List<AudioClip> WalkSFXs;
    public AudioClip CrashSFX;
    private bool PlayCrashSound;

    private float moveX;
    private float moveZ;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    private void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && PlayCrashSound)
        {
            Debug.Log("Crash!");
            MiscSource.PlayOneShot(CrashSFX, 0.25f);
            PlayCrashSound = false;
        }

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

        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * speed * Time.deltaTime);

        // Calculate the initial upward velocity required
        // to reach the configured jump height.
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            PlayCrashSound = true;
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }

        if ((moveX != 0 || moveZ != 0) && isGrounded)
        {
            Debug.Log("SoundOn!");
            if (!FootstepsSource.isPlaying)
            {

                FootstepsSource.PlayOneShot(CurrentFootstepSFX, 1.25f);
            }
        }
        else
        {
            FootstepsSource.Stop();
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
    public void ShowDirection(string info)
    {
        ButtonPrompt.text = info;
    }

    public void HideDirection()
    {
        ButtonPrompt.text = null;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Stone"))
        {
            CurrentFootstepSFX = WalkSFXs[0];
        }
        if (hit.gameObject.CompareTag("Metal"))
        {
            CurrentFootstepSFX = WalkSFXs[1];
        }
        if (hit.gameObject.CompareTag("Grass"))
        {
            CurrentFootstepSFX = WalkSFXs[2];
        }
        if (hit.gameObject.CompareTag("Tile"))
        {
            CurrentFootstepSFX = WalkSFXs[3];
        }
    }
}