using UnityEngine;
using Ink.Runtime;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public TextAsset inkJSON;
    private Story story;

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

    void Start()
    {
        dialoguePanel.SetActive(false);
        story = new Story(inkJSON.text);
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

    void RefreshView()
    {
        RemoveChildren(choicesPanel.transform);

        if (!story.canContinue && story.currentChoices.Count == 0)
        {
            EndDialogue();
            return;
        }

        if (story.canContinue)
        {
            if (bottomBubble != null)
            {
                Destroy(topBubble);
                topBubble = bottomBubble;

                RectTransform rt = topBubble.GetComponent<RectTransform>();
                if (rt != null)
                    rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, 150);
            }

            string text = story.Continue().Trim();

            bottomBubble = Instantiate(npcBubblePrefab, bubbleContainer);
            TMP_Text bubbleText = bottomBubble.GetComponentInChildren<TMP_Text>();
            if (bubbleText != null)
                bubbleText.text = text;

            RectTransform bottomRT = bottomBubble.GetComponent<RectTransform>();
            if (bottomRT != null)
                bottomRT.anchoredPosition = new Vector2(bottomRT.anchoredPosition.x, -bubbleSpacing);

            if (story.currentChoices.Count > 0)
            {
                for (int i = 0; i < story.currentChoices.Count; i++)
                {
                    Choice choice = story.currentChoices[i];
                    Button button = CreateChoiceView(choice.text.Trim());

                    int choiceIndex = i;
                    button.onClick.AddListener(() =>
                    {
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
                Invoke("EndDialogue", 2f);
            }
        }
        else if (!story.canContinue)
        {
            Invoke("EndDialogue", 5f);
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

        // Let characters know dialogue is done
        CharacterInteraction[] characters = FindObjectsOfType<CharacterInteraction>();
        foreach (var c in characters)
        {
            c.DialogueEnded();
        }
    }

    // Optional utility to get/set Ink vars if you need it
    public object GetStoryVariable(string varName)
    {
        return story?.variablesState[varName];
    }

    public void SetStoryVariable(string varName, object value)
    {
        if (story != null)
            story.variablesState[varName] = value;
    }
}