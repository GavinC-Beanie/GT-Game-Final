using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    // Ink story
    public TextAsset inkJSON;
    private Story story;
    
    // UI References
    public GameObject dialoguePanel;
    public Text nameText;
    public Text dialogueText;
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
        
        // Show dialogue panel
        dialoguePanel.SetActive(true);
        isDialogueActive = true;
        
        // Jump to the specified knot
        story.ChoosePathString(knotName);
        
        // Start the dialogue
        ContinueStory();
    }
    
    void ContinueStory()
    {
        if (story.canContinue)
        {
            // Get the next line of dialogue
            string text = story.Continue();
            Debug.Log("Story continued with text: " + text);
            
            // Display the text
            dialogueText.text = text;
            
            // Check for character name tags
            if (story.currentTags.Count > 0)
            {
                foreach (string tag in story.currentTags)
                {
                    // Check if this tag is for character name
                    if (tag.StartsWith("speaker:"))
                    {
                        string speakerName = tag.Substring(8); // Remove "speaker:" prefix
                        nameText.text = speakerName;
                    }
                }
            }
            
            // Display choices if any
            if (story.currentChoices.Count > 0)
            {
                DisplayChoices();
            }
        }
        else
        {
            // End the dialogue
            EndDialogue();
        }
    }
    
    void DisplayChoices()
    {
        // Clear existing choices
        foreach (Transform child in choicesPanel.transform)
        {
            Destroy(child.gameObject);
        }
        
        // Create new choice buttons
        for (int i = 0; i < story.currentChoices.Count; i++)
        {
            Choice choice = story.currentChoices[i];
            
            Button button = Instantiate(choiceButtonPrefab, choicesPanel.transform);
            button.GetComponentInChildren<Text>().text = choice.text;
            
            int choiceIndex = i; // Need this to create a local copy for the closure
            button.onClick.AddListener(() => {
                story.ChooseChoiceIndex(choiceIndex);
                ContinueStory();
            });
        }
    }
    
    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        isDialogueActive = false;
    }
}