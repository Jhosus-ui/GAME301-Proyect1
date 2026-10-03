using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform exitPoint;
    public Portal connectedPortal;

    // Bloqueo local por portal (no static)
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

        // Bloquea este portal y el destino para evitar bucle inmediato
        canTeleport = false;
        connectedPortal.canTeleport = false;

        // Desactiva el CharacterController antes de mover
        controller.enabled = false;
        player.transform.position = exitPoint.position;
        controller.enabled = true;

        // Libera el bloqueo en el siguiente frame (o tras un pequeño delay)
        Invoke(nameof(UnblockTeleport), 0.5f);
    }

    private void UnblockTeleport()
    {
        canTeleport = true;
        if (connectedPortal != null)
            connectedPortal.canTeleport = true;
    }
}