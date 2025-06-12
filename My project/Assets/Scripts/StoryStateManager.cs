using UnityEngine;
using System.Collections.Generic;
using Ink.Runtime;
using System.Collections;
using Unity.VisualScripting;  // Ensure you have the Ink runtime imported to use Ink.Runtime.Story

public class StoryStateManager : MonoBehaviour
{

    public static StoryStateManager Instance { get; private set; }

    private Dictionary<string, bool> characterVisibility = new Dictionary<string, bool>();

    // Define story phase states
   

   

    // Assign these references in the Unity Inspector to the corresponding character GameObjects
    public GameObject The_Commisioner;
    public GameObject Bill;
    public GameObject Pumplscroob;
    public GameObject The_Crank;
    public GameObject Gobster;
    public GameObject Grandma_Gob;
    public GameObject Derpy_Unicorn_Buttponey;
    public GameObject Gromblar;
    public GameObject Bill_the_Drawggin;
    public GameObject Tent_Dweller;
    public GameObject Grandpa;
    public GameObject Wheres_Bill;

    // Reference to the Ink story, if using Ink for narrative state
    public Story inkStory;

    // Flags to track which characters have been met in the current phase
    private bool firstQuestComMet, firstQuestBillMet, firstQuestPumpMet, firstQuestCrankMet, firstQuestGobsterMet, firstQuestGramMet;
    private bool secondQuestBillMet, secondQuestCrankMet, secondQuestDerpyMet, secondQuestPumpMet, secondQuestGromblarMet;
    private bool thirdQuestBillMet, thirdQuestComMet;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        
        Debug.Log("Wow");
    }
    void Start()
    {
        Debug.Log("Where in line are we");
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }


    }

    

    public void ApplyAllCharacters()
    {
        Debug.Log("Yo they here");
        if (The_Commisioner) ApplyCharacterVisibility("The_Commisioner", true);
        
    }


    private Queue<CharacterVisibilityChange> pendingVisibilityChanges = new Queue<CharacterVisibilityChange>();

    private struct CharacterVisibilityChange
    {
        public string characterName;
        public bool isVisible;
    }


    public void UpdateCharacterVisibility(string characterName, bool isVisible)
    {
        
        if (DialogueManager.isDialogueActive)
        {
            Debug.Log(characterName + " was sent to list");
            pendingVisibilityChanges.Enqueue(new CharacterVisibilityChange
            {
                characterName = characterName,
                isVisible = isVisible,
               
            });
        }
        else
        {
            Debug.Log(characterName + " was Applied");
            ApplyCharacterVisibility(characterName, isVisible);
        }
    }

    public void ProcessPendingVisibilityChanges()
    {
        Debug.Log("Starting ProcessPendingVisibilityChanges. Queue count: " + pendingVisibilityChanges.Count);

        int processedCount = 0;
        while (pendingVisibilityChanges.Count > 0)
        {
            var change = pendingVisibilityChanges.Dequeue();
            processedCount++;

            Debug.Log($"Processing #{processedCount}: {change.characterName} -> {(change.isVisible ? "SHOW" : "HIDE")}");

            ApplyCharacterVisibility(change.characterName, change.isVisible);

            Debug.Log($"Remaining in queue: {pendingVisibilityChanges.Count}");
        }

        Debug.Log($"Finished processing. Total processed: {processedCount}");

    }
   

    // Also update ApplyCharacterVisibility to show what actually happens:
    private void ApplyCharacterVisibility(string characterName, bool isVisible)
    {
        Debug.Log($"ApplyCharacterVisibility called: {characterName} -> {(isVisible ? "SHOW" : "HIDE")}");

        characterVisibility[characterName] = isVisible;

        if (!CharacterManager.Instance)
        {
            Debug.LogError("CharacterManager.Instance is null!");
            return;
        }

        if (isVisible)
        {
            Debug.Log($"Calling ShowCharacter for: {characterName}");
            CharacterManager.Instance.ShowCharacter(characterName);
        }
        else
        {
            Debug.Log($"Calling HideCharacter for: {characterName}");
            CharacterManager.Instance.HideCharacter(characterName);
        }

        Debug.Log($"ApplyCharacterVisibility completed for: {characterName}");
    }


    public void OnInkVariableChanged(string variableName, string characterName)
    {
        Debug.Log($"Processing Ink variable change: {variableName} for {characterName}");

        switch (variableName)
        {
            case "met_com":
                Debug.Log("Commissioner conversation completed!");
                firstQuestComMet = true;
                UpdateCharacterVisibility("The_Commisioner", false);
                UpdateCharacterVisibility("Bill_the_Drawggin", true);
                UpdateCharacterVisibility("Pumplscroob", true);
                UpdateCharacterVisibility("The_Crank", true);
                UpdateCharacterVisibility("Gobster", true);
                break;

            case "met_bill":
                Debug.Log("Bill conversation completed!");
                firstQuestBillMet = true;
                UpdateCharacterVisibility("Bill_the_Drawggin", false);
                if (firstQuestCrankMet)
                {
                    UpdateCharacterVisibility("Grandma_Gob", true);
                }
                break;

            case "met_crank":
                Debug.Log("Crank conversation completed!");
                firstQuestCrankMet = true;
                UpdateCharacterVisibility("The_Crank", false);

                if (firstQuestBillMet)
                {
                    UpdateCharacterVisibility("Grandma_Gob", true);
                }
                break;

            case "met_pump":
                Debug.Log("Pump conversation completed!");
                firstQuestPumpMet = true;
                UpdateCharacterVisibility("Pumplscroob", false);
                break;

            case "met_skate":
                Debug.Log("Gobster conversation completed!");
                firstQuestGobsterMet = true;
                UpdateCharacterVisibility("Gobster", false);
                break;
                

            case "asked_gram":
                Debug.Log("Gram conversation completed!");
                firstQuestGramMet = true;
                UpdateCharacterVisibility("Grandma_Gob", false);
                UpdateCharacterVisibility("Pumplscroob", false);
                UpdateCharacterVisibility("Gobster", false);
                UpdateCharacterVisibility("Bill", true);
                break;

            case "met_2ndBill":
                Debug.Log("2ndBill conversation completed!");
                secondQuestBillMet = true;
                UpdateCharacterVisibility("Bill", false);
                UpdateCharacterVisibility("Grandpa", true);
                UpdateCharacterVisibility("Derpy_Unicorn_Buttponey", true);
                break;


            case "met_2ndCrank":
                Debug.Log("2ndCrank conversation completed!");
                secondQuestCrankMet = true;
                UpdateCharacterVisibility("Grandpa", false);
                UpdateCharacterVisibility("Tent_Dweller", true);
                break;

            case "met_2ndPump":
                Debug.Log("2ndPump conversation completed!");
                secondQuestPumpMet = true;
                UpdateCharacterVisibility("Tent_Dweller", false);
                UpdateCharacterVisibility("Gromblar", true);
                break;

            case "met_derpy":
                Debug.Log("Derpy conversation completed!");
                secondQuestDerpyMet = true;
                UpdateCharacterVisibility("Derpy_Unicorn_Buttponey", false);
                if (secondQuestGromblarMet)
                {
                    UpdateCharacterVisibility("Where's_Bill", true);
                }
                break;
            
            case "met_grom":
                Debug.Log("Grom conversation completed!");
                secondQuestGromblarMet = true;
                UpdateCharacterVisibility("Gromblar", false);

                if (secondQuestDerpyMet)
                {
                    UpdateCharacterVisibility("Where's_Bill", true);
                }
                break;

            case "met_3rdBill":
                //make game end
                break;

            

            default:
                Debug.LogWarning($"Unknown variable change: {variableName}");
                break;
        }
    }


    public void OnCharacterMet(string characterName)
    {
        string nameKey = characterName.ToLower();
        // handle name case-insensitively
        Debug.Log("Chater Name passed " + characterName);
        switch (nameKey)
        {
            case "the_commisioner":
                Debug.Log("Commissioner interaction started");
                break;

            case "bill_the_drawggin":
                Debug.Log("Bill interaction started");
                break;

            case "pump":
                Debug.Log("Pump interaction started");
                break;

            case "the_crank":
                Debug.Log("Crank interaction started");
                break;

            case "gobster":
                Debug.Log("Gobster interaction started");
                break;

            case "gram":
                Debug.Log("Gram interaction started");
                break;

            // Add other cases as needed

            default:
                Debug.Log($"{nameKey} interaction started");
                break;
        }

    }


    public void AssignCharacterReference(string characterName, GameObject characterObject)
    {
        // Convert to lowercase for case-insensitive comparison
        string nameKey = characterName.ToLower();

        switch (nameKey)
        {

            case "the_commisioner":
                The_Commisioner = characterObject;
                break;

            case "bill":
                Bill = characterObject;
                break;

            case "pumplscroob":
                Pumplscroob = characterObject;
                break;

            case "the_crank":
                The_Crank = characterObject;
                break;

            case "gobster":
                Gobster = characterObject;
                break;

            case "grandma_gob":
                Grandma_Gob = characterObject;
                break;

            case "derpy_unicorn_buttponey":
                Derpy_Unicorn_Buttponey = characterObject;
                break;

            case "gromblar":
                Gromblar = characterObject;
                break;
        
            case "bill_the_drawggin":
                Bill_the_Drawggin = characterObject;
                break;

            case "where's_bill":
                Wheres_Bill = characterObject;
                break;

            case "grandpa":
                Grandpa = characterObject;
                break;

            case "tent_dweller":
                Tent_Dweller = characterObject;
                break;

            default:
                Debug.LogWarning($"Character ({nameKey}) not recognized in StoryStateManager.");
                break;
        }

    }
    
}
