using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterPrefab
{
    public string characterName;
    public GameObject prefab;
    public Transform spawnPosition;
    public StoryStateManager story_state_manager;
}

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance { get; private set; }

    [Header("List of all characters and their spawn locations")]
    [SerializeField] public List<CharacterPrefab> characterPrefabs = new List<CharacterPrefab>();

    [Header("Story State Manager Reference")]
    [SerializeField] private StoryStateManager storyStateManager;

    [Header("Common References")]
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private GameObject interactionPromptPrefab;

    // Tracks currently spawned characters
    private Dictionary<string, GameObject> spawnedCharacters = new Dictionary<string, GameObject>();

    //public event System.Action OnCharactersSpawned;
    public bool AreCharactersSpawned { get; private set; } = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        // Find StoryStateManager if not set in inspector
        if (storyStateManager == null)
        {
            storyStateManager = FindObjectOfType<StoryStateManager>();
            if (storyStateManager == null)
            {
                Debug.LogError("No StoryStateManager found. Character references won't be populated.");
            }
            else
            {
                Debug.Log("StoryStateManager found!");
            }
        }

        // Find DialogueManager if not set in inspector
        if (dialogueManager == null)
        {
            dialogueManager = FindObjectOfType<DialogueManager>();
            if (dialogueManager == null)
            {
                Debug.LogError("No DialogueManager found. Character dialogue won't function properly.");
            }
            else
            {
                Debug.Log("DialogueManager found!");
            }
            Debug.Log("CharacterManager Start() fired!");


        }

    }

    void Start()
    {
        Debug.Log("Character Manager Start!!!");

        // UNCOMMENT THIS - You need to spawn characters first!
        SpawnAllCharactersDisabled();
        AreCharactersSpawned = true;

        // THEN set up their visibility
        if (storyStateManager != null)
        {
            storyStateManager.upanddown();
        }
        else
        {
            Debug.LogError("Cannot call upanddown - storyStateManager is null!");
        }
    }

    // UNCOMMENT this entire method:
    private void SpawnAllCharactersDisabled()
    {
        foreach (var character in characterPrefabs)
        {
            if (!spawnedCharacters.ContainsKey(character.characterName))
            {
                if (character.prefab == null)
                {
                    Debug.LogError($"Missing prefab for character: {character.characterName}");
                    continue;
                }

                if (character.spawnPosition == null)
                {
                    Debug.LogError($"Missing spawn position for character: {character.characterName}");
                    continue;
                }

                GameObject instance = Instantiate(
                    character.prefab,
                    character.spawnPosition.position,
                    Quaternion.identity
                );

                instance.name = character.characterName;
                spawnedCharacters.Add(character.characterName, instance);

                // Populate the corresponding reference in StoryStateManager
                if (storyStateManager != null)
                {
                    storyStateManager.AssignCharacterReference(character.characterName, instance);
                }

                // Set up the character's interaction components
                SetupCharacterInteraction(instance);
            }
        }
        Debug.Log("Spawned All character Disabled");
    }

    private void SetupCharacterInteraction(GameObject characterObject)
    {
        CharacterInteraction interaction = characterObject.GetComponent<CharacterInteraction>();
        if (interaction != null)
        {
            // Set the dialogue manager reference
            if (dialogueManager != null)
            {
                interaction.SetDialogueManager(dialogueManager);
            }

            // Create and set up interaction prompt
            if (interactionPromptPrefab != null)
            {
                GameObject prompt = Instantiate(interactionPromptPrefab, characterObject.transform);
                prompt.SetActive(false); // Initially hidden
                interaction.SetInteractionPrompt(prompt);
            }
        }
        else
        {
            Debug.LogWarning($"Character {characterObject.name} doesn't have a CharacterInteraction component.");
        }
    }

    public GameObject GetCharacter(string characterName)
    {
        if (spawnedCharacters.TryGetValue(characterName, out GameObject character))
        {
            return character;
        }
        return null;
    }

    public void DespawnCharacter(string characterName)
    {
        if (spawnedCharacters.ContainsKey(characterName) && spawnedCharacters[characterName] != null)
        {
            Destroy(spawnedCharacters[characterName]);
            spawnedCharacters.Remove(characterName);
        }
    }

    public void HideCharacter(string characterName)
    {
        Debug.Log($"HideCharacter called for: {characterName}");
        GameObject character = GetCharacter(characterName);
        if (character != null)
        {
            Debug.Log($"Found character {characterName}, setting to inactive");
            character.SetActive(false);
            Debug.Log($"Character {characterName} is now active: {character.activeSelf}");
        }
        else
        {
            Debug.LogError($"Character {characterName} not found in spawnedCharacters!");

            
        }
    }

    public void ShowCharacter(string characterName)
    {
        Debug.Log($"ShowCharacter called for: {characterName}");

        GameObject character = GetCharacter(characterName);
        if (character)
        {
            Debug.Log($"Found character {characterName}, setting to active");
            character.SetActive(true);
            Debug.Log($"Character {characterName} is now active: {character.activeSelf}");
        }
        else
        {
            Debug.LogError($"Character {characterName} not found in spawnedCharacters!");
        }
    }

}
    
