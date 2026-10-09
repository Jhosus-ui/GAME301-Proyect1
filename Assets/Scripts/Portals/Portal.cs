using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform exitPoint;
    public Portal connectedPortal;

    // Prevents repeated teleportation while the portals are temporarily locked
    private bool canTeleport = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!canTeleport) return;

        TeleportPlayer(other);
    }

    private void TeleportPlayer(Collider player)
    {
        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller == null) return;

        // Lock both portals to prevent immediate teleportation loops
        canTeleport = false;
        connectedPortal.canTeleport = false;

        // Temporarily disable CharacterController to reposition the player safely
        controller.enabled = false;
        player.transform.position = exitPoint.position;
        controller.enabled = true;

        // Restore teleportation after a short cooldown
        Invoke(nameof(UnblockTeleport), 0.5f);
    }

    private void UnblockTeleport()
    {
        canTeleport = true;
        if (connectedPortal != null)
            connectedPortal.canTeleport = true;
    }
}