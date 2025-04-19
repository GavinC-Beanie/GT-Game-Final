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
	public TMP_Text topTextBox;     
	public TMP_Text bottomTextBox;
    public GameObject choicesPanel;
    public Button choiceButtonPrefab;
    
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
        
        // Reset text boxes
        topTextBox.text = "";
        bottomTextBox.text = "";
    
        
        // Show dialogue panel
        dialoguePanel.SetActive(true);
        isDialogueActive = true;
        
        // Jump to the specified knot
        story.ChoosePathString(knotName);
        
        // Begin the dialogue
        RefreshView();
    }

   void RefreshView() 
{
    // Clear choices
    RemoveChildren(choicesPanel.transform);
    
    // Check if story is completely finished
    if (!story.canContinue && story.currentChoices.Count == 0) {
        // No more content and no choices - we're done
        EndDialogue();
        return;
    }
    
    // Get the next line of story
    if (story.canContinue) {
        // Shift previous text to top box if there is any
        if (!string.IsNullOrEmpty(bottomTextBox.text)) {
            topTextBox.text = bottomTextBox.text;
        }
        
        // Get new text for bottom box
        string text = story.Continue().Trim();
        bottomTextBox.text = text;
        
        // Display choices if any
        if (story.currentChoices.Count > 0) {
            for (int i = 0; i < story.currentChoices.Count; i++) {
                Choice choice = story.currentChoices[i];
                Button button = CreateChoiceView(choice.text.Trim());
                int choiceIndex = i; // Create local copy for closure
                button.onClick.AddListener(() => {
                    // Move NPC text to top
                    topTextBox.text = bottomTextBox.text;
                    
                    // Select this choice in the story
                    story.ChooseChoiceIndex(choiceIndex);
                    
                    // Get the full dialogue line that results from the choice
                    if (story.canContinue) {
                        bottomTextBox.text = story.Continue().Trim();
                        
                        // Continue after a short delay
                        if (story.canContinue || story.currentChoices.Count > 0) {
                            Invoke("RefreshView", 1.5f);
                        } else {
                            // End dialogue after a delay if no more content
                            Invoke("EndDialogue", 2.0f);
                        }
                    } else {
                        // End dialogue after a delay if no more content
                        Invoke("EndDialogue", 2.0f);
                    }
                });
            }
        }
        else if (!story.canContinue) {
            // Display the final text for a moment before ending
            Invoke("EndDialogue", 2.0f);
        }
    }
    else if (!story.canContinue) {
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
        for (int i = childCount - 1; i >= 0; --i) {
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
    dialoguePanel.SetActive(false);
    isDialogueActive = false;
}
}