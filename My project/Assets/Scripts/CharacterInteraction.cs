using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private string characterKnot;              // Knot to start in Ink
    [SerializeField] public string characterName;              // Name used in StoryStateManager
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private float dialogueEndDistance = 5f;

    [Header("References")]
    [SerializeField] public DialogueManager dialogueManager;
    [SerializeField] public GameObject interactionPrompt;

    private Transform player;
    private bool inDialogue = false;


    // Public setters for the references
    public void SetDialogueManager(DialogueManager manager)
    {
        dialogueManager = manager;
    }

    public void SetInteractionPrompt(GameObject prompt)
    {
        interactionPrompt = prompt;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        // noah
        // find dialouge manager with tag like with player
        // drag in anything else from your unity assets its probably there
        // if anything else is IN SCENE BEFORE THIS IS CREATED use finds like player

        // collider?
        // these mfs dont have box colliders rn
        // most collisions shouldnt work... but it might
        // so if its not do the following
        // give each a box collider 2d (not box collider)
        // enable "is trigger" (on character box collider 2d) if you want player to be able to walk through said characters hit box


        if (player == null)
            Debug.LogError("Player not found � make sure they have the 'Player' tag.");

        if (interactionPrompt)
            interactionPrompt.SetActive(false);
    }

    void Update()
{
    if (!player) 
    {
        Debug.LogError($"{characterName} - Player reference is null!");
        return;
    }
    
    float distance = Vector2.Distance(transform.position, player.position);
    
    
    if (inDialogue)
    {
        
        if (distance > dialogueEndDistance)
        {
            Debug.Log($"{characterName} - Player moved too far ({distance:F2} > {dialogueEndDistance}). Ending dialogue.");
            inDialogue = false;
            Debug.Log($"{characterName} - Set inDialogue = false");
            
            if (dialogueManager != null)
            {
                Debug.Log($"{characterName} - Calling dialogueManager.EndDialogue()");
                dialogueManager.EndDialogue();
            }
            else
            {
                Debug.LogError($"{characterName} - dialogueManager is null!");
            }
        }
        
    }
    else
    {
        
        if (distance <= interactionDistance)
        {
        
            ShowPrompt(true);
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log($"{characterName} - E key pressed! Starting interaction.");
                StartInteraction();
            }
          
        }
        else
        {
           
            ShowPrompt(false);
        }
    }
}

    private void StartInteraction()
{
    Debug.Log($"{characterName} - StartInteraction() called");
    
    if (inDialogue)
    {
        Debug.LogWarning($"{characterName} - Already in dialogue, aborting StartInteraction");
        return;
    }
    
    if (dialogueManager == null)
    {
        Debug.LogError($"{characterName} - dialogueManager is null, aborting StartInteraction");
        return;
    }
    
    Debug.Log($"{characterName} - Setting inDialogue = true");
    inDialogue = true;
    
    Debug.Log($"{characterName} - Calling DisplayCharacterName with GameObject: {gameObject.name}");
    dialogueManager.DisplayCharacterName(gameObject);
    
    Debug.Log($"{characterName} - Setting current character in DialogueManager FIRST");
    dialogueManager.SetCurrentCharacter(characterName);  // ← MOVED THIS UP
    
    Debug.Log($"{characterName} - Starting dialogue from knot: {characterKnot}");
    dialogueManager.StartDialogueFromKnot(characterKnot);  // ← This calls RefreshView()
    
}


    private void ShowPrompt(bool show)
    {
        if (interactionPrompt != null)
            interactionPrompt.SetActive(show);
    }
}