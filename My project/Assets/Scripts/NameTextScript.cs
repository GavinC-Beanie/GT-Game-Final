using UnityEngine;
using UnityEngine.UI;

public class CharacterNameDisplay : MonoBehaviour
{
    public Text nameText; // Assign your UI Text component for the name
    
    // Called by CharacterInteraction when dialogue starts
    public void DisplayCharacterTag(GameObject character)
    {
        if (nameText != null && character != null)
        {
            // Display the character's tag
            nameText.text = character.tag;
        }
    }
    
    // Clear the name display
    public void ClearName()
    {
        if (nameText != null)
        {
            nameText.text = "";
        }
    }
}
