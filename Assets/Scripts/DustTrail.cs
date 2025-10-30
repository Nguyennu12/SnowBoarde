using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustTrail : MonoBehaviour
{
    [SerializeField] ParticleSystem dustEffect; 

    void OnCollisionEnter2D(Collision2D other)
    {
        // When the board (physics collider) hits the ground
        if (other.gameObject.tag == "Ground")
        {
            if (dustEffect != null)
            {
                dustEffect.Play();
            }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        // When the board leaves the ground
        if (other.gameObject.tag == "Ground")
        {
            if (dustEffect != null)
            {
                dustEffect.Stop();
            }
        }
    }
}
