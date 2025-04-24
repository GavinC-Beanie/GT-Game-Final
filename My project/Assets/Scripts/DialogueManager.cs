using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using System.Collections.Generic;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    // Ink story
    public TextAsset inkJSON;
    private Story story;

    // UI References
    public GameObject dialoguePanel;
    public TMP_Text nameText;

    // New bubble UI references
    public GameObject npcBubblePrefab;  // Gray bubble
    public GameObject pcBubblePrefab;   // Blue bubble
    public Transform bubbleContainer;   // Parent for bubbles
    public Image dialogueBackground;


    public GameObject choicesPanel;
    public Button choiceButtonPrefab;

    // Bubble management
    private GameObject topBubble;
    private GameObject bottomBubble;
    public float bubbleSpacing = 100f; // Space between bubbles (in pixels)

    // Dialogue state
    public static bool isDialogueActive = false;

    void Start()
    {
        // Hide dialogue panel at start
        dialoguePanel.SetActive(false);

        // Initialize story but don't start it
        story = new Story(inkJSON.text);
    }

    public void StartDialogueFromKnot(string knotName)
    {
        Debug.Log("Starting dialogue from knot: " + knotName);

        // Clean up any existing bubbles
        if (topBubble != null) Destroy(topBubble);
        if (bottomBubble != null) Destroy(bottomBubble);
        topBubble = null;
        bottomBubble = null;

        // Show dialogue panel
        dialoguePanel.SetActive(true);
        isDialogueActive = true;

        // Jump to the specified knot
        story.ChoosePathString(knotName);

        SetDialogueOpacity(0.80f);

        // Begin the dialogue
        RefreshView();
    }

    void RefreshView()
    {
        // Clear choices
        RemoveChildren(choicesPanel.transform);

        // Check if story is completely finished
        if (!story.canContinue && story.currentChoices.Count == 0)
        {
            // No more content and no choices - we're done
            EndDialogue();
            return;
        }

        // Get the next line of story
        if (story.canContinue)
        {
            // If there's a bottom bubble, move it to the top position
            if (bottomBubble != null)
            {
                if (topBubble != null) Destroy(topBubble);
                topBubble = bottomBubble;

                // Move it to the top position
                RectTransform rt = topBubble.GetComponent<RectTransform>();
                if (rt != null)
                {
                    rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, 150); // Move up
                }
            }

            // Get new text for bottom bubble
            string text = story.Continue().Trim();

            // Create new NPC bubble (gray)
            bottomBubble = Instantiate(npcBubblePrefab, bubbleContainer);

            // Set text in the bubble
            TMP_Text bubbleText = bottomBubble.GetComponentInChildren<TMP_Text>();
            if (bubbleText != null)
            {
                bubbleText.text = text;
            }

            // Position the bottom bubble
            RectTransform bottomRT = bottomBubble.GetComponent<RectTransform>();
            if (bottomRT != null)
            {
                bottomRT.anchoredPosition = new Vector2(bottomRT.anchoredPosition.x, -bubbleSpacing);

            }

            // Display choices if any
            if (story.currentChoices.Count > 0)
            {
                for (int i = 0; i < story.currentChoices.Count; i++)
                {
                    Choice choice = story.currentChoices[i];
                    Button button = CreateChoiceView(choice.text.Trim());
                    int choiceIndex = i; // Create local copy for closure

                    button.onClick.AddListener(() => {
                        // Move NPC bubble to top
                        if (topBubble != null) Destroy(topBubble);
                        topBubble = bottomBubble;

                        // Move it to the top position
                        RectTransform rt = topBubble.GetComponent<RectTransform>();
                        if (rt != null)
                        {
                            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, 150); // Move up
                        }

                        // Select this choice in the story
                        story.ChooseChoiceIndex(choiceIndex);

                        // Get the full dialogue line that results from this choice
                        if (story.canContinue)
                        {
                            // Create player dialogue bubble (blue) with the FULL text
                            bottomBubble = Instantiate(pcBubblePrefab, bubbleContainer);

                            // Get the full text that follows this choice
                            string fullDialogueText = story.Continue().Trim();

                            // Set text in player bubble
                            TMP_Text dialogueText = bottomBubble.GetComponentInChildren<TMP_Text>();
                            if (dialogueText != null)
                            {
                                dialogueText.text = fullDialogueText;
                            }

                            // Position the player bubble
                            RectTransform playerRT = bottomBubble.GetComponent<RectTransform>();
                            if (playerRT != null)
                            {
                                playerRT.anchoredPosition = new Vector2(playerRT.anchoredPosition.x, -bubbleSpacing);
                            }

                            // Continue after a short delay to show the response
                            if (story.canContinue)
                            {
                                Invoke("RefreshView", 1.5f);
                            }
                            else
                            {
                                // End dialogue after a delay if no more content
                                Invoke("EndDialogue", 2.0f);
                            }
                        }
                        else
                        {
                            // End dialogue after a delay if no more content
                            Invoke("EndDialogue", 2.0f);
                        }
                    });
                }
            }
            else if (!story.canContinue)
            {
                // Display the final text for a moment before ending
                Invoke("EndDialogue", 2.0f);
            }
        }
        else if (!story.canContinue)
        {
            // Display the final text for a moment before ending
            Invoke("EndDialogue", 5.0f);
        }
    }

    Button CreateChoiceView(string text)
    {
        Button choice = Instantiate(choiceButtonPrefab, choicesPanel.transform, false);
        TMP_Text choiceText = choice.GetComponentInChildren<TMP_Text>();
        if (choiceText == null)
        {
            // Fall back to regular Text if TMP_Text not found
            Text legacyText = choice.GetComponentInChildren<Text>();
            if (legacyText != null)
                legacyText.text = text;
        }
        else
        {
            choiceText.text = text;
        }
        return choice;
    }

    void RemoveChildren(Transform parent)
    {
        int childCount = parent.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }

    public void DisplayCharacterName(GameObject character)
    {
        if (nameText != null && character != null)
        {
            nameText.text = character.name;
        }
    }

    public void EndDialogue()
    {
        if (nameText != null)
        {
            nameText.text = "";
        }

        // Clean up bubbles
        if (topBubble != null) Destroy(topBubble);
        if (bottomBubble != null) Destroy(bottomBubble);
        topBubble = null;
        bottomBubble = null;

        dialoguePanel.SetActive(false);
        isDialogueActive = false;

        SetDialogueOpacity(0f);
    }

    public void SetDialogueOpacity(float alpha)
    {
        if (dialogueBackground != null)
        {
            Color color = dialogueBackground.color;
            color.a = Mathf.Clamp01(alpha); // make sure it's between 0–1
            dialogueBackground.color = color;
        }
    }

}