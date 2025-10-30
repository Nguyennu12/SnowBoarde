using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = 1f;
    [SerializeField] float boostSpeed = 30f;
    [SerializeField] float baseSpeed = 20f;

    [Header("Trick System")]
    [SerializeField] float rotationThreshold = 340f;
    [SerializeField] int pointsPerTrick = 100;

    Rigidbody2D rb2d;
    SurfaceEffector2D surfaceEffector2D;
    ScoreManager scoreManager;
    bool canMove = true;
    float originalBoostSpeed;
    float originalBaseSpeed;
    bool speedBoostActive = false;
    bool isGrounded = true;        
    float totalRotation = 0f;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        scoreManager = FindFirstObjectByType<ScoreManager>();
        surfaceEffector2D = FindFirstObjectByType<SurfaceEffector2D>();

        if (!speedBoostActive)
        {
            originalBoostSpeed = boostSpeed;
            originalBaseSpeed = baseSpeed;
        }
    }



    void FixedUpdate()
    {
        if (canMove)
        {
            RotatePlayer();
            RespondToBoost();
            if (!isGrounded)
            {
                
                totalRotation += Mathf.Abs(rb2d.angularVelocity) * Time.fixedDeltaTime;
            }
        }
    }

    public void DisableControls()
    {
        canMove = false;
    }

    void RespondToBoost()
    {
        
        if (Keyboard.current.upArrowKey.isPressed)
        {
            surfaceEffector2D.speed = boostSpeed;
        }
        else
        {
            surfaceEffector2D.speed = baseSpeed;
        }
    }

    void RotatePlayer()
    {
        
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            rb2d.AddTorque(torqueAmount);
        }
        
        else if (Keyboard.current.rightArrowKey.isPressed)
        {
            rb2d.AddTorque(-torqueAmount);
        }
    }
    public void ApplySpeedBoost(float multiplier, float duration)
    {
        if (speedBoostActive) return; // Prevent stacking

        originalBoostSpeed = boostSpeed;
        originalBaseSpeed = baseSpeed;

        boostSpeed *= multiplier;
        baseSpeed *= multiplier;
        speedBoostActive = true;

        StartCoroutine(RemoveSpeedBoost(duration));
    }

    // Restore original speed after duration
    System.Collections.IEnumerator RemoveSpeedBoost(float duration)
    {
        yield return new WaitForSeconds(duration);

        boostSpeed = originalBoostSpeed;
        baseSpeed = originalBaseSpeed;
        speedBoostActive = false;
    }
    // Called when the board leaves the ground
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    // Called when the board touches the ground
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;

            CheckForTrick(); // Check if a trick can be scored

            totalRotation = 0f; // Reset rotation counter
        }
    }

    // Check and award points for tricks
    void CheckForTrick()
    {
        if (totalRotation >= rotationThreshold)
        {
            int rotations = Mathf.FloorToInt(totalRotation / rotationThreshold);
            int totalPoints = pointsPerTrick * rotations;

            Debug.Log("TRICK SUCCESS! Points: " + totalPoints);

            if (scoreManager != null)
            {
                scoreManager.AddScore(totalPoints);
            }
        }
    }

}