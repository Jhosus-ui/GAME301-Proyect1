using UnityEngine;
using UnityEngine.SceneManagement;

public class Deathones : MonoBehaviour
{
    // Detect when an object enters the death zone trigger
    private void OnTriggerEnter(Collider other)
    {
        // Only restart the level when the player enters the death zone
        if (!other.CompareTag("Player"))
        {
            return;
        }

        // Reload the current scene to reset the player and puzzle mechanics
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
