using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    //[SerializeField] float reloadDelay = 0.5f;
    [SerializeField] ParticleSystem crashEffect;
    [SerializeField] AudioClip crashSFX;

    bool hasCrashed = false;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Detects a fall (when the HEAD trigger touches the ground)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground" && !hasCrashed)
        {
            TriggerCrashSequence();
        }
    }

    // NEW: Detects a physical collision (when the BODY hits an obstacle)
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Obstacle" && !hasCrashed)
        {
            TriggerCrashSequence();
        }
    }

    // Separate function to avoid repeating code
    void TriggerCrashSequence()
    {
        if (hasCrashed) return; // Ensure it only runs once

        hasCrashed = true;

        // Disable player controls
        FindFirstObjectByType<PlayerController>().DisableControls();

        // Play crash particle effect (if assigned)
        if (crashEffect != null)
        {
            crashEffect.Play();
        }

        // Play crash sound effect (if assigned)
        if (crashSFX != null && audioSource != null)
        {
            audioSource.PlayOneShot(crashSFX);
        }

        FindFirstObjectByType<GameSession>().ProcessPlayerDeath();

    }

    void ReloadScene()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
