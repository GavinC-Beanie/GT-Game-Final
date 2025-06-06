using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private string characterKnot;              // Knot to start in Ink
    [SerializeField] private string characterName;              // Name used in StoryStateManager
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private float dialogueEndDistance = 5f;

    [Header("References")]
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private GameObject interactionPrompt;

    private Transform player;
    private bool inDialogue = false;
    private bool hasBeenMet = false;

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
        if (!player) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (inDialogue)
        {
            if (distance > dialogueEndDistance)
            {
                Debug.Log($"{characterName} � Player moved too far. Ending dialogue.");
                EndInteraction();
            }
        }
        else
        {
            if (distance <= interactionDistance)
            {
                ShowPrompt(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
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
        if (inDialogue || dialogueManager == null) return;

        inDialogue = true;
        dialogueManager.DisplayCharacterName(gameObject);
        dialogueManager.StartDialogueFromKnot(characterKnot);

        Debug.Log($"{characterName} � Dialogue started at knot: {characterKnot}");

        if (!hasBeenMet)
        {
            StoryStateManager.Instance?.OnCharacterMet(characterName);
            hasBeenMet = true;
        }
    }

    private void EndInteraction()
    {
        inDialogue = false;
        dialogueManager.EndDialogue();
        ShowPrompt(false);
    }

    public void DialogueEnded()
    {
        inDialogue = false;
    }

    private void ShowPrompt(bool show)
    {
        if (interactionPrompt != null)
            interactionPrompt.SetActive(show);
    }
}
