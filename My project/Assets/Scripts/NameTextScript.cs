using UnityEngine;
using UnityEngine.UI;

public class CharacterNameDisplay : MonoBehaviour
{
    public Text nameText; // Assign your UI Text component for the name
    
    public void DisplayCharacterName(GameObject character)
    {
        if (nameText != null && character != null)
        {
            nameText.text = character.name;
        }
    }
    
    public void ClearName()
    {
        if (nameText != null)
        {
            nameText.text = "";
        }
    }
}