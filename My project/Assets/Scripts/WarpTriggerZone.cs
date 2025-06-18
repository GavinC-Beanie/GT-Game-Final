// Create this script: WarpTriggerZone.cs
using UnityEngine;

public class WarpTriggerZone : MonoBehaviour
{
    [Header("Warp Settings")]
    public bool showPrompt = true;              // Show "Press E to enter" prompt
    public GameObject interactionPrompt;        // UI prompt (like character interaction prompts)
    public KeyCode interactionKey = KeyCode.E;  // Key to press to warp

    [Header("Audio/Effects (Optional)")]
    public AudioClip warpSound;
    public GameObject warpEffect;               // Particle effect or animation

    private bool playerInRange = false;
    private bool hasWarped = false;

    void Start()
    {
        // Make sure prompt starts hidden
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false);
        }

        // Make sure this has a trigger collider
        Collider2D col = GetComponent<Collider2D>();
        if (col == null)
        {
            Debug.LogError("WarpTriggerZone needs a Collider2D component!");
        }
        else
        {
            col.isTrigger = true;
        }
    }

    void Update()
    {
        // Check for interaction input when player is in range
        if (playerInRange && !hasWarped)
        {
            if (Input.GetKeyDown(interactionKey))
            {
                TriggerWarp();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if player entered the trigger zone
        if (other.CompareTag("Player") && !hasWarped)
        {
            Debug.Log("Player entered warp trigger zone");
            playerInRange = true;

            // Show interaction prompt
            if (showPrompt && interactionPrompt != null)
            {
                interactionPrompt.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Check if player left the trigger zone
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left warp trigger zone");
            playerInRange = false;

            // Hide interaction prompt
            if (interactionPrompt != null)
            {
                interactionPrompt.SetActive(false);
            }
        }
    }

    void TriggerWarp()
    {
        Debug.Log("Triggering warp to special area!");

        // Hide prompt
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false);
        }

        // Play sound effect (optional)
        if (warpSound != null)
        {
            AudioSource.PlayClipAtPoint(warpSound, transform.position);
        }

        // Show warp effect (optional)
        if (warpEffect != null)
        {
            Instantiate(warpEffect, transform.position, Quaternion.identity);
        }

        // Trigger the actual warp
        if (WarpAreaManager.Instance != null)
        {
            WarpAreaManager.Instance.WarpToArea();
            hasWarped = true; // Prevent using this trigger again
        }
        else
        {
            Debug.LogError("WarpAreaManager not found!");
        }
    }

    public void ResetTrigger()
    {
        // Call this if you want to allow re-entering the warp
        hasWarped = false;
    }
}
