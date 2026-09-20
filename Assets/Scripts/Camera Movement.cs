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

    void Start()
    {
        // Stores the camera initial height so free camera movement
        yOriginal = transform.position.y;
    }

    void LateUpdate()
    {
        // While TAB is held, WASD controls the camera instead of the player
        if (Input.GetKey(KeyCode.Tab))
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            Vector3 movimiento = new Vector3(h, 0f, v) * velocity * Time.deltaTime;
            transform.position += movimiento;

            // Keeps the camera at its original height while moving freely
            // preventing vertical movement during camera mode
            Vector3 pos = transform.position;
            pos.y = yOriginal;
            transform.position = pos;
        }

        // When TAB is released, calculates the camera's normal position
        else if (player != null)
        {
            
            Vector3 destino = player.position + offset;
            destino.y = yOriginal;

            // Smoothly moves the camera back toward the player instead
            transform.position = Vector3.Lerp(
                transform.position,
                destino,
                returnSpeed * Time.deltaTime
            );
        }
    }
}