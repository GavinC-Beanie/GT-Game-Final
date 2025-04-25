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

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
            Debug.LogError("Player not found — make sure they have the 'Player' tag.");

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
                Debug.Log($"{characterName} — Player moved too far. Ending dialogue.");
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

        Debug.Log($"{characterName} — Dialogue started at knot: {characterKnot}");

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
