using UnityEngine;
using UnityEngine.SceneManagement; 

public class FinishLine : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f; 
    [SerializeField] ParticleSystem finishEffect;
    [SerializeField] AudioClip finishSFX;

    // Make sure the Finish Line object has a 2D Collider with "Is Trigger" checked
    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {
           
            if (finishEffect != null)
            {
                finishEffect.Play();
            }

            
            AudioSource audioSource = GetComponent<AudioSource>();
            if (finishSFX != null && audioSource != null)
            {
                audioSource.PlayOneShot(finishSFX);
            }

           
            FindFirstObjectByType<PlayerController>().DisableControls();

           
            Invoke(nameof(LoadNextLevel), loadDelay);
        }
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
           
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
}
