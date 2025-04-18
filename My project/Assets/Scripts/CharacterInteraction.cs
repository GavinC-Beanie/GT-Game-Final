using UnityEngine;
using UnityEngine.UI;

public class CharacterInteraction : MonoBehaviour
{
    [SerializeField] private string characterKnot; // e.g., "Bill" or "Gram"
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private GameObject interactionPrompt; // Optional UI prompt
    
    private Transform player;
    
    void Start()
    {
        Debug.Log(gameObject.name + " is set up with knot: " + characterKnot);
        
        // Find the player by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("No GameObject with Player tag found!");
        }
        
        // Hide prompt at start if it exists
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false);
        }
    }
    
    void Update()
    {
        // Check proximity to player
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            
            if (distance <= interactionDistance)
            {
                // Player is in range - show prompt
                ShowInteractionPrompt(true);
                
                // If player presses E, start dialogue
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log(gameObject.name + " interaction triggered with knot: " + characterKnot);
                    Interact();
                }
            }
            else
            {
                ShowInteractionPrompt(false);
            }
        }
    }
    
    public void Interact()
    {
        Debug.Log("Interact called for " + gameObject.name + " with knot: " + characterKnot);
        dialogueManager.StartDialogueFromKnot(characterKnot);
    }
    
    private void ShowInteractionPrompt(bool show)
    {
        // If you have a UI prompt, enable/disable it
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(show);
        }
        
        // For debugging
        if (show)
        {
            Debug.Log("Player in range of " + gameObject.name + " - press E to interact");
        }
    }
    
    // Optional: visualize the interaction radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }
}