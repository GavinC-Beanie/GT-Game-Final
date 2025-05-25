using UnityEngine;
using System.Collections.Generic;
using Ink.Runtime;
using Unity.VisualScripting;  // Ensure you have the Ink runtime imported to use Ink.Runtime.Story

public class StoryStateManager : MonoBehaviour
{

    public static StoryStateManager Instance { get; private set; }

    private Dictionary<string, bool> characterVisibility = new Dictionary<string, bool>();

    // Define story phase states
    public enum StoryPhase { Start, FirstQuestActive, SecondQuestActive, ThirdQuestActive }

    [SerializeField] private StoryPhase currentPhase = StoryPhase.Start;

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
    public GameObject Boss_Man;
    public GameObject Wheres_Bill;

    // Reference to the Ink story, if using Ink for narrative state
    public Story inkStory;

    // Flags to track which characters have been met in the current phase
    private bool firstQuestComMet, firstQuestBillMet, firstQuestPumpMet, firstQuestCrankMet, firstQuestGobsterMet, firstQuestGramMet;
    private bool secondQuestBillMet, secondQuestCrankMet, secondQuestDerpyMet, secondQuestPumpMet, secondQuestGromblarMet;
    private bool thirdQuestBillMet, thirdQuestComMet;
    void Start()
    {
        // Begin in the Start phase (The_Commisioner only)
        currentPhase = StoryPhase.Start;

        HideAllCharacters();

        // Show only the The_Commisioner
        

        UpdateVisibilityForPhase();
    }

    /// <summary>Hide all character GameObjects.</summary>
    private void HideAllCharacters()
    {
        if (The_Commisioner) The_Commisioner.SetActive(false);
        if (Bill) Bill.SetActive(false);
        if (Bill_the_Drawggin) Bill_the_Drawggin.SetActive(false);
        if (Pumplscroob) Pumplscroob.SetActive(false);
        if (The_Crank) The_Crank.SetActive(false);
        if (Gobster) Gobster.SetActive(false);
        if (Grandma_Gob) Grandma_Gob.SetActive(false);
        if (Derpy_Unicorn_Buttponey) Derpy_Unicorn_Buttponey.SetActive(false);
        if (Gromblar) Gromblar.SetActive(false);
        if (Wheres_Bill) Wheres_Bill.SetActive(false);
        if (Tent_Dweller) Tent_Dweller.SetActive(false);
        if (Boss_Man) Boss_Man.SetActive(false);
        if (Grandpa) Grandpa.SetActive(false);
    }





    /// <summary>Show/hide characters based on the current story phase.</summary>
    private void UpdateVisibilityForPhase()
    {
        Debug.Log("updating visiblity");
        
        Debug.Log("CURRENT PHASE: " + currentPhase);
        switch (currentPhase)
        {
            case StoryPhase.Start:
                Debug.Log("PHASE START");
                // First Quest begins: show Bill, Pump, Crank, Gobster. (Gram appears later when conditions met)
                if (The_Commisioner) The_Commisioner.SetActive(true);
                {
                Debug.Log("The Com set true");
        }
                if (Boss_Man) Boss_Man.SetActive(false);
                {
                Debug.Log("Boss Man set false");
        }
                if (Bill) Bill.SetActive(false);
                if (Pumplscroob) Pumplscroob.SetActive(false);
                if (The_Crank) The_Crank.SetActive(false);
                if (Gobster) Gobster.SetActive(false);
                if (Grandma_Gob) Grandma_Gob.SetActive(false);  // Gram starts hidden
                // Commissioner is presumably gone during the quest.
                break;

            case StoryPhase.SecondQuestActive:
                // Second Quest begins: initially only Bill is active (others come later in sequence).
                if (Bill) Bill.SetActive(true);
                // Crank, Derpy, Pump, Gromblar remain hidden until their turn in the sequence.
                break;

            case StoryPhase.ThirdQuestActive:
                // Third Quest begins: no previous characters should remain.
                // (Activate any new third quest characters here if applicable; none specified in this scenario.)
                if (Wheres_Bill) Wheres_Bill.SetActive(true);
                break;
        }
    }

    /// <summary>Transition from Start phase to First Quest.</summary>


    /// <summary>Transition from First Quest to Second Quest (called after Gram is interacted with).</summary>
    public void StartSecondQuest()
    {
        currentPhase = StoryPhase.SecondQuestActive;
        // Despawn all characters from first quest
        HideAllCharacters();
        // Reset second quest flags
        secondQuestBillMet = secondQuestCrankMet = secondQuestDerpyMet = secondQuestPumpMet = secondQuestGromblarMet = false;
        // Activate the first NPC for second quest (Bill)
        if (Bill) Bill.SetActive(true);
        if (inkStory != null && inkStory.variablesState.GlobalVariableExistsWithName("secondQuestStarted"))
        {
            inkStory.variablesState["secondQuestStarted"] = true;
        }
    }

    /// <summary>Transition from Second Quest to Third Quest.</summary>
    public void StartThirdQuest()
    {
        currentPhase = StoryPhase.ThirdQuestActive;
        // Despawn all remaining characters from second quest
        HideAllCharacters();
        if (inkStory != null && inkStory.variablesState.GlobalVariableExistsWithName("thirdQuestStarted"))
        {
            inkStory.variablesState["thirdQuestStarted"] = true;
        }
        // (If third quest has its own characters, they can be enabled here.)
    }




    private Queue<CharacterVisibilityChange> pendingVisibilityChanges = new Queue<CharacterVisibilityChange>();

    private struct CharacterVisibilityChange
    {
        public string characterName;
        public bool isVisible;
    }

    private void ApplyCharacterVisibility(string characterName, bool isVisible)
    {
        characterVisibility[characterName] = isVisible;

        if (!CharacterManager.Instance) return;

        if (isVisible)
        {
            CharacterManager.Instance.ShowCharacter(characterName);
        }
        else
        {
            CharacterManager.Instance.HideCharacter(characterName);
        }
    }

    public void UpdateCharacterVisibility(string characterName, bool isVisible)
    {
        if (DialogueManager.isDialogueActive)
        {
            pendingVisibilityChanges.Enqueue(new CharacterVisibilityChange
            {
                characterName = characterName,
                isVisible = isVisible
            });
        }
        else
        {
            ApplyCharacterVisibility(characterName, isVisible);
        }
    }

    public void ProcessPendingVisibilityChanges()
    {
        while (pendingVisibilityChanges.Count > 0)
        {
            var change = pendingVisibilityChanges.Dequeue();
            ApplyCharacterVisibility(change.characterName, change.isVisible);
        }
    }



    /// <summary>
    /// Call this when the player has met/interacted with a character. 
    /// It updates the state and triggers any necessary visibility or phase changes.
    /// </summary>
    public void OnCharacterMet(string characterName)
    {
        string nameKey = characterName.ToLower();  // handle name case-insensitively
        switch (nameKey)
        {
            case "commisioner":
                if (currentPhase == StoryPhase.FirstQuestActive)
                {
                    firstQuestComMet = true;
                    if (The_Commisioner) The_Commisioner.SetActive(false);

                    if (Bill_the_Drawggin) Bill_the_Drawggin.SetActive(true);
                    if (Pumplscroob) Pumplscroob.SetActive(true);
                    if (The_Crank) The_Crank.SetActive(true);
                    if (Gobster) Gobster.SetActive(true);


                }
                break;

            case "bill":
                if (currentPhase == StoryPhase.FirstQuestActive)
                {
                    firstQuestBillMet = true;
                    if (Bill) Bill.SetActive(false);  // hide Bill after meeting him
                    // If Bill **and** Crank are met, reveal Gram for the next step.
                    if (Grandma_Gob && firstQuestCrankMet && !Grandma_Gob.activeSelf)
                    {
                        Grandma_Gob.SetActive(true);
                    }
                    if (inkStory != null && inkStory.variablesState.GlobalVariableExistsWithName("billMet"))
                    {
                        inkStory.variablesState["billMet"] = true;
                    }
                }
                else if (currentPhase == StoryPhase.SecondQuestActive)
                {
                    secondQuestBillMet = true;
                    if (Bill) Bill.SetActive(false);
                    // After finishing Bill in second quest, activate Crank and Derpy.
                    if (The_Crank) The_Crank.SetActive(true);
                    if (Derpy_Unicorn_Buttponey) Derpy_Unicorn_Buttponey.SetActive(true);
                    if (inkStory != null && inkStory.variablesState.GlobalVariableExistsWithName("bill2Met"))
                    {
                        inkStory.variablesState["bill2Met"] = true;
                    }
                }
                break;

            case "pump":
                if (currentPhase == StoryPhase.FirstQuestActive)
                {
                    firstQuestPumpMet = true;
                    if (Pumplscroob) Pumplscroob.SetActive(false);
                    if (inkStory != null && inkStory.variablesState.GlobalVariableExistsWithName("pumpMet"))
                    {
                        inkStory.variablesState["pumpMet"] = true;
                    }
                }
                else if (currentPhase == StoryPhase.SecondQuestActive)
                {
                    secondQuestPumpMet = true;
                    if (Pumplscroob) Pumplscroob.SetActive(false);
                    // After Pump in second quest, show Gromblar.
                    if (Gromblar) Gromblar.SetActive(true);
                    if (inkStory != null && inkStory.variablesState.GlobalVariableExistsWithName("pump2Met"))
                    {
                        inkStory.variablesState["pump2Met"] = true;
                    }
                }
                break;

            case "crank":
                if (currentPhase == StoryPhase.FirstQuestActive)
                {
                    firstQuestCrankMet = true;
                    if (The_Crank) The_Crank.SetActive(false);
                    // If Crank **and** Bill are met, reveal Gram.
                    if (Grandma_Gob && firstQuestBillMet && !Grandma_Gob.activeSelf)
                    {
                        Grandma_Gob.SetActive(true);
                    }
                    if (inkStory != null && inkStory.variablesState.GlobalVariableExistsWithName("crankMet"))
                    {
                        inkStory.variablesState["crankMet"] = true;
                    }
                }
                else if (currentPhase == StoryPhase.SecondQuestActive)
                {
                    secondQuestCrankMet = true;
                    if (The_Crank) The_Crank.SetActive(false);
                    // If Crank and Derpy are both done in second quest, move to Pump.
                    if (secondQuestDerpyMet)
                    {
                        // Both NPCs in this stage are met, proceed to Pump.
                        if (Derpy_Unicorn_Buttponey) Derpy_Unicorn_Buttponey.SetActive(false);  // (Derpy would already be inactive if interacted with)
                        if (Pumplscroob) Pumplscroob.SetActive(true);
                    }
                    if (inkStory != null && inkStory.variablesState.GlobalVariableExistsWithName("crank2Met"))
                    {
                        inkStory.variablesState["crank2Met"] = true;
                    }
                }
                break;

            case "gobster":
                if (currentPhase == StoryPhase.FirstQuestActive)
                {
                    firstQuestGobsterMet = true;
                    if (Gobster) Gobster.SetActive(false);
                    if (inkStory != null && inkStory.variablesState.GlobalVariableExistsWithName("gobsterMet"))
                    {
                        inkStory.variablesState["gobsterMet"] = true;
                    }
                }
                // Gobster is not part of second quest, so no case for SecondQuestActive.
                break;

            case "gram":
                if (currentPhase == StoryPhase.FirstQuestActive)
                {
                    firstQuestGramMet = true;
                    if (Grandma_Gob) Grandma_Gob.SetActive(false);
                    if (inkStory != null && inkStory.variablesState.GlobalVariableExistsWithName("gramMet"))
                    {
                        inkStory.variablesState["gramMet"] = true;
                    }
                    // Gram being asked marks end of first quest -> begin second quest.
                    StartSecondQuest();
                }
                break;

            case "derpy":
                if (currentPhase == StoryPhase.SecondQuestActive)
                {
                    secondQuestDerpyMet = true;
                    if (Derpy_Unicorn_Buttponey) Derpy_Unicorn_Buttponey.SetActive(false);
                    // If Derpy and Crank are now both met, proceed to Pump.
                    if (secondQuestCrankMet)
                    {
                        if (Pumplscroob) Pumplscroob.SetActive(true);
                    }
                    if (inkStory != null && inkStory.variablesState.GlobalVariableExistsWithName("derpyMet"))
                    {
                        inkStory.variablesState["derpyMet"] = true;
                    }
                }
                // Derpy isn't present in first quest.
                break;

            case "gromblar":
                if (currentPhase == StoryPhase.SecondQuestActive)
                {
                    secondQuestGromblarMet = true;
                    if (Gromblar) Gromblar.SetActive(false);
                    if (inkStory != null && inkStory.variablesState.GlobalVariableExistsWithName("gromblarMet"))
                    {
                        inkStory.variablesState["gromblarMet"] = true;
                    }
                    // After Gromblar is met (and Derpy should be met by now), transition to third quest.
                    if (secondQuestDerpyMet)
                    {
                        StartThirdQuest();
                    }
                    // (If for some reason Derpy wasn�t met yet, we hold off transitioning until Derpy is done.)
                }
                break;

            case "where's bill":
                 if (currentPhase == StoryPhase.ThirdQuestActive)
                {
                    thirdQuestBillMet = true;
                    if (Wheres_Bill) Wheres_Bill.SetActive(false);
                    if (inkStory != null && inkStory.variablesState.GlobalVariableExistsWithName("bill3Met"))
                    {
                        inkStory.variablesState["bill3Met"] = true;
                    }
                }
                // Gobster is not part of second quest, so no case for SecondQuestActive.
                break;

            case "boss man":
                if (currentPhase == StoryPhase.ThirdQuestActive)
                {
                    thirdQuestComMet = true;
                    if (Boss_Man) Boss_Man.SetActive(false);
                    if (inkStory != null && inkStory.variablesState.GlobalVariableExistsWithName("gobsterMet"))
                    {
                        inkStory.variablesState["gobsterMet"] = true;
                    }
                }
                // Gobster is not part of second quest, so no case for SecondQuestActive.
                break;
        }
    }

    public void AssignCharacterReference(string characterName, GameObject characterObject)
    {
        // Convert to lowercase for case-insensitive comparison
        string nameKey = characterName.ToLower();

        switch (nameKey)
        {

            case "the commisioner":
                The_Commisioner = characterObject;
                break;
            case "bill":
                Bill = characterObject;
                break;
            case "pumplscroob":
                Pumplscroob = characterObject;
                break;
            case "the crank":
                The_Crank = characterObject;
                break;
            case "gobster":
                Gobster = characterObject;
                break;
            case "grandma gob":
                Grandma_Gob = characterObject;
                break;
            case "derpy unicorn buttponey":
                Derpy_Unicorn_Buttponey = characterObject;
                break;
            case "gromblar":
                Gromblar = characterObject;
                break;
            case "boss man":
                Boss_Man = characterObject;
                break;
            case "bill the drawggin'":
                Bill_the_Drawggin = characterObject;
                break;
            case "where's bill":
                Wheres_Bill = characterObject;
                break;
            case "grandpa!":
                Grandpa = characterObject;
                break;
            case "tent dweller":
                Tent_Dweller = characterObject;
                break;
            default:
                Debug.LogWarning($"Character ({nameKey}) not recognized in StoryStateManager.");
                break;
        }
        
    }
    
}
