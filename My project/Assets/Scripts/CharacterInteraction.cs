using UnityEngine;
using UnityEngine.UI;

public class CharacterInteraction : MonoBehaviour
{
    [SerializeField] private string characterKnot; 
    [SerializeField] private string characterKnot2;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private float dialogueEndDistance = 3f; // Distance at which dialogue ends
    [SerializeField] private GameObject interactionPrompt;
    
    private Transform player;
    private bool inDialogue = false;

     private int characterMeetings = 0;
    
    
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
            
            // If in dialogue, check if player has moved too far away
            if (inDialogue)
            {
                if (distance > dialogueEndDistance)
                {
                    Debug.Log("Player moved too far away - ending dialogue");
                    inDialogue = false;
                    dialogueManager.EndDialogue();
                }
            }
            // Not in dialogue - check for interaction
            else
            {
                if (distance <= interactionDistance)
                {
                    // Player is in range - show prompt
                    ShowInteractionPrompt(true);
                    
                    // If player presses E, start dialogue
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log(gameObject.name + " interaction triggered with knot: " + characterKnot);
                        inDialogue = true;
                        Interact();
                    }
                }
                else
                {
                    ShowInteractionPrompt(false);
                }
            }
        }
    }
    
    public void Interact()
    {
        if(characterMeetings == 1) // Use == for comparison, not = which is assignment
        {
            dialogueManager.DisplayCharacterName(gameObject);
            characterMeetings++; // Use a separate statement, not comma
            dialogueManager.StartDialogueFromKnot(characterKnot2);
        }
        else
        {
            Debug.Log("Interact called for " + gameObject.name + " with knot: " + characterKnot);
            dialogueManager.DisplayCharacterName(gameObject);
            characterMeetings++; // Use a separate statement, not comma
            dialogueManager.StartDialogueFromKnot(characterKnot);
        }
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
    
    // When dialogue ends externally, update our state
    public void DialogueEnded()
    {
        inDialogue = false;
    }
    
    
}