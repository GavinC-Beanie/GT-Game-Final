using UnityEngine;
using Ink.Runtime;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class DialogueManager : MonoBehaviour
{
    public TextAsset inkJSON;
    private Story story;
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
        Debug.Log("Where the fuck is this at huh");
        dialoguePanel.SetActive(false);
        story = new Story(inkJSON.text);
        string text = story.Continue().Trim();



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
            Debug.Log("Can Continue 1");

            if (bottomBubble != null)
            {
                Debug.Log("Moving previous bubble to top");
                Destroy(topBubble);
                topBubble = bottomBubble;
                RectTransform rt = topBubble.GetComponent<RectTransform>();
                if (rt != null)
                    rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, 150);
            }

            Debug.Log("Can Continue 2");

            
    

            // Check if prefab exists
            if (npcBubblePrefab == null)
            {
                Debug.LogError("npcBubblePrefab is NULL! Assign it in the Inspector!");
                return;
            }
            else
            {
                Debug.Log("npcBubblePrefab exists");
            }

            Debug.Log("Can Continue 3");

            // Check if container exists
            if (bubbleContainer == null)
            {
                Debug.LogError("bubbleContainer is NULL! Assign it in the Inspector!");
                return;
            }
            else
            {
                Debug.Log($"bubbleContainer exists: {bubbleContainer.name}");
            }

            Debug.Log("About to instantiate bubble...");
            bottomBubble = Instantiate(npcBubblePrefab, bubbleContainer);

            if (bottomBubble == null)
            {
                Debug.LogError("Failed to create bottomBubble!");
                return;
            }
            else
            {
                Debug.Log($"bottomBubble created: {bottomBubble.name}");
            }

            Debug.Log("Can Continue 4");

            Debug.Log("Setting bubble position...");
            RectTransform bottomRT = bottomBubble.GetComponent<RectTransform>();
            if (bottomRT == null)
            {
                Debug.LogError("No RectTransform on bubble!");
            }
            else
            {
                bottomRT.anchoredPosition = new Vector2(bottomRT.anchoredPosition.x, -bubbleSpacing);
                Debug.Log($"Bubble positioned at: {bottomRT.anchoredPosition}");
            }

            Debug.Log("Can Continue 4");

            Debug.Log("Looking for TMP_Text component...");
            TMP_Text bubbleText = bottomBubble.GetComponentInChildren<TMP_Text>();


            string text = story.Continue().Trim();

            if (bubbleText && story.currentChoices.Count == 0)
            {
                Invoke("EndDialogue", 2f);
            }
            else
            {
                
                bubbleText.text = text;
                Debug.Log($"Text set to: '{bubbleText.text}'");

            }


                // Check what happened after continuing
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
                                Invoke("EndDialogue", 2f);
                        }
                        else
                        {
                            Invoke("EndDialogue", 2f);
                        }
                    });
                }
            }
            else if (!story.canContinue)
            {
                Debug.Log("No more content after this bubble - ending in 2 seconds");
                Invoke("EndDialogue", 2f);
            }
        }
        else
        {
            Debug.Log("Story cannot continue - ending dialogue");
            Invoke("EndDialogue", 2f);
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

    public void EndDialogue()
    {

        Debug.Log($"EndDialogue called for character: {currentCharacterName}");

        // Call OnCharacterMet for the current character
        if (!string.IsNullOrEmpty(currentCharacterName) && StoryStateManager.Instance != null)
        {
            Debug.Log($"Sent to OnCharacterMet: {currentCharacterName}");
            StoryStateManager.Instance.OnCharacterMet(currentCharacterName);
        }
        else
        {
            Debug.LogError("No current character or StoryStateManager is null!");
        }

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