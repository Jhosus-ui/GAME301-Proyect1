using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("State of Movement")]
    public float velocity = 10f;

    [Header("Follow Player")]
    public Transform player;
    public float returnSpeed = 5f;
    public Vector3 offset = new Vector3(0f, 5f, -8f);

    private float yOriginal;
    public bool IsFreeCameraActive { get; private set; }

    private void Start()
    {
        // Store the camera's initial height to keep it fixed during free movement
        yOriginal = transform.position.y;

        IsFreeCameraActive = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            IsFreeCameraActive = !IsFreeCameraActive;
        }
    }

    private void LateUpdate()
    {
        if (IsFreeCameraActive)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            // Convert WASD input into camera movement independent of frame rate
            Vector3 movement = new Vector3(h, 0f, v) * velocity * Time.deltaTime;

            transform.position += movement;
            Vector3 position = transform.position;
            position.y = yOriginal;
            transform.position = position;
        }
        else if (player != null)
        {
            // Calculate the camera's follow position relative to the player
            Vector3 destination = player.position + offset;
            destination.y = yOriginal;

            transform.position = Vector3.Lerp(transform.position,destination, returnSpeed * Time.deltaTime);
        }
    }
}