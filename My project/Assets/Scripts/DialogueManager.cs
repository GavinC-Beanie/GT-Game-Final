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
        
        
    }
    
    void RefreshView () {
		// Remove all the UI on screen
		RemoveChildren ();
		
		// Read all the content until we can't continue any more
		while (story.canContinue) {
			// Continue gets the next line of the story
			string text = story.Continue ();
			// This removes any white space from the text.
			text = text.Trim();
			// Display the text on screen!
			CreateContentView(text);
		}

		// Display all the choices, if there are any!
		if(story.currentChoices.Count > 0) {
			for (int i = 0; i < story.currentChoices.Count; i++) {
				Choice choice = story.currentChoices [i];
				Button button = CreateChoiceView (choice.text.Trim ());
				// Tell the button what to do when we press it
				button.onClick.AddListener (delegate {
					OnClickChoiceButton (choice);
				});
			}
		}
    }
    
    void RemoveChildren () {
		int childCount = dialoguePanel.transform.childCount;
		for (int i = childCount - 1; i >= 0; --i) {
			Destroy (dialoguePanel.transform.GetChild (i).gameObject);
		}
	}

    Button CreateChoiceView (string text) {
		// Creates the button from a prefab
		Button choice = Instantiate (choiceButtonPrefab) as Button;
		choice.transform.SetParent (dialoguePanel.transform, false);
		
		// Gets the text from the button prefab
		Text choiceText = choice.GetComponentInChildren<Text> ();
		choiceText.text = text;

		// Make the button expand to fit the text
		HorizontalLayoutGroup layoutGroup = choice.GetComponent <HorizontalLayoutGroup> ();
		layoutGroup.childForceExpandHeight = false;

		return choice;
	}

    void OnClickChoiceButton (Choice choice) {
		story.ChooseChoiceIndex (choice.index);
		RefreshView();
	}

    void CreateContentView (string text) {
		Text storyText = Instantiate (dialogueText) as Text;
		storyText.text = text;
		storyText.transform.SetParent (dialoguePanel.transform, false);
	}

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        isDialogueActive = false;
    }


}