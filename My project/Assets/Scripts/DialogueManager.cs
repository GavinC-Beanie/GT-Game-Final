using UnityEngine;
using Ink.Runtime;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;

public class DialogueManager : MonoBehaviour
{
    public TextAsset inkJSON;
    public Story story;
    //public List<groupItem> items = new List<groupItem>();

    public GameObject dialoguePanel;
    public TMP_Text nameText;

    public GameObject npcBubblePrefab;
    public GameObject pcBubblePrefab;
    public Transform bubbleContainer;
    public Image dialogueBackground;

    public GameObject choicesPanel;
    public Button choiceButtonPrefab;

    private GameObject topBubble;
    private GameObject bottomBubble;

    public float bubbleSpacing = 100f;

    public static bool isDialogueActive = false;

    private StoryStateManager storyManager;

    void Awake()
    {
        storyManager = FindObjectOfType<StoryStateManager>();
        if (storyManager == null)
        {
            Debug.LogWarning("No StoryStateManager found in scene.");
        }
        
    }

    private string currentCharacterName = "";

    void Start()
{
    Debug.Log("DialogueManager Start");
    dialoguePanel.SetActive(false);
    
    if (inkJSON == null)
    {
        Debug.LogError("inkJSON is null! Assign your Ink file in the Inspector!");
        return;
    }
    
    try
    {
        Debug.Log("Creating Story from Ink JSON...");
        story = new Story(inkJSON.text);
        Debug.Log("Story created successfully");
        
        // Test if we can access variables
        Debug.Log("Testing variable access...");
        foreach (string varName in story.variablesState)
        {
            object value = story.variablesState[varName];
            Debug.Log($"Found variable: {varName} = {value}");
        }
        
        // Bind external function
        Debug.Log("Binding external function...");
        try
        {
            story.BindExternalFunction("OnVariableChanged", (string variableName, string characterName) => {
                Debug.Log($"EXTERNAL FUNCTION CALLED!");
                Debug.Log($"Variable: {variableName}");
                Debug.Log($"Character: {characterName}");
                
                if (storyManager != null)
                {
                    Debug.Log($"Calling storyManager.OnInkVariableChanged()");
                    storyManager.OnInkVariableChanged(variableName, characterName);
                }
                else
                {
                    Debug.LogError("storyManager is null!");
                }
            });
            
            Debug.Log("External function binding completed successfully");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"External function binding failed: {e.Message}");
            Debug.LogError($"This means your Ink file is missing: EXTERNAL OnVariableChanged(variableName, characterName)");
        }
        
        Debug.Log("Ready for dialogue! External function will be tested when you talk to a character.");
    }
    catch (System.Exception e)
    {
        Debug.LogError($"Failed to create Story: {e.Message}");
    }
}



    public void StartDialogueFromKnot(string knotName)
    {
        if (string.IsNullOrEmpty(knotName) || story == null) return;

        Destroy(topBubble);
        Destroy(bottomBubble);
        topBubble = null;
        bottomBubble = null;

        dialoguePanel.SetActive(true);
        isDialogueActive = true;

        story.ChoosePathString(knotName);
        SetDialogueOpacity(0.85f);
        RefreshView();
    }

    public void SetCurrentCharacter(string characterName)
    {
        currentCharacterName = characterName;
        Debug.Log($"Current character set to: {currentCharacterName}");
    }




    void RefreshView()
    {
        Debug.Log("RefreshView called");
        Debug.Log($"story.canContinue: {story.canContinue}");
        Debug.Log($"story.currentChoices.Count: {story.currentChoices.Count}");

        RemoveChildren(choicesPanel.transform);

        // Check if dialogue should end FIRST, before anything else
        if (!story.canContinue && story.currentChoices.Count == 0)
        {
            Debug.Log("No more content and no choices - ending dialogue");
            Invoke("EndDialogue", 2f);
            return;
        }

        if (story.canContinue)
        {
            Debug.Log("Story can continue - processing...");

            // Move previous bubble up
            if (bottomBubble != null)
            {
                Debug.Log("Moving previous bubble to top");
                Destroy(topBubble);
                topBubble = bottomBubble;
                RectTransform rt = topBubble.GetComponent<RectTransform>();
                if (rt != null)
                    rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, 150);
            }

            // Check prefabs
            if (npcBubblePrefab == null)
            {
                Debug.LogError("npcBubblePrefab is NULL!");
                return;
            }

            if (bubbleContainer == null)
            {
                Debug.LogError("bubbleContainer is NULL!");
                return;
            }

            // Create new bubble
            Debug.Log("Creating new bubble...");
            bottomBubble = Instantiate(npcBubblePrefab, bubbleContainer);

            if (bottomBubble == null)
            {
                Debug.LogError("Failed to create bottomBubble!");
                return;
            }

            // Position bubble
            RectTransform bottomRT = bottomBubble.GetComponent<RectTransform>();
            if (bottomRT != null)
                bottomRT.anchoredPosition = new Vector2(bottomRT.anchoredPosition.x, -bubbleSpacing);

            // Get and process text with comprehensive end detection
            string text = story.Continue().Trim();
            Debug.Log($"Story text: '{text}' (Length: {text.Length})");

            // Multiple ways to detect dialogue end
            bool isDialogueEnd = false;

            // Check 1: Empty or whitespace text
            if (string.IsNullOrWhiteSpace(text))
            {
                Debug.Log("Empty text detected - dialogue ending");
                isDialogueEnd = true;
            }

            // Check 2: No more content AND no choices
            if (!story.canContinue && story.currentChoices.Count == 0)
            {
                Debug.Log("No more content and no choices - dialogue ending");
                isDialogueEnd = true;
            }

            // Check 3: Special end marker (optional)
            if (text.Contains("[END_DIALOGUE]"))
            {
                Debug.Log("End dialogue marker found - dialogue ending");
                isDialogueEnd = true;
                text = text.Replace("[END_DIALOGUE]", "").Trim(); // Remove marker from display
            }

            if (isDialogueEnd)
            {
                // Don't create bubble for empty content
                if (string.IsNullOrWhiteSpace(text) && bottomBubble != null)
                {
                    Destroy(bottomBubble);
                    bottomBubble = null;
                }

                Invoke("EndDialogue", 3f);
                return;
            }

            // Only set text if we have actual content
            if (!string.IsNullOrWhiteSpace(text))
            {
                TMP_Text bubbleText = bottomBubble.GetComponentInChildren<TMP_Text>();
                if (bubbleText != null)
                {
                    bubbleText.text = text;
                    Debug.Log($"Text set successfully: '{text}'");
                }
                else
                {
                    Debug.LogError("bubbleText is null!");
                }
            }

            // Check what to do next
            Debug.Log($"After Continue - canContinue: {story.canContinue}, choices: {story.currentChoices.Count}");

            if (story.currentChoices.Count > 0)
            {
                Debug.Log("Creating choice buttons...");
                for (int i = 0; i < story.currentChoices.Count; i++)
                {
                    Choice choice = story.currentChoices[i];
                    Button button = CreateChoiceView(choice.text.Trim());

                    int choiceIndex = i;
                    button.onClick.AddListener(() =>
                    {
                        Debug.Log($"Choice {choiceIndex} clicked");

                        Destroy(topBubble);
                        topBubble = bottomBubble;

                        RectTransform rt = topBubble.GetComponent<RectTransform>();
                        if (rt != null)
                            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, 150);

                        story.ChooseChoiceIndex(choiceIndex);

                        if (story.canContinue)
                        {
                            bottomBubble = Instantiate(pcBubblePrefab, bubbleContainer);
                            string fullText = story.Continue().Trim();

                            TMP_Text playerText = bottomBubble.GetComponentInChildren<TMP_Text>();
                            if (playerText != null)
                                playerText.text = fullText;

                            RectTransform playerRT = bottomBubble.GetComponent<RectTransform>();
                            if (playerRT != null)
                                playerRT.anchoredPosition = new Vector2(playerRT.anchoredPosition.x, -bubbleSpacing);

                            if (story.canContinue)
                                Invoke("RefreshView", 1.5f);
                            else
                                Invoke("EndDialogue", 3f);
                        }
                        else
                        {
                            Invoke("EndDialogue", 3f);
                        }
                    });
                }
            }
            else if (!story.canContinue)
            {
                Debug.Log("No choices and cannot continue - ending dialogue");
                Invoke("EndDialogue", 3f);
            }
            else
            {
                Debug.Log("No choices but can still continue - will continue automatically");
                // Let the story continue processing (external functions, variable changes, etc.)
                Invoke("RefreshView", 0.5f);
            }
        }
        else
        {
            Debug.Log("Story cannot continue - ending dialogue");
            Invoke("EndDialogue", 3f);
        }
    }




    Button CreateChoiceView(string text)
    {
        Button choice = Instantiate(choiceButtonPrefab, choicesPanel.transform, false);
        TMP_Text choiceText = choice.GetComponentInChildren<TMP_Text>();
        if (choiceText != null)
            choiceText.text = text;
        return choice;
    }

    void RemoveChildren(Transform parent)
    {
        for (int i = parent.childCount - 1; i >= 0; --i)
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

    public void SetDialogueOpacity(float alpha)
    {
        if (dialogueBackground != null)
        {
            Color color = dialogueBackground.color;
            color.a = Mathf.Clamp01(alpha);
            dialogueBackground.color = color;
        }
    }


    public void CheckVariableChanges()
    {
        if (story != null)
        {
            Debug.Log("Checking current variable values:");
            foreach (string varName in story.variablesState)
            {
                object value = story.variablesState[varName];
                Debug.Log($"{varName} = {value}");
            }
        }
    }


    public void EndDialogue()
    {

        Debug.Log($"EndDialogue called for character: {currentCharacterName}");

        CheckVariableChanges();

        if (nameText != null)
            nameText.text = "";

        Destroy(topBubble);
        Destroy(bottomBubble);
        topBubble = null;
        bottomBubble = null;

        dialoguePanel.SetActive(false);
        isDialogueActive = false;

        SetDialogueOpacity(0f);

        // Apply any queued character visibility changes
        if (storyManager != null)
            storyManager.ProcessPendingVisibilityChanges();


    }
}