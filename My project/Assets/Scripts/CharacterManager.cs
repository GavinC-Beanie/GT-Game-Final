using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance { get; private set; }

    [System.Serializable]
    public class CharacterPrefab
    {
        public string characterName;
        public GameObject prefab;
        public Transform spawnPosition;
    }

    [Header("List of all characters and their spawn locations")]
    public List<CharacterPrefab> characterPrefabs = new List<CharacterPrefab>();

    // Tracks currently spawned characters
    private Dictionary<string, GameObject> spawnedCharacters = new Dictionary<string, GameObject>();

    public event System.Action OnCharactersSpawned;
    public bool AreCharactersSpawned { get; private set; } = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        // Spawn everyone off-screen (disabled) first so visibility can be toggled
        SpawnAllCharactersDisabled();

        AreCharactersSpawned = true;
        OnCharactersSpawned?.Invoke();

        if (StoryStateManager.Instance != null)
        {
            StoryStateManager.Instance.ProcessPendingVisibilityChanges();
        }
    }

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
            instance.SetActive(false); // VERY IMPORTANT
            spawnedCharacters.Add(character.characterName, instance);
        }
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
        GameObject character = GetCharacter(characterName);
        if (character != null)
        {
            character.SetActive(false);
        }
    }


    public void HideAllCharacters()
    {
        foreach (var character in spawnedCharacters.Values)
        {
            if (character != null)
            {
                character.SetActive(false);
            }
        }
    }

    public void ShowCharacter(string characterName)
    {
        GameObject character = GetCharacter(characterName);
        if (character != null)
        {
            character.SetActive(true);
        }
    }

    public bool IsCharacterSpawned(string characterName)
    {
        return spawnedCharacters.ContainsKey(characterName);
    }
}
