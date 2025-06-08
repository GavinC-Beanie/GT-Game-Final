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

    
    private void HideAllCharactersAgain()
    {
        if (The_Commisioner) ApplyCharacterVisibility("The_Commisioner", false);
        if (Bill) ApplyCharacterVisibility("Bill", false);
        if (Bill_the_Drawggin) ApplyCharacterVisibility("Bill_the_Drawggin", false);
        if (Pumplscroob) ApplyCharacterVisibility("Pumplscroob", false);
        if (The_Crank) ApplyCharacterVisibility("The_Crank", false);
        if (Gobster) ApplyCharacterVisibility("Gobster", false);
        if (Grandma_Gob) ApplyCharacterVisibility("Grandma_Gob", false);
        if (Derpy_Unicorn_Buttponey) ApplyCharacterVisibility("Derpy_Unicorn_Buttponey", false);
        if (Gromblar) ApplyCharacterVisibility("Gromblar", false);
        if (Wheres_Bill) ApplyCharacterVisibility("Wheres_Bill", false);
        if (Tent_Dweller) ApplyCharacterVisibility("Tent_Dweller", false);
        if (Grandpa) ApplyCharacterVisibility("Grandpa!", false);
    }

    /// <summary>Transition from Start phase to First Quest.</summary>


    /// <summary>Transition from First Quest to Second Quest (called after Gram is interacted with).</summary>
    public void StartSecondQuest()
    {
    
        // Despawn all characters from first quest
        HideAllCharactersAgain();
        // Reset second quest flags
        secondQuestBillMet = secondQuestCrankMet = secondQuestDerpyMet = secondQuestPumpMet = secondQuestGromblarMet = false;
        // Activate the first NPC for second quest (Bill)
        if (Bill) UpdateCharacterVisibility("Bill", true);
        if (inkStory != null && inkStory.variablesState.GlobalVariableExistsWithName("secondQuestStarted"))
        {
            inkStory.variablesState["secondQuestStarted"] = true;
        }
    }

    /// <summary>Transition from Second Quest to Third Quest.</summary>
    public void StartThirdQuest()
    {
        
        // Despawn all remaining characters from second quest
        HideAllCharactersAgain();
        if (Wheres_Bill) UpdateCharacterVisibility("Wheres_Bill", true);
        // (If third quest has its own characters, they can be enabled here.)
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




    public void OnCharacterMet(string characterName)
    {
        string nameKey = characterName.ToLower();
        // handle name case-insensitively
        Debug.Log("Chater Name passed " + characterName);
        switch (nameKey)
        {
           
            case "the_commisioner":
                Debug.Log("Commision intraction ending");

{
                    firstQuestComMet = true;
                    

                    if (inkStory != null && (bool)inkStory.variablesState["met_com"] == true)
                    {
                   
                        inkStory.variablesState["met_com"] = true;
                        Debug.Log("Variable Hit!!!");

                        UpdateCharacterVisibility("The_Commisioner", false);
                        Debug.Log("Com Uptaded to false");

                        UpdateCharacterVisibility("Bill_the_Drawggin", true);
                        UpdateCharacterVisibility("Pumplscroob", true);
                        UpdateCharacterVisibility("The_Crank", true);
                        UpdateCharacterVisibility("Gobster", true);
                    }




                    //UpdateCharacterVisibility("Bill_the_Drawggin", true);
                    //UpdateCharacterVisibility("Pumplscroob", true);
                    //UpdateCharacterVisibility("The_Crank", true);
                    //UpdateCharacterVisibility("Gobster", true);

                }
                break;

            case "bill_the_drawggin":
               
                {
                    firstQuestBillMet = true;
                    UpdateCharacterVisibility("Bill_the_Drawggin", false);

                    if (inkStory != null && inkStory.variablesState.GlobalVariableExistsWithName("met_bill"))
                    {
                        inkStory.variablesState["met_bill"] = true;
                    }
                }
                break;

            
            case "pump":
              
                {
                    firstQuestPumpMet = true;
                    UpdateCharacterVisibility("Pumplscroob", false);
                    
                    if (inkStory != null && inkStory.variablesState.GlobalVariableExistsWithName("met_pump"))
                    {
                        inkStory.variablesState["met_pump"] = true;
                    }
                }
               
                break;

            case "the_crank":
             
                {
                    firstQuestCrankMet = true;
                    UpdateCharacterVisibility("The_Crank", false);
                    // If Crank **and** Bill are met, reveal Gram.
                   
                    UpdateCharacterVisibility("Grandma_Gob", true);



                    if (inkStory != null && inkStory.variablesState.GlobalVariableExistsWithName("met_crank"))
                    {
                        inkStory.variablesState["met_crank"] = true;
                    }
                }
               
                break;

            case "gobster":
          
                {
                    firstQuestGobsterMet = true;
                    UpdateCharacterVisibility("Gobster", false);
                    if (inkStory != null && inkStory.variablesState.GlobalVariableExistsWithName("met_gobster"))
                    {
                        inkStory.variablesState["met_gobster"] = true;
                    }
                }
                // Gobster is not part of second quest, so no case for SecondQuestActive.
                break;

            case "gram":
         
                {
                    firstQuestGramMet = true;
                    UpdateCharacterVisibility("Grandma_Gob", false);
                    UpdateCharacterVisibility("Pumplscroob", false);
                    UpdateCharacterVisibility("Gobster", false);
                    if (inkStory != null && inkStory.variablesState.GlobalVariableExistsWithName("met_gram"))
                    {
                        inkStory.variablesState["met_gram"] = true;
                    }
                    // Gram being asked marks end of first quest -> begin second quest.
                    StartSecondQuest();
                }
                break;

            case "bill":
         
                {
                    secondQuestBillMet = true;
                    UpdateCharacterVisibility("Bill", false);

                    UpdateCharacterVisibility("Derpy_Unicorn_Buttponey", true);
                    UpdateCharacterVisibility("Grandpa!", true);

                    if (inkStory != null && inkStory.variablesState.GlobalVariableExistsWithName("met_2ndBill"))
                    {
                        inkStory.variablesState["met_2ndBill"] = true;
                    }
                }
                
                break;

            case "grandpa!":
  
                {
                    secondQuestCrankMet = true;
                    UpdateCharacterVisibility("Grandpa!", false);

                    UpdateCharacterVisibility("Tent_Dweller", true);

                    if (inkStory != null && inkStory.variablesState.GlobalVariableExistsWithName("met_2ndCrank"))
                    {
                        inkStory.variablesState["met_2ndCrank"] = true;
                    }
                }

                break;

            case "derpy_unicorn_buttponey":
       
                {
                    secondQuestDerpyMet = true;
                    UpdateCharacterVisibility("Derpy_Unicorn_Buttponey", false);
                    
                    if (inkStory != null && inkStory.variablesState.GlobalVariableExistsWithName("met_derpy"))
                    {
                        inkStory.variablesState["met_derpy"] = true;
                    }
                    if (secondQuestGromblarMet)
                    {
                        StartThirdQuest();
                    }
                }
                
                break;

            case "tent_dweller":

                {
                    secondQuestPumpMet = true;
                    UpdateCharacterVisibility("Tent_Dweller", true);

                    UpdateCharacterVisibility("Gromblar", true);

                    if (inkStory != null && inkStory.variablesState.GlobalVariableExistsWithName("met_2ndPump"))
                    {
                        inkStory.variablesState["met_2ndPump"] = true;
                    }
                }

                break;

            case "gromblar":
                {
                    secondQuestGromblarMet = true;
                    UpdateCharacterVisibility("Gromblar", false);
                    if (inkStory != null && inkStory.variablesState.GlobalVariableExistsWithName("met_gromblar"))
                    {
                        inkStory.variablesState["met_gromblar"] = true;
                    }
                    // After Gromblar is met (and Derpy should be met by now), transition to third quest.
                    if (secondQuestDerpyMet)
                    {
                        StartThirdQuest();
                    }
                    // (If for some reason Derpy wasn�t met yet, we hold off transitioning until Derpy is done.)
                }
                break;

            case "where's_bill":
         
                {
                    thirdQuestBillMet = true;
                    UpdateCharacterVisibility("Wheres_Bill", false);
                    if (inkStory != null && inkStory.variablesState.GlobalVariableExistsWithName("met_3rdBill"))
                    {
                        inkStory.variablesState["met_3rdBill"] = true;
                    }
                }
                break;

            default:
                Debug.Log("default for " + nameKey);
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

            case "grandpa!":
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
